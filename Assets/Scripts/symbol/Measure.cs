using System;
using System.Collections.Generic;

namespace symbol
{
    public class Measure
    {
        private List<List<List<Symbol>>> _measureSymbolList;
        private Beat _beat;
        private Head _highHead;
        private Head _lowHead;
        private bool _hasHead; // 小节首部是否有头部信息
        private bool _hasBeat; // 小节首部是否有拍号信息
        private int _maxCount; // 小节中高低表乐符数最大值
        private int _measureLength; // 小节总长度

        public Measure(List<List<List<Symbol>>> measureSymbolList)
        {
            _measureSymbolList = measureSymbolList;
        }

        public List<List<List<Symbol>>> GetMeasureSymbolList() { return _measureSymbolList; }

        public bool HasHead() { return _hasHead; }

        public void SetHasHead(bool hasHead) { _hasHead = hasHead; }

        public bool HasBeat() { return _hasBeat; }

        public void SetHasBeat(bool hasBeat) { _hasBeat = hasBeat; }

        public Beat GetBeat() { return _beat; }

        public void SetBeat(Beat beat) { _beat = beat; }

        public List<Head> GetHead()
        {
            List<Head> headList = new List<Head>();
            headList.Add(_highHead);
            headList.Add(_lowHead);
            return headList;
        }

        public void SetHead(Head highHead, Head lowHead) { _highHead = highHead; _lowHead = lowHead; }

        public void SetMaxCount(int maxCount) { _maxCount = maxCount; }

        public int GetMaxCount() { return _maxCount; }

        public void SetMeasureLength(int measureLength) { _measureLength = measureLength; }

        public int GetMeasureLength() { return _measureLength; }
    }
}