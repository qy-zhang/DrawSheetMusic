using System;
using UnityEngine;
using UnityEngine.UI;

namespace symbol
{
    public class NoteView : SymbolView
    {
        private Note _note;
        private int _stemX;  //  符干起始横坐标
        private int _stemY;  //  符干起始纵坐标
        private int _beamX;  //  符杠起始横坐标
        private int _beamY;  //  符杠起始纵坐标
        private int _tailX;  //  符尾起始横坐标
        private int _tailY;  //  符尾起始纵坐标

        public NoteView(Symbol symbol, int width, int start, GameObject[] paramObjects) : base(symbol, width, start, paramObjects)
        {
            _note = (Note)symbol;
            Init();
        }

        private void Init()
        {
            OnDraw();
        }

        protected override void OnDraw()
        {
            // 绘制定位线
            if (Cursor)
            {
                int tempStart = Start - 2 + Num * _note.GetSymbolWidth();
                DrawLine(tempStart, 0, tempStart, ParamsGetter.GetTotalHeight());
            }

            // 乐符纵坐标
            int yPosition =  ParamsGetter.GetStaffCenterPosition() + _note.GetShift();

            int shift = _note.GetShift() / ParamsGetter.GetPitchPositionDiff();
            // 偏移线，当超过6的时候会画线。传入参数为shift，因为以左下角为原点
            DrawShiftLine(shift);
            // 绘制变音记号
            DrawAccidental(_note.GetAccidental(), yPosition);

            if (_note.GetDot() == 1)
            {
                DrawPoint(Start + ParamsGetter.GetDotePosition(), yPosition);
            }

            if (Type == 1) // 全音符
            {
                DrawSymbol("\uE12B", Start, yPosition);
                if (_note.HasChord()) DrawChord();
            }
            else
            {
                if (Type == 2) // 二分之一音符
                    DrawSymbol("\uE12C", Start, yPosition);
                else // 其余的所有
                    DrawSymbol("\uE12D", Start, yPosition);

                if (_note.IsUpOrDown())
                {
                    _stemX = Start + ParamsGetter.GetNoteRightShift(); // 符干向上时横坐标向右偏移
                    _stemY = yPosition; // 符干向上时纵坐标与乐符中心纵坐标位置相同
                    _tailX = Start + ParamsGetter.GetNoteTailUpLandscapeShift();
                    _tailY = _stemY + ParamsGetter.GetNoteStemHeight() - ParamsGetter.GetNoteTailDownPortraitShift();
                }
                else
                {
                    _stemX = Start - ParamsGetter.GetNoteLeftShift(); // 符干向下时横坐标向左偏移
                    _stemY = yPosition - ParamsGetter.GetNoteStemDownShift(); // 符干向下时纵坐标与乐符中心纵坐标位置偏移
                    _tailX = Start - ParamsGetter.GetNoteTailDownLandscapeShift();
                    _tailY = _stemY - ParamsGetter.GetNoteStemHeight() + ParamsGetter.GetNoteTailDownPortraitShift();
                }

                _beamX = _stemX;
                _beamY = _note.GetEnd();

                if (_note.HasChord())
                {
                    if (_note.IsUpOrDown())
                    {
                        int temp = _note.GetShift();
                        foreach (Note noteChord in _note.GetChordList())
                        {
                            if (temp > noteChord.GetShift())
                            {
                                temp = noteChord.GetShift();
                            }
                        }
                        _tailY = temp + ParamsGetter.GetNoteStemHeight();
                    }
                    DrawChord();
                }
                DrawStem(); // 画符干
                if (_note.IsSlur())
                {
                    DrawBeam(); // 画符杠
                }
                else
                {
                    DrawTail();
                }
            }
        }

        // 绘制偏移线
        private void DrawShiftLine(int shift)
        {
            if (shift > 4)
            {
                int num = (shift - 4) / 2 + 1;
                for (int i = 1; i < num; i++)
                {
                    int y = ParamsGetter.GetStaffCenterPosition() + (4 + 2 * i) * ParamsGetter.GetPitchPositionDiff();
                    DrawLine(Start - 4 - ParamsGetter.GetNoteLeftShift(), y, Start + 4 + ParamsGetter.GetNoteRightShift(), y);
                }
            }
            else if (shift < -4)
            {
                int num = (-4 - shift) / 2 + 1;
                for (int i = 1; i < num; i ++)
                {
                    int y = ParamsGetter.GetStaffCenterPosition() - (4 + 2 * i) * ParamsGetter.GetPitchPositionDiff();
                    DrawLine(Start - 4 - ParamsGetter.GetNoteLeftShift(), y, Start + 4 + ParamsGetter.GetNoteRightShift(), y);
                }
            }
        }

        // 绘制变音记号
        private void DrawAccidental(string accidental, int position)
        {
            switch (accidental)
            {
                case "flat": DrawSymbol("\uE114", 0, position); break;
                case "sharp": DrawSymbol("\uE10E", 0, position); break;
                case "natural": DrawSymbol("\uE113", 0, position); break;
                default: break;
            }
        }

        // 绘制符干
        private void DrawStem()
        {
            if (_note.IsSlur())
            {
                DrawLine(_stemX, _stemY, _stemX, _beamY);
            }
            else {
                if (_note.IsUpOrDown()) {
                    DrawLine(_stemX, _stemY, _stemX, _stemY + ParamsGetter.GetNoteStemHeight());
                } else {
                    DrawLine(_stemX, _stemY, _stemX, _stemY - ParamsGetter.GetNoteStemHeight());
                }
            }
        }

        // 绘制符杠
        private void DrawBeam()
        {
            // 绘制符杠，根据之前计算出来的起始x坐标和起始y坐标，画到下一个连音符的x坐标，划的长度为乐符之间的间隔width
            // 两条符杠之间的间隔
            int beamDiff = -ParamsGetter.GetNoteBeamDiff(_note.IsUpOrDown());
            if (_note.IsLast())
            {
                switch (_note.GetLastNote().GetType())
                {
                    case 8:
                        DrawLine(_beamX, _beamY, 0, _beamY, 2);
                        break;
                    case 16:
                        DrawLine(_beamX, _beamY, 0, _beamY, 2);
                        DrawLine(_beamX, _beamY + beamDiff, 0, _beamY + beamDiff, 2);
                        break;
                    case 64:
                        DrawLine(_beamX, _beamY, 0, _beamY, 2);
                        DrawLine(_beamX, _beamY + beamDiff, 0, _beamY + beamDiff, 2);
                        DrawLine(_beamX, _beamY + 2 * beamDiff, 0, _beamY + 2 * beamDiff, 2);
                        break;
                    default: break;
                }

                if (_note.GetLastNote().GetDot() != 0)
                {
                    switch ((int) (_note.GetLastNote().GetType() * Math.Pow(2, _note.GetLastNote().GetDot())))
                    {
                        case 16:
                            DrawLine(_beamX, _beamY + beamDiff, 0, _beamY + beamDiff, 2);
                            break;
                        case 32:
                            DrawLine(_beamX, _beamY + beamDiff, 0, _beamY + beamDiff, 2);
                            DrawLine(_beamX, _beamY + 2 * beamDiff, 0, _beamY + 2 * beamDiff, 2);
                            break;
                        default: break;
                    }
                }
            }

            if (_note.IsNext())
            {
                switch (_note.GetType())
                {
                    case 8:
                        DrawLine(_beamX - 1, _beamY, Width, _beamY, 2);
                        break;
                    case 16:
                        DrawLine(_beamX - 1, _beamY, Width, _beamY, 2);
                        DrawLine(_beamX - 1, _beamY + beamDiff, Width, _beamY + beamDiff, 2);
                        break;
                    case 32:
                        DrawLine(_beamX - 1, _beamY, Width, _beamY, 2);
                        DrawLine(_beamX - 1, _beamY + beamDiff, Width, _beamY + beamDiff, 2);
                        DrawLine(_beamX - 1, _beamY + 2 * beamDiff, Width, _beamY + 2 * beamDiff, 2);
                        break;
                    default: break;
                }

                if (_note.GetDot() != 0) {
                    int temp = (int) Math.Pow(2, _note.GetDot());
                    int start = (2 * temp - 1) * Width / (2 * temp);
                    switch (_note.GetType() * temp) {
                        case 16:
                            DrawLine(start, _beamY + beamDiff, Width, _beamY + beamDiff, 2);
                            break;
                        case 32:
                            DrawLine(start, _beamY + beamDiff, Width, _beamY + beamDiff, 2);
                            DrawLine(start, _beamY + 2 * beamDiff, Width, _beamY + 2 * beamDiff, 2);
                            break;
                        default: break;
                    }
                }
            }
        }

        // 绘制符尾
        private void DrawTail()
        {
            if (_note.IsUpOrDown())
            {
                switch (Type)
                {
                    case 8: DrawSymbol("\uE190", _tailX, _tailY); break;
                    case 16: DrawSymbol("\uE191", _tailX, _tailY); break;
                    case 32: DrawSymbol("\uE192", _tailX, _tailY); break;
                    case 64: DrawSymbol("\uE193", _tailX, _tailY); break;
                    default: break;
                }
            }
            else
            {
                switch (Type)
                {
                    case 8: DrawSymbol("\uE194", _tailX, _tailY); break;
                    case 16: DrawSymbol("\uE197", _tailX, _tailY); break;
                    case 32: DrawSymbol("\uE198", _tailX, _tailY); break;
                    case 64: DrawSymbol("\uE199", _tailX, _tailY); break;
                    default: break;
                }
            }
        }

        // 绘制和弦
        private void DrawChord()
        {
            bool leftOrRight = _note.IsUpOrDown();
            int headPosition = Start;
            int lastPosition = 0;
//            int chordPaintNum = 0;
            // 绘制和弦中的其他乐符（最后一个乐符即当前乐符，已经绘制），去掉和弦表中的最后一个乐符
            for (int i = 0; i < _note.GetChordList().Count - 1; i++)
            {
                Note extraNote = _note.GetChordList()[i];
                DrawShiftLine(extraNote.GetShift() / ParamsGetter.GetPitchPositionDiff());
                int extraDuration = extraNote.GetType();
                int extraPosition = extraNote.GetShift() + ParamsGetter.GetStaffCenterPosition();
                if (Math.Abs(extraPosition - lastPosition) == ParamsGetter.GetPitchPositionDiff())
                {
                    if (leftOrRight)
                        headPosition += ParamsGetter.GetNoteHeadWidth();
                    else
                        headPosition -= ParamsGetter.GetNoteHeadWidth();
                    leftOrRight = !leftOrRight;
                }

                if (extraDuration == 1) // 全音符
                {
                    DrawSymbol("\uE12B", headPosition, extraPosition);
                    continue;
                }
                else
                {
                    if (extraDuration == 2)
                        DrawSymbol("\uE12C", headPosition, extraPosition);
                    else
                        DrawSymbol("\uE12D", headPosition, extraPosition);
                }

                lastPosition = extraPosition;
                int temp;
                if (_note.IsUpOrDown())
                    temp = extraPosition + ParamsGetter.GetNoteStemHeight();
                else
                    temp = extraPosition - ParamsGetter.GetNoteStemHeight();
                DrawLine(_stemX, extraPosition, _stemX, temp);
            }
        }
    }
}