using UnityEngine;

namespace util
{
    public class CommonParams
    {
        private static CommonParams instance = new CommonParams();

        // 作为公共数据存储类
        private string _scoreName = "";
        private string _xmlFolderPath = "Assets/Materials/MusicXml";
        private GameObject _prefabSymbol;
        private GameObject _prefabText;
        private GameObject _prefabLine;
        private GameObject _prefabFileButton;

        public static CommonParams GetInstance() { return instance; }

        public string GetScoreName() { return _scoreName; }

        public void SetScoreName(string scoreName) { _scoreName = scoreName; }

        public string GetXmlFolderPath() { return _xmlFolderPath; }

        public void SetXmlFolderPath(string xmlFolderPath) { _xmlFolderPath = xmlFolderPath; }

        public GameObject GetPrefabSymbol() { return _prefabSymbol; }

        public void SetPrefabSymbol(GameObject prefabSymbol) { _prefabSymbol = prefabSymbol; }

        public GameObject GetPrefabLine() { return _prefabLine; }

        public void SetPrefabLine(GameObject prefabLine) { _prefabLine = prefabLine; }

        public GameObject GetPrefabText() { return _prefabText; }

        public void SetPrefabText(GameObject prefabText) { _prefabText = prefabText; }

        public GameObject GetPrefabFileButton() { return _prefabFileButton; }

        public void SetPrefabFileButton(GameObject prefabFileButton) { _prefabFileButton = prefabFileButton; }
    }
}