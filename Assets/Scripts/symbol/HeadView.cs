using util;
using UnityEngine;
using UnityEngine.UI;

namespace symbol
{
    public class HeadView
    {
        private Head _head;
        private GameObject _parentObject;
        private ParamsGetter _paramsGetter = ParamsGetter.GetInstance();
        private CommonParams _commonParams = CommonParams.GetInstance();

        public HeadView(Head head, GameObject parentObject)
        {
            _head = head;
            _parentObject = parentObject;
            Init();
        }

        private void Init()
        {
            OnDraw();
        }

        private void OnDraw()
        {
            int shift = 0; // 谱号偏移，如果是高音谱号偏移整个五线谱长度
            switch (_head.GetSign())
            {
                case "G": // 高音符号G
                    shift = _paramsGetter.GetTotalHeight();
                    DrawSymbol("\uE19E", _paramsGetter.GetClefPortraitShift(), _paramsGetter.GetStaffCenterPosition() + shift);
                    break;
                case "F": // 低音符号F
                    shift = 0;
                    DrawSymbol("\uE19C", _paramsGetter.GetClefPortraitShift(), _paramsGetter.GetStaffCenterPosition() + shift);
                    break;
                default: break;
            }

            float first = _paramsGetter.GetFirstFifthsPosition();
            float second = _paramsGetter.GetSecondFifthsPosition();
            switch (_head.GetFifths()) {
                case "2":
                {
                    DrawSymbol("\uE10E", first, _paramsGetter.GetStaffPosition() + shift); // #
                    DrawSymbol("\uE10E", second, _paramsGetter.GetStaffCenterPosition() + shift);
                }
                    break;
                case "1": DrawSymbol("\uE10E", first, _paramsGetter.GetStaffPosition() + shift); break;
                case "-1": DrawSymbol("\uE114", first, _paramsGetter.GetStaffCenterPosition() + shift); break; // B
                default: break;
            }
        }

        // 绘制乐符文字
        private void DrawSymbol(string text, float x, float y)
        {
            GameObject mySymbol = GameObject.Instantiate(_commonParams.GetPrefabSymbol(),
                _parentObject.transform.position,
                _commonParams.GetPrefabSymbol().transform.rotation);
            mySymbol.transform.SetParent(_parentObject.transform);
            Text noteText = mySymbol.GetComponent<Text>();
            noteText.transform.localPosition = new Vector3(x, y, 0);
            noteText.text = text;
        }
    }
}