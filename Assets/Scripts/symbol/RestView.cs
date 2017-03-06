using UnityEngine;
using UnityEngine.UI;

namespace symbol
{
    public class RestView : SymbolView
    {
        private Rest _rest;
        public RestView(Symbol symbol, int width, int start, GameObject[] paramObjects) : base(symbol, width, start, paramObjects)
        {
            _rest = (Rest) symbol;
            Init();
        }

        private void Init()
        {
            OnDraw();
        }

        protected override void OnDraw()
        {
            // 绘制定位光标
            if (Cursor)
            {
                int tempStart = Start - 2 + Num * _rest.GetSymbolWidth();
                DrawLine(tempStart, 0, tempStart, ParamsGetter.GetTotalHeight());
            }

            // 乐符的位置
            int yPosition = ParamsGetter.GetStaffCenterPosition();

            // 如果是点，绘制
            if (_rest.GetDot() == 1)
            {
                DrawPoint(Start + ParamsGetter.GetDotePosition(), yPosition);
            }

            // 乐符的内容
            switch (Type)
            {
                case 1: DrawSymbol("\uE100", Start, yPosition); break;
                case 2: DrawSymbol("\uE101", Start, yPosition); break;
                case 4: DrawSymbol("\uE107", Start, yPosition); break;
                case 8: DrawSymbol("\uE109", Start, yPosition); break;
                case 16: DrawSymbol("\uE10A", Start, yPosition); break;
                case 32: DrawSymbol("\uE10B", Start, yPosition); break;
                case 64: DrawSymbol("\uE10C", Start, yPosition); break;
                case 128: DrawSymbol("\uE10D", Start, yPosition); break;
                default: break;
            }
        }
    }
}