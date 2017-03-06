namespace symbol
{
    public class Beat
    {
        private string _beats; // 拍号
        private string _beatType; // 节拍类型

        public Beat(string beats, string beatType)
        {
            _beats = beats;
            _beatType = beatType;
        }

        public string GetBeats() { return _beats; }

        public string GetBeatType() { return _beatType; }
    }
}