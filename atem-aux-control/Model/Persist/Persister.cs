using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace midi_aux_control.Model.Persist
{
    public class Persister
    {

        #region Singleton
        public static Persister Instance
        {
            get
            {
                if (instance == null)
                    instance = new Persister();
                return instance;
            }
        }

        private static Persister instance = null;

        private Persister()
        { }
        #endregion

        List<IPersistable> persistables = new List<IPersistable>();
       
        public void RegisterPersistable(IPersistable obj)
        {
            persistables.Add(obj);
        }

        private Dictionary<string, Dictionary<string, string>> data = new Dictionary<string, Dictionary<string, string>>();

        private static readonly XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
        {
            CloseOutput = false,
            Indent = true,
            IndentChars = "\t"
        };

        private const string SETTINGS_FILE_PATH = "settings.xml";

        private const string ROOT_TAG = "settings";
        private const string ARRAY_TAG = "array";
        private const string ARRAY_ATTRIBUTE_NAME = "name";
        private const string ENTRY_TAG = "entry";
        private const string ENTRY_ATTRIBUTE_KEY = "key";
        private const string ENTRY_ATTRIBUTE_VALUE = "value";

        public void RequestPersist(IPersistable source)
        {

            XElement rootElement = new XElement(ROOT_TAG);

            foreach (IPersistable obj in persistables)
            {
                XElement objElement = serializePersistable(obj);
                if (objElement != null)
                    rootElement.Add(objElement);
            }

            using (FileStream stream = new FileStream(SETTINGS_FILE_PATH, FileMode.Create))
            using (XmlWriter writer = XmlWriter.Create(stream, xmlWriterSettings))
            {
                rootElement.WriteTo(writer);
            }

        }

        private static XElement serializePersistable(IPersistable obj)
        {
            XElement xmlElement = new XElement(ARRAY_TAG);
            xmlElement.SetAttributeValue(ARRAY_ATTRIBUTE_NAME, obj.PersistenceKey);
            foreach (var pair in obj.GetPersistableData())
            {
                XElement attributeElement = new XElement(ENTRY_TAG);
                attributeElement.SetAttributeValue(ENTRY_ATTRIBUTE_KEY, pair.Key);
                attributeElement.SetAttributeValue(ENTRY_ATTRIBUTE_VALUE, pair.Value);
                xmlElement.Add(attributeElement);
            }
            return xmlElement;
        }

        public void Load()
        {

            data = new Dictionary<string, Dictionary<string, string>>();

            try
            {

                XmlDocument doc = new XmlDocument();
                doc.Load(SETTINGS_FILE_PATH);

                XmlNode root = doc.DocumentElement;
                if (root.LocalName != ROOT_TAG)
                    return;

                foreach (XmlNode node in root.ChildNodes)
                {
                    string key = node.Attributes[ARRAY_ATTRIBUTE_NAME].Value;
                    Dictionary<string, string> array = deserializePersistable(node);
                    if (array != null)
                        data.Add(key, array);
                }

                Distribute();

            }
            catch
            { }

        }

        public void Distribute(params IPersistable[] exclude)
        {
            foreach (IPersistable persistable in persistables)
                if (!exclude.Contains(persistable))
                    persistable.SetPersistableData(GetData(persistable.PersistenceKey));
        }

        public Dictionary<string, string> GetData(string key)
        {
            if (data.TryGetValue(key, out Dictionary<string, string> values))
                return values;
            return new Dictionary<string, string>();
        }

        private static Dictionary<string, string> deserializePersistable(XmlNode node)
        {

            try
            {

                string persistenceKey = node.Attributes[ARRAY_ATTRIBUTE_NAME].Value;

                Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
                foreach (XmlNode attributeNode in node.ChildNodes)
                {
                    if (attributeNode.LocalName == ENTRY_TAG)
                    {
                        string key = attributeNode.Attributes[ENTRY_ATTRIBUTE_KEY].Value;
                        string value = attributeNode.Attributes[ENTRY_ATTRIBUTE_VALUE].Value;
                        if (!string.IsNullOrWhiteSpace(key))
                            keyValuePairs.Add(key, value);
                    }
                }

                return keyValuePairs;

            }
            catch
            {
                return null;
            }

        }

    }
}
