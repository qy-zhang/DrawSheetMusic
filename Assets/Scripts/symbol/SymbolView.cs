using System;
using System.Collections.Generic;
using util;
using UnityEngine;
using UnityEngine.UI;

namespace symbol
{
    public abstract class SymbolView
    {
        protected Symbol symbol;
        protected int Width; // 符号视图宽度
        protected int Start; // 符号起始横坐标
        protected int Type; // 符号类型
        protected Color color; // 画笔颜色
        protected int Num = 0; // 定位线位置
        protected int ChordPlay = -1; // 演奏正确的和弦
        protected bool Cursor = false; // 播放定位线
        protected bool RightPlay; // 是否已正确演奏
        protected List<int> Chord = new List<int>(); // 演奏正确的和弦
        protected ParamsGetter ParamsGetter = ParamsGetter.GetInstance();

        protected GameObject[] ParamObjects;
        protected GameObject ParentObject; // 对象的父对象
        protected GameObject PrefabSymbol; // 音符对象的实例
        protected GameObject PrefabLine; // 线段对象的实例


        public SymbolView() {}

        public SymbolView(Symbol symbol, int width, int start, GameObject[] paramObjects) {
            this.symbol = symbol;
            Width = width;
            Start = start;
            ParamObjects = paramObjects;
            ParentObject = ParamObjects[0];
            PrefabSymbol = ParamObjects[1];
            PrefabLine = ParamObjects[2];
            Init();
        }

        private void Init()
        {
            Type = symbol.GetType();
        }

        // 更改整个乐符对象的颜色
        public void SetColor(Color color)
        {
            // 遍历整个乐符对象，修改CanvasRenderer的颜色属性
            foreach (Transform childTransform in ParentObject.transform)
            {
                // 当乐符对象是划线的时候，使用的是image，修改颜色先找到image组件，然后设置颜色
                CanvasRenderer canvasRenderer = childTransform.gameObject.GetComponent<CanvasRenderer>();
                Image image = canvasRenderer.GetComponent<Image>();
                if (image != null)
                {
                    image.color = color;
                    continue; // 已经确定是划线，后面的不需要执行，直接判断下一个乐符对象
                }
                // 当乐符对象是音符的时候，使用的是text，修改颜色先找到text组件，然后设置颜色
                Text text = canvasRenderer.GetComponent<Text>();
                if (text != null)
                {
                    text.color = color;
                    continue;
                }
            }
        }

        public void SetCursor(int num, bool cursor) { Num = num; Cursor = cursor; }

        public void SetChord(List<int> chord) { Chord = chord; }

        public bool IsRightPlay() { return RightPlay; }

        public void SetRightPlay(bool rightPlay) { RightPlay = rightPlay; }


        // 绘制乐符文字
        protected void DrawSymbol(string text, float x, float y)
        {
            GameObject symbolObject = GameObject.Instantiate(PrefabSymbol, ParentObject.transform.position, PrefabSymbol.transform.rotation);
            symbolObject.transform.SetParent(ParentObject.transform);
            Text noteText = symbolObject.GetComponent<Text>();
            noteText.transform.localPosition = new Vector3(x, y, 0);
            noteText.text = text;
        }

        // 绘制线段
        protected void DrawLine(float startX, float startY, float stopX, float stopY)
        {
            // 实例化一个线段对象
            GameObject lineObject = GameObject.Instantiate(PrefabLine, ParentObject.transform.position, PrefabLine.transform.rotation);
            lineObject.transform.SetParent(ParentObject.transform);
            RectTransform lineRect = lineObject.GetComponent<RectTransform>();
            float width = Math.Max(startX, stopX) - Math.Min(startX, stopX) + 1;
            float heigh = Math.Max(startY, stopY) - Math.Min(startY, stopY) + 1;
            lineRect.sizeDelta = new Vector2(width, heigh); // 设置线段长宽
            // 设置线段位置，从最小画到最大
            lineRect.localPosition = new Vector3(Math.Min(startX, stopX), Math.Min(startY, stopY), 0);
        }

        // 重载DrawLine函数，绘制线段，通过strokeWidth设置线段粗细
        protected void DrawLine(float startX, float startY, float stopX, float stopY, int strokeWidth)
        {
            // 实例化一个线段对象
            GameObject lineObject = GameObject.Instantiate(PrefabLine, ParentObject.transform.position, PrefabLine.transform.rotation);
            lineObject.transform.SetParent(ParentObject.transform);
            RectTransform lineRect = lineObject.GetComponent<RectTransform>();
            float width = Math.Max(startX, stopX) - Math.Min(startX, stopX) + strokeWidth;
            float heigh = Math.Max(startY, stopY) - Math.Min(startY, stopY) + strokeWidth;
            lineRect.sizeDelta = new Vector2(width, heigh); // 设置线段长宽
            // 设置线段位置，从最小画到最大
            lineRect.localPosition = new Vector3(Math.Min(startX, stopX), Math.Min(startY, stopY), 0);
        }

        protected void DrawPoint(float x, float y)
        {

        }

        protected abstract void OnDraw();
    }
}
