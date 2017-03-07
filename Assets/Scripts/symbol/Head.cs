namespace symbol
{
    public class Head
    {
        private string _fifths;
        private string _sign;
        private string _line;

        public Head(string fifths, string sign, string line)
        {
            _fifths = fifths;
            _sign = sign;
            _line = line;
        }

        public string GetFifths() { return _fifths; }

        public string GetSign() { return _sign; }

        public string GetLine() { return _line; }
    }
}