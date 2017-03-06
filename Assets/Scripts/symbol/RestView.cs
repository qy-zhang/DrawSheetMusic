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
            int yPosition = ParamsGetter.GetStaffCenterPosition(); // 乐符的位置


//            if (_rest.GetDot() == 1)
//                noteText.transform.localPosition = new Vector3(Start + ParamsGetter.GetDotePosition(), yPosition, 0);
//            else
//                noteText.transform.localPosition = new Vector3(Start, yPosition, 0);
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