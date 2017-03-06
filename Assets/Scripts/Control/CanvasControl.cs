using System.Collections.Generic;
using Control;
using UnityEngine;
using generator;
using symbol;
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
//        DrawScore("Assets/Materials/example.xml");
	    DrawScore("Assets/Materials/MusicXml/印第安鼓.xml");
	}

	// Update is called once per frame
	private void Update ()
	{

	}

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

        // 绘制乐谱视图
        ScoreView scoreView = new ScoreView(scoreList, paramObjects, screenSize, scoreInfo);

        // 更改乐符颜色
        Symbol symbol = scoreList[0][0].GetMeasureSymbolList()[0][1][2];
        SymbolControl symbolControl = new SymbolControl(symbol);
        symbolControl.SetColor(Color.red);
    }
}
