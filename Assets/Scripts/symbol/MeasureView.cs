using System;
using System.Collections.Generic;
using util;
using UnityEngine;
using UnityEngine.UI;

namespace symbol
{
    public class MeasureView
    {
        private Measure _measure;
        private GameObject[] _paramObject;
        private GameObject _parentObject;
        private GameObject _prefabLine;
        private GameObject _measureLines;
        private ParamsGetter _paramsGetter = ParamsGetter.GetInstance();

        public MeasureView(Measure measure, GameObject[] paramObject)
        {
            _measure = measure;

            _paramObject = paramObject;
            _parentObject = paramObject[0];
            _prefabLine = paramObject[2];

            Init();
        }

        private void Init()
        {
            OnDraw();
        }

        private void OnDraw()
        {
            // 绘制小节的五线谱
            DrawMeasureLines();
            int shift = 0; // 如果有头部信息的偏移
            // 如果有谱号信息，绘制
            if (_measure.HasHead())
            {
                DrawClef();
                shift += _paramsGetter.GetHeadWidth();
            }
            // 如果有拍号信息，绘制
            if (_measure.HasBeat())
            {
                DrawBeat();
                shift += _paramsGetter.GetBeatWidth();
            }

            // 每小节的长度
            float setLength = _measure.GetMeasureLength() - shift;
            Vector3 setPosition = Vector3.zero;
            if (_measure.GetMeasureSymbolList().Count != 0)
            {
                setLength /= _measure.GetMeasureSymbolList().Count;
            }

            // 遍历一个小节中的所有组队，绘制每个组队
            for (int i = 0; i < _measure.GetMeasureSymbolList().Count; i++)
            {
                // 新建Set对象作为目录
                string objName = "Set" + (i + 1);
                GameObject setObject = new GameObject(objName);
                setObject.transform.SetParent(_paramObject[0].transform);
                setObject.transform.localPosition = new Vector3(setPosition.x + setLength * i + shift,
                    setPosition.y, setPosition.z);

                // 将Set对象赋为下一层的父对象
                GameObject[] paramObject = new GameObject[4];
                paramObject[0] = setObject; paramObject[1] = _paramObject[1]; paramObject[2] = _paramObject[2];

                // 绘制Set视图
                SetView setView = new SetView(_measure.GetMeasureSymbolList()[i], paramObject, setLength);
            }
        }

        // 绘制一行乐谱线，根据小节数来绘制
        private void DrawMeasureLines()
        {
            // 为线段构建一个Lines目录放置
            _measureLines = new GameObject("Lines");
            _measureLines.transform.SetParent(_parentObject.transform);
            _measureLines.transform.localPosition = new Vector3(0, 0, 0);

            int measureLength = _measure.GetMeasureLength();

            // 绘制高音五条横乐谱线
            for (int i = 0; i < 5; i++)
            {
                // 横线的纵坐标固定，第三条线(j==2)在乐谱中心位置
                float yPosition = _paramsGetter.GetTotalHeight() + _paramsGetter.GetStaffCenterPosition() + (i - 2) * _paramsGetter.GetUnit();
                // 设置线段宽高，横线，宽度为小节长度，高度为1
                DrawLine(0, yPosition, measureLength, yPosition);
            }
            // 绘制每个高音小节的两条竖线
            for (int i = 0; i < 2; i++)
            {
                // 竖线的纵坐标起点为五条横线的最下面一条线的纵坐标
                float startY = _paramsGetter.GetTotalHeight() + _paramsGetter.GetStaffCenterPosition() - 2 * _paramsGetter.GetUnit();
                float stopY = _paramsGetter.GetTotalHeight()+ _paramsGetter.GetStaffCenterPosition() + 2 * _paramsGetter.GetUnit();
                // 设置线段宽高，竖线，宽度为1，高度五线谱高度
                DrawLine(measureLength * i, startY, measureLength * i, stopY);
            }

            // 绘制低音五条横乐谱线
            for (int i = 0; i < 5; i++)
            {
                // 横线的纵坐标固定，第三条线(j==2)在乐谱中心位置
                float yPosition = _paramsGetter.GetTotalHeight() - _paramsGetter.GetStaffCenterPosition() + (i - 2) * _paramsGetter.GetUnit();
                // 设置线段宽高，横线，宽度为小节长度，高度为1
                DrawLine(0, yPosition, measureLength, yPosition);
            }
            // 绘制每个低音小节的两条竖线
            for (int i = 0; i < 2; i++)
            {
                // 竖线的纵坐标起点为五条横线的最下面一条线的纵坐标
                float startY = _paramsGetter.GetTotalHeight() - _paramsGetter.GetStaffCenterPosition() - 2 * _paramsGetter.GetUnit();
                float stopY = _paramsGetter.GetTotalHeight() - _paramsGetter.GetStaffCenterPosition() + 2 * _paramsGetter.GetUnit();
                // 设置线段宽高，竖线，宽度为1，高度五线谱高度
                DrawLine(measureLength * i, startY, measureLength * i, stopY);
            }
        }

        private void DrawClef()
        {
            // 绘制高音符号
            GameObject highHeadObject = new GameObject("HighHead");
            highHeadObject.transform.SetParent(_parentObject.transform);
            highHeadObject.transform.localPosition = Vector3.zero;
            Head highHead = _measure.GetHead()[0];
            // 将highHead对象对象赋为下一层的父对象
            GameObject[] paramObject = new GameObject[3];
            paramObject[0] = highHeadObject; paramObject[1] = _paramObject[1]; paramObject[2] = _paramObject[2];
            HeadView highHeadView = new HeadView(highHead, paramObject);

            // 绘制低音符号
            GameObject lowHeadObject = new GameObject("LowHead");
            lowHeadObject.transform.SetParent(_parentObject.transform);
            lowHeadObject.transform.localPosition = Vector3.zero;
            Head lowHead = _measure.GetHead()[1];
            // 将highHead对象对象赋为下一层的父对象
            paramObject[0] = lowHeadObject; paramObject[1] = _paramObject[1]; paramObject[2] = _paramObject[2];
            HeadView lowHeadView = new HeadView(lowHead, paramObject);
        }

        private void DrawBeat()
        {
            // 绘制节拍信息
            GameObject beatObject = new GameObject("Beat");
            beatObject.transform.SetParent(_parentObject.transform);
            beatObject.transform.localPosition = Vector3.zero;
            Beat beat = _measure.GetBeat();
            // 将beat对象对象赋为下一层的父对象
            GameObject[] paramObject = new GameObject[3];
            paramObject[0] = beatObject; paramObject[1] = _paramObject[1]; paramObject[2] = _paramObject[2];
            BeatView beatView = new BeatView(beat, paramObject);
        }

        // 绘制线段
        private void DrawLine(float startX, float startY, float stopX, float stopY)
        {
            // 实例化一个线段对象
            GameObject lineObject = GameObject.Instantiate(_prefabLine, _measureLines.transform.position, _prefabLine.transform.rotation);
            lineObject.transform.SetParent(_measureLines.transform);
            RectTransform lineRect = lineObject.GetComponent<RectTransform>();
            float width = Math.Max(startX, stopX) - Math.Min(startX, stopX) + 1;
            float heigh = Math.Max(startY, stopY) - Math.Min(startY, stopY) + 1;
            lineRect.sizeDelta = new Vector2(width, heigh); // 设置线段长宽
            // 设置线段位置，从最小画到最大
            lineRect.localPosition = new Vector3(Math.Min(startX, stopX), Math.Min(startY, stopY), 0);
        }
    }
}