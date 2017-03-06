using util;
using UnityEngine;
using UnityEngine.UI;

namespace symbol
{
    public class BeatView
    {
        private Beat _beat;
        private GameObject[] _paramObjects;
        private ParamsGetter _paramsGetter = ParamsGetter.GetInstance();

        public BeatView(Beat beat, GameObject[] paramObjects)
        {
            _beat = beat;
            _paramObjects = paramObjects;
            Init();
        }

        private void Init()
        {
            OnDraw();
        }

        private void OnDraw()
        {
            DrawText(_beat.GetBeats(), _paramsGetter.GetBeatPortraitShift(), _paramsGetter.GetStaffCenterPosition() + _paramsGetter.GetUnit() + _paramsGetter.GetTotalHeight());
            DrawText(_beat.GetBeatType(), _paramsGetter.GetBeatPortraitShift(), _paramsGetter.GetStaffCenterPosition() - _paramsGetter.GetUnit() + _paramsGetter.GetTotalHeight());
        }

        // 绘制乐符文字
        protected void DrawText(string text, float x, float y)
        {
            GameObject mySymbol = GameObject.Instantiate(_paramObjects[1], _paramObjects[0].transform.position, _paramObjects[1].transform.rotation);
            mySymbol.transform.SetParent(_paramObjects[0].transform);
            Text noteText = mySymbol.GetComponent<Text>();
            noteText.transform.localPosition = new Vector3(x, y, 0);
            noteText.text = text;
        }
    }
}