using symbol;
using UnityEngine;

namespace control
{
    public class SymbolControl
    {
        private Symbol _symbol;
        public SymbolControl(Symbol symbol)
        {
            _symbol = symbol;
        }

        // 设置乐符颜色
        public void SetColor(Color color)
        {
            _symbol.ChangeColor(color);
        }

        // 乐符对象弹跳动画
        public void Rebound()
        {

        }
    }
}