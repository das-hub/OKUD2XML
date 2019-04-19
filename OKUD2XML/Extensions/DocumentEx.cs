using System.Collections.Generic;
using OKUD2XML.Core;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;

namespace OKUD2XML.Extensions
{
    public static class DocumentEx
    {
        public static DocumentInfo ExtractInfo(this Document doc)
        {
            List<string> values = new List<string>();

            foreach (Section section in doc.Sections)
            {
                foreach (Paragraph paragraph in section.Body.Paragraphs)
                {
                    foreach (DocumentObject obj in paragraph.ChildObjects)
                    {
                        if (obj is ShapeObject shape && shape.ShapeType == ShapeType.Rectangle)
                        {
                            foreach (DocumentObject o in shape.ChildObjects) if (o is Paragraph p) values.Add(p.Text);
                        }
                    }   
                }
            }

            return new DocumentInfo(values.ToArray());
        }

        public static OkudXml CreateOkudXml(this Document doc)
        {
            OkudXml result = new OkudXml();

            DocumentInfo info = doc.ExtractInfo();

            result.Dt = info.Dt;
            result.AdoptionDate = info.AdoptionDate;

            OkudXml.OkudXmlRow row = result.AddRow();

            row.NotificationNumber = info.NotificationNumber;
            row.Dt = info.RowDt;
            row.OperationCode = info.OperationCode;
            row.CurrencyCode = info.CurrencyCode;
            row.Sum = info.Sum;
            row.DocumentNumber = info.DocumentNumber;

            return result;
        }
    }
}
