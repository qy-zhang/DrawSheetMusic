using System.Collections.Generic;

namespace symbol
{
    public class Note : Symbol
    {
        private string _step;        //  音阶
        private string _octave;      //  八度
        private string _beam;        //  符杠
        private string _accidental;  //  临时
        private int _shift;          //  与标准音符（三线音符）的位置偏移
        private int _end;            //  音符最上/下处坐标
        private Note _lastNote;      //  连音中的上一个音符
        private List<Note> _chordList = new List<Note>();
        private bool _slur;
        private bool _last;
        private bool _next;
        private bool _hasChord;
        private bool _upOrDown;

        public Note(string step, string octave) {
            _step = step;
            _octave = octave;
        }

        public string GetStep() { return _step; }

        public string GetOctave() { return _octave; }

        public string GetBeam() { return _beam; }

        public void SetBeam(string beam) { _beam = beam; }

        public string GetAccidental() { return _accidental; }

        public void SetAccidental(string accidental) { _accidental = accidental; }

        public int GetShift() { return _shift; }

        public void SetShift(int shift) { _shift = shift; }

        public int GetEnd() { return _end; }

        public void SetEnd(int end) { _end = end; }

        public Note GetLastNote() { return _lastNote; }

        public void SetLastNote(Note lastNote) { _lastNote = lastNote; }

        public List<Note> GetChordList() { return _chordList; }

        public bool IsSlur() { return _slur; }

        public void SetSlur(bool slur) { _slur = slur; }

        public bool IsLast() { return _last; }

        public void SetLast(bool last) { _last = last; }

        public bool IsNext() { return _next; }

        public void SetNext(bool next) { _next = next; }

        public bool HasChord() { return _hasChord; }

        public void SetHasChord(bool hasChord) { _hasChord = hasChord; }

        public bool IsUpOrDown() {
            if (IsSlur()) {
                return _upOrDown;
            }
//            return GetShift() < 0;
            return _upOrDown;
        }

        public void SetUpOrDown(bool upOrDown) { _upOrDown = upOrDown; }

        public override void SetType(string type) {
            switch (type) {
                case "whole":   base.Type = 1; break;
                case "half":    base.Type = 2; break;
                case "quarter": base.Type = 4; break;
                case "eighth":  base.Type = 8; break;
                case "16th":    base.Type = 16; break;
                default: break;
            }
        }

        public override int GetRate() {
            int rate = 1;
            switch (_step) {
                case "C": rate = 33; break;
                case "D": rate = 37; break;
                case "E": rate = 41; break;
                case "F": rate = 44; break;
                case "G": rate = 49; break;
                case "A": rate = 55; break;
                case "B": rate = 62; break;
                default: break;
            }
            switch (_octave) {
                case "2": rate *= 2; break;
                case "3": rate *= 4; break;
                case "4": rate *= 8; break;
                case "5": rate *= 16; break;
                case "6": rate *= 32; break;
                case "7": rate *= 64; break;
                default: break;
            }
            return rate;
        }
    }
}