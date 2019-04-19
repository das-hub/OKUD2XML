namespace OKUD2XML.Core
{
    public class DocumentInfo
    {
        private readonly string[] _content;

        public DocumentInfo(string[] content)
        {
            _content = content;
        }

        public string Dt => GetValue(7);

        public string AdoptionDate => GetValue(65);

        public string NotificationNumber => GetValue(38);

        public string RowDt => GetValue(45);

        public string OperationCode => GetValue(40);

        public string CurrencyCode => GetValue(41);

        public string Sum => GetValue(46);

        public string DocumentNumber => GetValue(42);

        private string GetValue(int index)
        {
            return IsExists(index) ? _content[index] : "Данные не найдены";
        }

        private bool IsExists(int index)
        {
            return index <= _content.Length;
        }
    }
}
