using System.Collections.Generic;
using UnityEngine;

namespace symbol
{
    public class ParagraphView
    {
        private List<Measure> _paragraphList;
        private GameObject[] _paramObject;
        private GameObject _parentObject;

        public ParagraphView(List<Measure> paragraphList, GameObject[] paramObject)
        {
            _paragraphList = paragraphList;
            _paramObject = paramObject;
            _parentObject = paramObject[0];
            Init();
        }

        private void Init()
        {
            OnDraw();
        }

        private void OnDraw()
        {
            Vector3 measurePosition = Vector3.zero;
            // 遍历一行中的所有小节，绘制每个小节
            for (int i = 0; i < _paragraphList.Count; i++)
            {
                // 新建Measure对象作为目录
                string objName = "Measure" + (i + 1);
                GameObject measureObject = new GameObject(objName);
                measureObject.transform.SetParent(_parentObject.transform);
                measureObject.transform.localPosition = new Vector3(measurePosition.x,
                    measurePosition.y, measurePosition.z);

                // 将Measure对象对象赋为下一层的父对象
                GameObject[] paramObject = new GameObject[4];
                paramObject[0] = measureObject;
                paramObject[1] = _paramObject[1];
                paramObject[2] = _paramObject[2];
                paramObject[3] = _paramObject[3];

                // 绘制Measure视图
                MeasureView measureView = new MeasureView(_paragraphList[i], paramObject);

                // 调整下一个小节的起始横坐标
                measurePosition.x += _paragraphList[i].GetMeasureLength();
            }
        }
    }
}