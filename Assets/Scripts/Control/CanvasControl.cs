using System.Collections.Generic;
using UnityEngine;
using generator;
using symbol;
using util;
using xmlParser;

namespace control
{
    public class CanvasControl : MonoBehaviour
    {
        public Material LineMaterial;
        public GameObject PrefabSymbol;
        public GameObject PrefabText;
        public GameObject PrefabLine;
        public GameObject PrefabFileButton;
        private CommonParams _commonParams = CommonParams.GetInstance();

        // Use this for initialization
        private void Start()
        {
//        DrawScore("Assets/Materials/example.xml");
            string scoreName = _commonParams.GetScoreName();
	        DrawScore(scoreName);
//            DrawScore("Assets/Materials/MusicXml/印第安鼓.xml");
        }

        // Update is called once per frame
        private void Update()
        {

        }

        private void DrawScore(string filename)
        {
            // 解析MusicXml文件
            XmlFacade xmlFacade = new XmlFacade(filename);
            // 生成乐谱表
            ScoreGenerator scoreGenerator =
                new ScoreGenerator(xmlFacade.GetBeat().GetBeats(), xmlFacade.GetBeat().GetBeatType());
            List<List<Measure>> scoreList = scoreGenerator.Generate(xmlFacade.GetMeasureList(), Screen.width - 67);

            // 准备绘制乐谱对象及其他参数
            GameObject parentObject = GameObject.Find("Canvas_Score");
            List<float> screenSize = new List<float>();
            screenSize.Add(Screen.width);
            screenSize.Add(Screen.height);
            List<string> scoreInfo = new List<string>();
            // 乐谱名称和作者信息
            scoreInfo.Add(xmlFacade.GetWorkTitle()); // 0
            scoreInfo.Add(xmlFacade.GetCreator()); // 1

            // 绘制乐谱视图
            ScoreView scoreView = new ScoreView(scoreList, parentObject, screenSize, scoreInfo);

            // 更改乐符颜色
//        Symbol symbol = scoreList[0][0].GetMeasureSymbolList()[0][1][2];
//        SymbolControl symbolControl = new SymbolControl(symbol);
//        symbolControl.SetColor(Color.red);
        }
    }
}