using System.IO;
using System.Text;
using System.Xml.Linq;

namespace OKUD2XML.Core
{
    public class OkudXml
    {
        private readonly XDocument _context;
        public OkudXml()
        {
            _context = new XDocument(new XDeclaration("1.0", "utf-8", null), 
                                        new XElement("Document", 
                                            new XAttribute("ImagePath", ""),
                                            new XAttribute("Dt", ""),
                                            new XAttribute("AdoptionDate", "")));
        }

        public string ImagePath
        {
            get => (string) _context.Root.Attribute("ImagePath");
            set => _context.Root.SetAttributeValue("ImagePath", value);
        }

        public string Dt
        {
            get => (string)_context.Root.Attribute("Dt");
            set => _context.Root.SetAttributeValue("Dt", value);
        }

        public string AdoptionDate
        {
            get => (string)_context.Root.Attribute("AdoptionDate");
            set => _context.Root.SetAttributeValue("AdoptionDate", value);
        }

        public OkudXmlRow AddRow()
        {
            XElement element = new XElement("DocumentRow", 
                                  new XElement("NotificationNumber", ""),
                                  new XElement("Dt", ""),
                                  new XElement("OperationCode", ""),
                                  new XElement("CurrencyCode", ""),
                                  new XElement("Sum", ""),
                                  new XElement("DocumentNumber", ""));

            _context.Root.Add(element);

            return new OkudXmlRow(element);
        }

        public void SaveAs(string file)
        {
            using (StringWriterWithEncoding writer = new StringWriterWithEncoding(Encoding.UTF8))
            {
                _context.Save(writer);
                File.WriteAllText(file, writer.ToString());
            }

        }

        public class OkudXmlRow
        {
            private readonly XElement _context;
            public OkudXmlRow(XElement context)
            {
                _context = context;
            }

            public string NotificationNumber
            {
                get => (string)_context.Element("NotificationNumber").Value;
                set => _context.Element("NotificationNumber").Value = value;
            }

            public string Dt
            {
                get => (string)_context.Element("Dt").Value;
                set => _context.Element("Dt").Value = value;
            }

            public string OperationCode
            {
                get => (string)_context.Element("OperationCode").Value;
                set => _context.Element("OperationCode").Value = value;
            }

            public string CurrencyCode
            {
                get => (string)_context.Element("CurrencyCode").Value;
                set => _context.Element("CurrencyCode").Value = value;
            }

            public string Sum
            {
                get => (string)_context.Element("Sum").Value;
                set => _context.Element("Sum").Value = value;
            }

            public string DocumentNumber
            {
                get => (string)_context.Element("DocumentNumber").Value;
                set => _context.Element("DocumentNumber").Value = value;
            }
        }
    }

    public class StringWriterWithEncoding : StringWriter
    {
        public StringWriterWithEncoding(Encoding encoding)
        {
            Encoding = encoding;
        }

        public override Encoding Encoding { get; }
    }
}
