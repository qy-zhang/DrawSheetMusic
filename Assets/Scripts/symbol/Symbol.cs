using UnityEngine;

namespace symbol
{
    public abstract class Symbol
    {
        protected int Duration;     //  符号时长
        protected int StartTime;    //  符号起始时刻
        protected int StopTime;     //  符号结束时刻
        protected int Type;         //  符号类型，1代表全音符/休止符、2代表二分音符/休止符、以此类推
        protected int Dot;      //  附点
        protected int SymbolWidth;  //  符号视图宽度
        protected bool isChord;  //  是否是和弦
        protected SymbolView symbolView;

        public int GetDuration() {return Duration;}

        public void SetDuration(string divisions, string duration) {Duration = 64 * int.Parse(duration) / int.Parse(divisions);}

        public int GetStartTime() { return StartTime; }

        public void SetStartTime(int startTime) {StartTime = startTime * 100 / 12;}

        public int GetStopTime() {return StopTime;}

        public void SetStopTime(int stopTime) {StopTime = stopTime * 100 / 12;}

        public new int GetType() {return Type;}

        public int GetDot() {return Dot;}

        public void SetDot(int dot) {Dot = dot;}

        public int GetSymbolWidth() {return SymbolWidth;}

        public void SetSymbolWidth(int symbolWidth) {SymbolWidth = symbolWidth;}

        public bool IsChord() {return isChord;}

        public void SetChord(bool chord) {isChord = chord;}

        public void ChangeColor(Color color) { GetSymbolView().SetColor(color); }

        public SymbolView GetSymbolView() {return symbolView;}

        public void SetSymbolView(SymbolView symbolview) {symbolView = symbolview;}

        public abstract void SetType(string type);

        public abstract int GetRate();
    }
}