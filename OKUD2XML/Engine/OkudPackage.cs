using System.Collections;
using System.Collections.Generic;
using System.IO;
using OKUD2XML.Core;
using Spire.Doc;
using Spire.Doc.Documents;

namespace OKUD2XML.Engine
{
    public class OkudPackage : IEnumerable<Okud>
    {
        private readonly string _file;
        private readonly FileFormat _format;

        private OkudPackage(string file, FileFormat format)
        {
            _file = file;
            _format = format;
        }

        public static OkudPackage OpenRtf(string file)
        {
            return new OkudPackage(file, FileFormat.Rtf);
        }

        public IEnumerator<Okud> GetEnumerator()
        {
            Document source = new Document();

            using (FileStream stream = new FileStream(_file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                source.LoadFromStream(stream, _format);
            }

            source.LastParagraph.AppendBreak(BreakType.PageBreak);

            int index = 1;

            foreach (Section s in source.Sections)
            {
                foreach (Paragraph p in s.Body.Paragraphs)
                {
                    var target = new Document();
                    var section = target.AddSection();
                    section.PageSetup.Orientation = PageOrientation.Landscape;
                    var paragraph = section.AddParagraph();

                    foreach (DocumentObject obj in p.ChildObjects)
                    {
                        if (obj is Break br && br.BreakType == BreakType.PageBreak)
                        {
                            yield return new Okud(target, $"{Path.GetFileNameWithoutExtension(_file)}-part{index++:0000}");

                            target = new Document();
                            section = target.AddSection();
                            section.PageSetup.Orientation = PageOrientation.Landscape;
                            paragraph = section.AddParagraph();
                        }
                        else
                            paragraph.ChildObjects.Add(obj.Clone());

                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
