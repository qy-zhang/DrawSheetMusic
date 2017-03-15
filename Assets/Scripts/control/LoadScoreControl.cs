using System.IO;
using util;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace control
{
    public class LoadScoreControl : MonoBehaviour
    {
        private CommonParams _commonParams = CommonParams.GetInstance();
        private GameObject _prefabSymbol;
        private GameObject _prefabText;
        private GameObject _prefabLine;
        private GameObject _prefabFileButton;

        private void Awake()
        {
            // 初始化一些参数
            _prefabSymbol = (GameObject)Resources.Load("Prefabs/Prefab_Symbol");
            _prefabText = (GameObject)Resources.Load("Prefabs/Prefab_Text");
            _prefabLine = (GameObject)Resources.Load("Prefabs/Prefab_Line");
            _prefabFileButton = (GameObject)Resources.Load("Prefabs/Prefab_FileButton");
            // 设置到单例模式中对应的参数
            _commonParams.SetPrefabSymbol(_prefabSymbol);
            _commonParams.SetPrefabText(_prefabText);
            _commonParams.SetPrefabLine(_prefabLine);
            _commonParams.SetPrefabFileButton(_prefabFileButton);
        }

        // Use this for initialization
        void Start()
        {
            // 设置到单例模式中对应的参数
            _commonParams.SetPrefabSymbol(_prefabSymbol);
            _commonParams.SetPrefabText(_prefabText);
            _commonParams.SetPrefabLine(_prefabLine);
            _commonParams.SetPrefabFileButton(_prefabFileButton);
            LoadScore();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void LoadScore()
        {
            // 获取Canvas
            GameObject canvasObject = GameObject.Find("Canvas_Menu");

            // 遍历musicxml目录里的所有xml文件
            DirectoryInfo xmlFolder = new DirectoryInfo(_commonParams.GetXmlFolderPath());

            int xmlFileCount = 0;
            Vector3 buttonPosition = new Vector3(Screen.width/2, Screen.height - 100, 0);
            foreach (FileInfo xmlFile in xmlFolder.GetFiles())
            {
                if (xmlFile.Extension == ".xml")
                {
                    xmlFileCount += 1;
                    if (xmlFileCount >= 10) //TODO 设置一页最长放置个数，后续完善成滚动加载
                    {
                        break;
                    }

                    string buttonName = "Button" + xmlFileCount;
                    GameObject buttonObject = GameObject.Instantiate(_commonParams.GetPrefabFileButton(),
                        canvasObject.transform.position, _commonParams.GetPrefabFileButton().transform.rotation);
                    buttonObject.name = buttonName;
                    buttonObject.transform.SetParent(canvasObject.transform);
                    RectTransform rect = buttonObject.GetComponent<RectTransform>();
                    // 设置位置为以画布左下角为坐标原点
                    rect.position = new Vector3(buttonPosition.x,
                        buttonPosition.y - 50 * xmlFileCount,
                        buttonPosition.z);
                    Text btnText = buttonObject.GetComponentInChildren<Text>();
                    btnText.text = xmlFile.Name.Replace(xmlFile.Extension, ""); // 设置button显示文字为去掉扩展名的文件名

                    Button button = buttonObject.GetComponent<Button>();
                    button.onClick.AddListener(delegate
                    {
                        _commonParams.SetScoreName(xmlFile.FullName); // 设置要加载的xml文件名
                        SceneManager.LoadScene("DrawScore");
                    });
                }
            }
        }
    }
}