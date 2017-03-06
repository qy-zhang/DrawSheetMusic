using System.Collections.Generic;
using symbol;

namespace xmlParser
{
    public class XmlFacade
    {
        private string _filename;
        private string _workTitle;
        private string _creator;
        private Beat _beat;
        private Head _highHead;
        private Head _lowHead;
        private List<Symbol> _highSymbolList;
        private List<Symbol> _lowSymbolList;
        private List<List<List<Symbol>>> _measureSymbolList;
        private List<Measure> _measureList;

        public XmlFacade(string filename) {
            _filename = filename;
            Init();
        }

        private void Init() {
            XmlParser xmlParser = new XmlParser(_filename);

            _workTitle = xmlParser.GetWorkTitle();
            _creator = xmlParser.GetCreator();
            _beat = xmlParser.GetBeat();
            _highHead = xmlParser.GetHighHead();
            _lowHead = xmlParser.GetLowHead();
            _highSymbolList = xmlParser.GetHighSymbolList();
            _lowSymbolList = xmlParser.GetLowSymbolList();
            _measureSymbolList = xmlParser.GetMeasureSymbolList();
            _measureList = xmlParser.GetMeasureList();
        }

        public string GetWorkTitle() { return _workTitle; }

        public string GetCreator() { return _creator; }

        public Beat GetBeat() { return _beat; }

        public Head GetHighHead() { return _highHead; }

        public Head GetLowHead() { return _lowHead; }

        public List<Symbol> GetHighSymbolList() { return _highSymbolList; }

        public List<Symbol> GetLowSymbolList() { return _lowSymbolList; }

        public List<List<List<Symbol>>> GetMeasureSymbolList() { return _measureSymbolList; }

        public List<Measure> GetMeasureList() { return _measureList; }
    }
}