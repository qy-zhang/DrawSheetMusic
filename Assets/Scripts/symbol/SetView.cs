using System;
using System.Collections.Generic;
using util;
using UnityEngine;

namespace symbol
{
    public class SetView
    {
        private List<List<Symbol>> _setList;
        private List<Symbol> _highSymbolList;
        private List<Symbol> _lowSymbolList;
        private GameObject[] _paramObject;
        private float _setLength;
        private ParamsGetter _paramsGetter = ParamsGetter.GetInstance();

        public SetView(List<List<Symbol>> setList, GameObject[] paramObject, float setLength)
        {
            _setList = setList;
            _paramObject = paramObject;
            _highSymbolList = setList[0];
            _lowSymbolList = setList[1];
            _setLength = setLength;
            Init();
        }

        private void Init()
        {
            OnDraw();
        }

        private void OnDraw()
        {
            Vector3 notePosition = Vector3.zero;
            float noteLength = _setLength;
            int maxCount = Math.Max(_highSymbolList.Count, _lowSymbolList.Count);
            if (_setList.Count != 0)
            {
                noteLength = _setLength / maxCount;
            }

            for (int i = 0; i < _highSymbolList.Count; i++)
            {
                // 新建HighNote对象作为目录
                string objName = "HighNote" + (i + 1);
                GameObject highNoteObj = new GameObject(objName);
                highNoteObj.transform.SetParent(_paramObject[0].transform);
                highNoteObj.transform.localPosition = new Vector3(
                    notePosition.x + _paramsGetter.GetBeatWidth() + noteLength * i,
                    notePosition.y + _paramsGetter.GetTotalHeight(),
                    notePosition.z);

                // 将Set对象赋为下一层的父对象
                GameObject[] paramObject = new GameObject[3];
                paramObject[0] = highNoteObj; paramObject[1] = _paramObject[1]; paramObject[2] = _paramObject[2];

                if (_highSymbolList[i] is Note)
                {
                    NoteView noteView = new NoteView(_highSymbolList[i], (int)noteLength, _paramsGetter.GetSymbolStart(), paramObject);
                    _highSymbolList[i].SetSymbolView(noteView);
                }
                else if (_highSymbolList[i] is Rest)
                {
                    RestView restView = new RestView(_highSymbolList[i], (int)noteLength, _paramsGetter.GetSymbolStart(), paramObject);
                    _highSymbolList[i].SetSymbolView(restView);
                }
            }
            for (int i = 0; i < _lowSymbolList.Count; i++)
            {
                // 新建LowNote对象作为目录
                string objName = "LowNote" + (i + 1);
                GameObject lowNoteObj = new GameObject(objName);
                lowNoteObj.transform.SetParent(_paramObject[0].transform);
                lowNoteObj.transform.localPosition = new Vector3(
                    notePosition.x + _paramsGetter.GetBeatWidth() + noteLength * i,
                    notePosition.y,
                    notePosition.z);

                // 将Set对象赋为下一层的父对象
                GameObject[] paramObject = new GameObject[3];
                paramObject[0] = lowNoteObj; paramObject[1] = _paramObject[1]; paramObject[2] = _paramObject[2];

                if (_lowSymbolList[i] is Note)
                {
                    NoteView noteView = new NoteView(_lowSymbolList[i], (int)noteLength, _paramsGetter.GetSymbolStart(), paramObject);
                    _lowSymbolList[i].SetSymbolView(noteView);
                }
                else if (_lowSymbolList[i] is Rest)
                {
                    RestView restView = new RestView(_lowSymbolList[i], (int)noteLength, _paramsGetter.GetSymbolStart(), paramObject);
                    _lowSymbolList[i].SetSymbolView(restView);
                }
            }
        }
    }
}