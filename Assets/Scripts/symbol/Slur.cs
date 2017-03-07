using System;
using System.Collections.Generic;
using util;

namespace symbol
{
    public class Slur
    {
        private List<Note> _list = new List<Note>();
        private ParamsGetter _paramsGetter = ParamsGetter.GetInstance();

        public List<Note> GetList() {
            return _list;
        }

        public void Operate()
        {
            int temp;
            Boolean upOrDown = _list[0].GetShift() < 0; // 从五线谱中心往下偏移，说明符干朝上
            int stemEnd;
            if (upOrDown)
            {
                temp = GetHighestNote();
                stemEnd = temp + _paramsGetter.GetStaffCenterPosition() + _paramsGetter.GetNoteStemHeight();
            }
            else
            {
                temp = GetLowestNote();
                stemEnd = temp + _paramsGetter.GetStaffCenterPosition() - _paramsGetter.GetNoteStemHeight();
            }

            foreach (Note note in _list)
            {
                note.SetEnd(stemEnd);
                note.SetUpOrDown(upOrDown);
            }
        }

        private int GetHighestNote()
        {
            int temp = _list[0].GetShift();
            foreach (Note note in _list)
            {
                if (note.HasChord())
                {
                    foreach (Note noteChord in note.GetChordList())
                    {
                        if (noteChord.GetShift() > temp)
                        {
                            temp = noteChord.GetShift();
                        }
                    }
                }
                if (note.GetShift() > temp)
                {
                    temp = note.GetShift();
                }
            }
            return temp;
        }

        private int GetLowestNote()
        {
            int temp = _list[0].GetShift();
            foreach (Note note in _list)
            {
                if (note.HasChord())
                {
                    foreach (Note noteChord in note.GetChordList())
                    {
                        if (noteChord.GetShift() < temp)
                        {
                            temp = noteChord.GetShift();
                        }
                    }
                }
                if (note.GetShift() < temp)
                {
                    temp = note.GetShift();
                }
            }
            return temp;
        }
    }
}