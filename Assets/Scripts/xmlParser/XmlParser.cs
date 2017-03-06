using System;
using System.Collections.Generic;
using System.Xml;
using symbol;
using util;

namespace xmlParser
{
    public class XmlParser
    {
        private static string[] PITCH_NAME = {"C", "D", "E", "F", "G", "A", "B"};
        private int _highTime = 0; //  乐谱演奏时间
        private int _lowTime = 0; //  乐谱演奏时间
        private bool _isChord = false; //  是否是和弦
        private string _filename; //  musicXML文件内容
        private string _workTitle = ""; //  作品题目
        private string _creator = ""; //  创建者
        private string _divisions = ""; //  分割
        private string _fifths = ""; //  五度环
        private string _beats = ""; //  节拍
        private string _beatType = ""; //  节拍类型
        private string _clef = ""; //  谱号
        private string _sign = ""; //  符号
        private string _line = ""; //  线值
        private string _step = ""; //  音阶
        private string _octave = ""; //  八度
        private string _duration = ""; //  持续
        private string _type = ""; //  类型
        private string _accidental = ""; //  临时记号
        private string _staff = ""; //  五线谱
        private string _stem = ""; // 符干方向
        private string _beam = ""; //  符杠
        private string _beamNum = ""; //  符杠序号
        private Symbol _symbol;
        private Beat _beat;
        private Head _highHead;
        private Head _lowHead;
        private Slur _slur;
        private string _highStandardStep = "F";
        private string _highStandardOctave = "4";
        private string _lowStandardStep = "F";
        private string _lowStandardOctave = "4";
        private List<Symbol> _highSymbolList = new List<Symbol>(); // 高音乐符列表
        private List<Symbol> _lowSymbolList = new List<Symbol>(); // 低音乐符列表

        private List<Symbol> _highSymbolMeasure = new List<Symbol>(); // 一节中的高音乐符
        private List<Symbol> _lowSymbolMeasure = new List<Symbol>(); // 一节中的低音乐符
        private List<List<List<Symbol>>> _measureSymbolList = new List<List<List<Symbol>>>(); // 小节乐符列表
        private List<Measure> _measureList = new List<Measure>(); // 小节表
        private Beat _measureBeat; // 小节中的节拍
        private Head _measureHighHead; // 小节中的高音头
        private Head _measureLowHead; // 小节中的低音头

        public XmlParser(string filename)
        {
            _filename = filename;
            Init();
        }

        private void Init()
        {
            try
            {
                _highSymbolList = new List<Symbol>();
                _lowSymbolList = new List<Symbol>();

                XmlReaderSettings readerSettings = new XmlReaderSettings();
                readerSettings.ProhibitDtd = false;
//                readerSettings.DtdProcessing = DtdProcessing.Ignore;
                XmlReader xmlReader = XmlReader.Create(_filename, readerSettings);
//                XmlReader xmlReader = XmlReader.Create(_filename);

                while (xmlReader.Read())
                {
                    if (xmlReader.NodeType == XmlNodeType.Element)
                    {
                        switch (xmlReader.Name)
                        {
                            case "work-title": _workTitle = xmlReader.ReadString(); break;
                            case "creator": _creator = xmlReader.ReadString(); break;
                            case "divisions": _divisions = xmlReader.ReadString(); break;
                            case "fifths": _fifths = xmlReader.ReadString(); break;
                            case "beats": _beats = xmlReader.ReadString(); break;
                            case "beat-type": _beatType = xmlReader.ReadString(); break;
                            case "clef": _clef = xmlReader.GetAttribute("number"); break;
                            case "sign": _sign = xmlReader.ReadString(); break;
                            case "line": _line = xmlReader.ReadString(); break;
                            case "step": _step = xmlReader.ReadString(); break;
                            case "octave": _octave = xmlReader.ReadString(); break;
                            case "duration": _duration = xmlReader.ReadString(); break;
                            case "type": _type = xmlReader.ReadString(); break;
                            case "accidental": _accidental = xmlReader.ReadString(); break;
                            case "staff": _staff = xmlReader.ReadString(); break;
                            case "stem": _stem = xmlReader.ReadString(); break;
                            case "beam":
                                _beamNum = xmlReader.GetAttribute("number");
                                if (_beamNum.Equals("1"))
                                {
                                    _beam = xmlReader.ReadString();
                                }
                                break;
                            case "rest": // 休止符，由于休止符是self closing <rest /> 的，所以放在这里
                                _symbol = new Rest();
                                _symbol.SetChord(false);
                                break;
                            case "chord": // 和弦，由于和弦也是self closing <chord /> 的，所以也放在这里
                                _isChord = true; break;
                        }
                    }
                    else if (xmlReader.NodeType == XmlNodeType.EndElement)
                    {
                        if (xmlReader.Name.Equals("time"))
                        {
                            // 节拍
                            _beat = new Beat(_beats, _beatType);
                            _measureBeat = new Beat(_beats, _beatType);
                        }

                        if (xmlReader.Name.Equals("clef"))
                        {
                            // 谱号
                            if (_clef.Equals("1"))
                            {
                                _highHead = new Head(_fifths, _sign, _line);
                                _measureHighHead = new Head(_fifths, _sign, _line);
                                SetHighStandard();
                            }
                            else if (_clef.Equals("2"))
                            {
                                _lowHead = new Head(_fifths, _sign, _line);
                                _measureLowHead = new Head(_fifths, _sign, _line);
                                SetLowStandard();
                            }
                        }

                        if (xmlReader.Name.Equals("pitch"))
                        {
                            //  音高
                            _symbol = new Note(_step, _octave);
                            _symbol.SetChord(_isChord);
                            _isChord = false;
                        }

                        if (xmlReader.Name.Equals("dot"))
                        {
                            _symbol.SetDot(1); // 附点
                        }

                        if (xmlReader.Name.Equals("note"))
                        {
                            //  音符，包括音符及休止符
                            _symbol.SetDuration(_divisions, _duration);
                            _symbol.SetType(_type);

                            bool isNote = _symbol is Note;
                            if (isNote)
                            {
                                ((Note) _symbol).SetAccidental(_accidental);
                                _accidental = "";
                                if (_stem.Equals("up")) ((Note) _symbol).SetUpOrDown(true);
                                else if (_stem.Equals("down")) ((Note) _symbol).SetUpOrDown(false);
                            }

                            if (_staff.Equals("1"))
                            {
                                if (isNote)
                                {
                                    SetShift((Note) _symbol, _highStandardStep, _highStandardOctave);
                                    SetBeam(_highSymbolList);
                                    SetBeam(_highSymbolMeasure);
                                }
                                if (_symbol.IsChord())
                                {
                                    SetChord(_highSymbolList);
                                    SetChord(_highSymbolMeasure);
                                }
                                else
                                {
                                    _symbol.SetStartTime(_highTime);
                                    _symbol.SetStopTime((_highTime += _symbol.GetDuration()));
                                    _highSymbolList.Add(_symbol);
                                    _highSymbolMeasure.Add(_symbol);
                                }
                            }
                            else
                            {
                                if (isNote)
                                {
                                    SetShift((Note) _symbol, _lowStandardStep, _lowStandardOctave);
                                    SetBeam(_lowSymbolList);
                                    SetBeam(_lowSymbolMeasure);
                                }
                                if (_symbol.IsChord())
                                {
                                    SetChord(_lowSymbolList);
                                    SetChord(_lowSymbolMeasure);
                                }
                                else
                                {
                                    _symbol.SetStartTime(_lowTime);
                                    _symbol.SetStopTime((_lowTime += _symbol.GetDuration()));
                                    _lowSymbolList.Add(_symbol);
                                    _lowSymbolMeasure.Add(_symbol);
                                }
                            }
                        }
                        if (xmlReader.Name.Equals("measure")) // 小节结束
                        {
                            // 整合这一个小节中的乐符
                            int maxCount = ArrangeMeasure();

                            // 存入小节
                            Measure measure = new Measure(_measureSymbolList);
                            measure.SetMaxCount(maxCount);
                            if (_measureBeat == null)
                            {
                                measure.SetHasBeat(false);
                            }
                            else
                            {
                                measure.SetHasBeat(true);
                                measure.SetBeat(GetBeat());
                            }
                            if (_measureHighHead == null || _measureLowHead == null)
                            {
                                measure.SetHasHead(false);
                            }
                            else
                            {
                                measure.SetHasHead(true);
                                measure.SetHead(GetHighHead(), GetLowHead());
                            }

                            _measureList.Add(measure);

                            // 将相应List清零，下一个小节再重新赋值
                            _highSymbolMeasure = new List<Symbol>();
                            _lowSymbolMeasure = new List<Symbol>();
                            _measureSymbolList = new List<List<List<Symbol>>>();

                            _measureBeat = null;
                            _measureHighHead = null;
                            _measureLowHead = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public string GetWorkTitle() { return _workTitle; }

        public string GetCreator() { return _creator; }

        public Beat GetBeat() { return _beat; }

        public Head GetHighHead()  { return _highHead; }

        public Head GetLowHead() { return _lowHead; }

        public List<Symbol> GetHighSymbolList() { return _highSymbolList; }

        public List<Symbol> GetLowSymbolList() { return _lowSymbolList; }

        public List<List<List<Symbol>>> GetMeasureSymbolList() { return _measureSymbolList; }

        public List<Measure> GetMeasureList() { return _measureList; }

        private int GetStandard() {
            int standard = 0;
            switch (_sign)
            {
                case "G": standard = 4; break;
                case "F": standard = 3; break;
            }
            return standard;
        }

        private void SetHighStandard()
        {
            int standard = GetStandard();
            int temp = 2 * (3 - int.Parse(_line));
            _highStandardStep = PITCH_NAME[(temp + standard) % 7];
            _highStandardOctave = standard.ToString();
        }

        private void SetLowStandard()
        {
            int standard = GetStandard();
            int temp = 2 * (3 - int.Parse(_line));
            _lowStandardStep = PITCH_NAME[(temp + standard) % 7];
            _lowStandardOctave = standard.ToString();
        }

        private void SetShift(Note note, string standardStep, string standardOctave)
        {
            ParamsGetter paramsGetter = ParamsGetter.GetInstance();
            int shift = -(GetDigitizedPitch(standardStep, standardOctave) - GetDigitizedPitch(_step, _octave)) *
                        paramsGetter.GetPitchPositionDiff();
            note.SetShift(shift);
        }

        private int GetDigitizedPitch(string step, string octave)
        {
            int digitizedPitch = 1;

            switch (step)
            {
                case "C": digitizedPitch = 1; break;
                case "D": digitizedPitch = 2; break;
                case "E": digitizedPitch = 3; break;
                case "F": digitizedPitch = 4; break;
                case "G": digitizedPitch = 5; break;
                case "A": digitizedPitch = 6; break;
                case "B": digitizedPitch = 7; break;
            }

            return digitizedPitch + (int.Parse(octave) - 1) * 7;
        }

        private void SetChord(List<Symbol> symbolList)
        {
            Note lastNote = (Note) symbolList[symbolList.Count - 1];
            lastNote.SetHasChord(true);
            lastNote.GetChordList().Add((Note) _symbol);
        }

        private void SetBeam(List<Symbol> symbolList)
        {
            switch (_beam)
            {
                case "begin":
                {
                    _slur = new Slur();
                    _slur.GetList().Add((Note) _symbol);
                    ((Note) _symbol).SetSlur(true);
                    ((Note) _symbol).SetNext(true);
                }
                    break;
                case "continue":
                {
                    _slur.GetList().Add((Note) _symbol);
                    ((Note) _symbol).SetLastNote((Note) symbolList[symbolList.Count - 1]);
                    ((Note) _symbol).SetSlur(true);
//                    ((Note) _symbol).SetLast(true);
                    ((Note) _symbol).SetNext(true);
                }
                    break;
                case "end":
                {
                    Note lastNote = (Note) symbolList[symbolList.Count - 1];
                    _slur.GetList().Add((Note) _symbol);
                    ((Note) _symbol).SetLastNote(lastNote);
                    ((Note) _symbol).SetSlur(true);
                    ((Note) _symbol).SetLast(true);

                    if (_slur != null)
                    {
                        _slur.Operate();
                    }
                }
                    break;
                default: break;
            }

            ((Note) _symbol).SetBeam(_beam);
            _beam = "";
        }

        // 将小节中按照音符时长分组，返回整个小节的总duration
        private int ArrangeMeasure()
        {
            int i = 0;
            int j = 0;
            int tempHighDuration = 0;
            int tempLowDuration = 0;
            while (i < _highSymbolMeasure.Count && j < _lowSymbolMeasure.Count)
            {
                List<List<Symbol>> setList = new List<List<Symbol>>();
                List<Symbol> highList = new List<Symbol>();
                List<Symbol> lowList = new List<Symbol>();

                highList.Add(_highSymbolMeasure[i]);
                tempHighDuration += _highSymbolMeasure[i].GetDuration();
                i++;
                lowList.Add(_lowSymbolMeasure[j]);
                tempLowDuration += _lowSymbolMeasure[j].GetDuration();
                j++;

                while (tempHighDuration != tempLowDuration)
                {
                    if (tempHighDuration > tempLowDuration)
                    {
                        lowList.Add(_lowSymbolMeasure[j]);
                        tempLowDuration += _lowSymbolMeasure[j].GetDuration();
                        j++;
                    }
                    else if (tempHighDuration < tempLowDuration)
                    {
                        highList.Add(_highSymbolMeasure[i]);
                        tempHighDuration += _highSymbolMeasure[i].GetDuration();
                        i++;
                    }
                }
                setList.Add(highList);
                setList.Add(lowList);
                _measureSymbolList.Add(setList);
            }
            return i >= j ? i : j;
        }
    }
}