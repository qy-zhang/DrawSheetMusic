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
        protected int Width;    //  符号视图宽度
        protected int Start;    //  符号起始横坐标
        protected int Type;     //  符号类型
//        protected Color Color;   //  画笔颜色
//        protected int Num = 0;                              //  定位线位置
//    protected int chordPlay = -1;                        //  演奏正确的和弦
//        protected bool Cursor = false;                   //  播放定位线
        protected bool RightPlay;                //  是否已正确演奏
        protected List<int> Chord = new List<int>();  //  演奏正确的和弦
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
            Init();
        }

        private void Init()
        {
            ParentObject = ParamObjects[0];
            PrefabSymbol = ParamObjects[1];
            PrefabLine = ParamObjects[2];
            Type = symbol.GetType();
        }
//        public void SetColor(Color color) {
//            Color = color;
//        }

        public void SetChord(List<int> chord) { Chord = chord; }

//        public bool IsRightPlay() { return RightPlay; }

//        public void SetRightPlay(bool rightPlay) { RightPlay = rightPlay; }

        // 绘制乐符文字
        protected void DrawSymbol(string text, float x, float y)
        {
            GameObject mySymbol = GameObject.Instantiate(PrefabSymbol, ParentObject.transform.position, PrefabSymbol.transform.rotation);
            mySymbol.transform.SetParent(ParentObject.transform);
            Text noteText = mySymbol.GetComponent<Text>();
            noteText.transform.localPosition = new Vector3(x, y, 0);
            noteText.text = text;
        }

        // 绘制线段
        protected void DrawLine(float startX, float startY, float stopX, float stopY)
        {
            // 实例化一个线段对象
            GameObject myLine = GameObject.Instantiate(PrefabLine, ParentObject.transform.position, PrefabLine.transform.rotation);
            myLine.transform.SetParent(ParentObject.transform);
            RectTransform lineRect = myLine.GetComponent<RectTransform>();
            float width = Math.Max(startX, stopX) - Math.Min(startX, stopX) + 1;
            float heigh = Math.Max(startY, stopY) - Math.Min(startY, stopY) + 1;
            lineRect.sizeDelta = new Vector2(width, heigh); // 设置线段长宽
            // 设置线段位置，从最小画到最大
            lineRect.localPosition = new Vector3(Math.Min(startX, stopX), Math.Min(startY, stopY), 0);
        }

        protected void DrawLine(float startX, float startY, float stopX, float stopY, int strokeWidth)
        {
            // 实例化一个线段对象
            GameObject myLine = GameObject.Instantiate(PrefabLine, ParentObject.transform.position, PrefabLine.transform.rotation);
            myLine.transform.SetParent(ParentObject.transform);
            RectTransform lineRect = myLine.GetComponent<RectTransform>();
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
