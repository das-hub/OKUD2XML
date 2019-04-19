using System.IO;
using OKUD2XML.Extensions;
using Spire.Doc;

namespace OKUD2XML.Core
{
    public class Okud
    {
        private readonly Document _document;

        public Okud(Document document, string fileName)
        {
            Name = fileName;
            _document = document;
        }

        public string Name { get; }

        public string SaveAs(string path)
        {
            OkudXml xml = _document.CreateOkudXml();

            xml.ImagePath = Path.Combine(path, $"{Name}.pdf");

            _document.SaveToFile(xml.ImagePath, FileFormat.PDF);
            xml.SaveAs(Path.Combine(path, $"{Name}.xml"));

            return Path.Combine(path, $"{Name}.xml/pdf");
        }
    }
}
