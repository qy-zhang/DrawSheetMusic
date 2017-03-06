using System.Collections.Generic;
using generator;
using symbol;
using UnityEngine;

using xmlParser;

public class CanvasControl : MonoBehaviour
{
    public Material LineMaterial;
    public GameObject PrefabSymbol;
    public GameObject PrefabText;
    public GameObject PrefabLine;

    // Use this for initialization
	private void Start ()
	{
//	    DrawMeasure("Assets/Materials/三个滑稽的小丑.xml");
//        DrawScore("Assets/Materials/polka.xml");
	    DrawScore("Assets/Materials/印第安鼓.xml");
	}

	// Update is called once per frame
	private void Update ()
	{

	}

    // 绘制一个小节
//    private void DrawMeasure(string filename)
//    {
//        // 从xml中读乐谱score
//        XmlFacade xmlFacade = new XmlFacade(filename);
//        List<Symbol> highSymbolList = xmlFacade.GetHighSymbolList();
//        List<Symbol> lowSymbolList = xmlFacade.GetLowSymbolList();
////        Debug.Log(highSymbolList.Count);
//        ScoreGenerator scoreGenerator = new ScoreGenerator(xmlFacade.GetBeat().GetBeats(), xmlFacade.GetBeat().GetBeatType());
//        List<List<List<List<List<Symbol>>>>> scoreList = scoreGenerator.Generate(xmlFacade.GetHighSymbolList(), xmlFacade.GetLowSymbolList());
//        List<List<Symbol>> measureList = scoreList[0][0][0];
//
//        // 遍历每个小结measure，为每个小结绘制一个canvas，读取每个小结中的内容，绘制
//        for (int i = 0; i < 1; i++)
//        {
//            string measureName = "Measure" + (i + 1);
//            GameObject newMeasure = new GameObject(measureName);
//            Canvas newCanvas = newMeasure.AddComponent<Canvas>();
//            newCanvas.transform.SetParent(GameObject.Find("Canvas").transform);
//            RectTransform rect = newCanvas.GetComponent<RectTransform>();
//            // 设置位置为以画布左下角为坐标原点
//            rect.anchorMin = Vector2.zero; rect.anchorMax = Vector2.zero; rect.pivot = new Vector2(0.5f, 0.5f);
//            rect.position = new Vector3(33.5f, 600, 0);
//            // 设置画布大小
//            rect.sizeDelta = new Vector2(150, _paramsGetter.GetTotalHeight());
//
//            // 遍历小结中的每个乐符note
//            GameObject measureNotes = new GameObject("Notes");
//            measureNotes.transform.SetParent(newMeasure.transform);
//            measureNotes.transform.localPosition = new Vector3(0, 0, 0);
//
//            GameObject[] paramObjects = new GameObject[3];
//            paramObjects[0] = measureNotes;
//            paramObjects[1] = PrefabSymbol;
//            paramObjects[2] = PrefabLine;
////            List<List<Symbol>> measureList = new List<List<Symbol>>();
////            measureList.Add(highSymbolList); measureList.Add(lowSymbolList);
//
////            MeasureView measureView = new MeasureView(measureList, paramObjects);
//
//            // 画五条线，一行要放4个小结，每个小结长度为600 / 4 = 150
//            // 设置初始乐谱线的位置
//            float measureLineLength = (Screen.width - 67f) / 4;
//
//            GameObject measureLines = new GameObject("Lines");
//            measureLines.transform.SetParent(newMeasure.transform);
//            measureLines.transform.localPosition = new Vector3(0, 0, 0);
//            // 绘制五条乐谱线
////            for (int j = 0; j < 5; j++)
////            {
////                string lineName = "Line" + (j + 1);
////                GameObject measureLine = new GameObject(lineName);
////                measureLine.transform.SetParent(measureLines.transform);
////                measureLine.transform.localPosition = new Vector3(0, 0, 0);
////                LineRenderer lr = measureLine.AddComponent<LineRenderer>();
////                lr.useWorldSpace = false;
////                lr.material = LineMaterial;
////                // 设置每根乐谱线的位置
////                Vector3 measureLineStart = new Vector3(measureLineLength*i, j*_paramsGetter.GetUnit(), 0);
////                Vector3 measureLineEnd = new Vector3(measureLineLength*(i+1), j*_paramsGetter.GetUnit(), 0);
////
////                lr.SetPosition(0, measureLineStart);
////                lr.SetPosition(1, measureLineEnd);
////
////            }
//
//
//            // 画高音谱号
//           if (i == 0)
//           {
//               string noteName = "NoteG";
//               GameObject newNote = new GameObject(noteName);
//               newNote.transform.SetParent(measureNotes.transform);
//               Text noteText = newNote.AddComponent<Text>();
//               noteText.font = _paramsGetter.GetSymbolFont();
//               noteText.fontSize = _paramsGetter.GetSymbolSize();
////                newFont.rectTransform.sizeDelta = new Vector2(300, 200);
////                newFont.rectTransform.localScale = new Vector3(1, 1, 1);
//               noteText.color = Color.black;
//               noteText.alignment = TextAnchor.MiddleCenter;
//               noteText.alignByGeometry = true;
//               noteText.text = "\uE19E";
//               noteText.transform.localPosition = new Vector3(_paramsGetter.GetSymbolStart()*2, _paramsGetter.GetStaffCenterPosition(), 0);
//           }
//
//            // 使用UI Image加载Line材料绘制五条乐谱线
//            for (int j = 0; j < 5; j++)
//            {
////                string lineName = "Line" + (j + 1);
////                GameObject measureLine = new GameObject(lineName);
////                measureLine.transform.SetParent(measureLines.transform);
////                Image img = measureLine.AddComponent<Image>();
////                img.material = LineMaterial;
////                // 设置线段位置以及长度
////                RectTransform lineRect = measureLine.GetComponent<RectTransform>();
////                lineRect.sizeDelta = new Vector2(measureLineLength, 1); // 设置线条宽度
////                // 设置线段的起点为左边，如果设置pivot为0.5的话线段起点坐标为线段中间位置
////                lineRect.anchorMin = Vector2.zero; lineRect.anchorMax = Vector2.zero; lineRect.pivot = Vector2.zero;
////                // 设置每根乐谱线的位置
////                Vector3 measureLineStart = new Vector3(measureLineLength*i, j*_paramsGetter.GetUnit(), 0);
////                lineRect.localPosition = measureLineStart;
//                GameObject myLine = GameObject.Instantiate(PrefabLine, measureLines.transform.position, PrefabLine.transform.rotation);
//                myLine.transform.SetParent(measureLines.transform);
//                RectTransform lineRect = myLine.GetComponent<RectTransform>();
//                lineRect.sizeDelta = new Vector2(measureLineLength, 1); // 设置线段长宽
//                float yPos = _paramsGetter.GetStaffCenterPosition() + (j - 2) * _paramsGetter.GetUnit(); // 第三条线(j==2)在乐谱中心位置
//                lineRect.localPosition = new Vector3(measureLineLength*i, yPos, 0); // 设置线段位置
//            }
//            // 绘制小结的两条竖线
//            for (int j = 0; j < 2; j++)
//            {
//                string lineName = "Line" + (j + 6);
//                GameObject measureLine = new GameObject(lineName);
//                measureLine.transform.SetParent(measureLines.transform);
//                Image img = measureLine.AddComponent<Image>();
//                img.material = LineMaterial;
//                // 设置线段位置以及长度
//                RectTransform lineRect = measureLine.GetComponent<RectTransform>();
//                lineRect.sizeDelta = new Vector2(1, _paramsGetter.GetStaffHeight()); // 设置线条宽度
//                // 设置线段的起点为左边，如果设置pivot为0.5的话线段起点坐标为线段中间位置
//                lineRect.anchorMin = Vector2.zero; lineRect.anchorMax = Vector2.zero; lineRect.pivot = Vector2.zero;
//                // 设置每根乐谱线的位置
//                float yPos = _paramsGetter.GetStaffCenterPosition() - 2 * _paramsGetter.GetUnit(); // 竖线的纵坐标起点为五条横线的最下面一条线的纵坐标
//                Vector3 measureLineStart = new Vector3(measureLineLength*(i+j), yPos, 0);
//                lineRect.localPosition = measureLineStart;
//            }
//        }
//    }

    private void DrawScore(string filename)
    {
        // 解析MusicXml文件
        XmlFacade xmlFacade = new XmlFacade(filename);
        // 生成乐谱表
        ScoreGenerator scoreGenerator = new ScoreGenerator(xmlFacade.GetBeat().GetBeats(), xmlFacade.GetBeat().GetBeatType());
        List<List<Measure>> scoreList = scoreGenerator.Generate(xmlFacade.GetMeasureList(), Screen.width - 67);

        // 准备绘制乐谱对象及其他参数
        GameObject[] paramObjects = new GameObject[4];
        paramObjects[0] = GameObject.Find("Canvas_Score");
        paramObjects[1] = PrefabSymbol;
        paramObjects[2] = PrefabLine;
        paramObjects[3] = PrefabText;
        float[] screenSize = new float[2];
        screenSize[0] = Screen.width;
        screenSize[1] = Screen.height;
        List<string> scoreInfo = new List<string>();
        // 乐谱名称和作者信息
        scoreInfo.Add(xmlFacade.GetWorkTitle()); // 0
        scoreInfo.Add(xmlFacade.GetCreator()); // 1
        // 乐谱拍号信息
        scoreInfo.Add(xmlFacade.GetBeat().GetBeats()); // 2
        scoreInfo.Add(xmlFacade.GetBeat().GetBeatType()); // 3
        // 乐谱高低音谱号信息
        scoreInfo.Add(xmlFacade.GetHighHead().GetFifths()); // 4
        scoreInfo.Add(xmlFacade.GetHighHead().GetSign()); // 5
        scoreInfo.Add(xmlFacade.GetHighHead().GetLine()); // 6
        scoreInfo.Add(xmlFacade.GetLowHead().GetFifths()); // 7
        scoreInfo.Add(xmlFacade.GetLowHead().GetSign()); // 8
        scoreInfo.Add(xmlFacade.GetLowHead().GetLine()); // 9

        // 绘制乐谱视图
        ScoreView scoreView = new ScoreView(scoreList, paramObjects, screenSize, scoreInfo);
    }
}
