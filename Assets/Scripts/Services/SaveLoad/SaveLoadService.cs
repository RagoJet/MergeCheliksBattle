using System.IO;
using UnityEngine;

namespace Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string myPath = "/MyGame.json";

        private SavedData _savedData;
        public SavedData SavedData => _savedData;


        public SaveLoadService()
        {
            _savedData = new SavedData();
        }

        public void SaveProgress()
        {
            string JSONString = JsonUtility.ToJson(_savedData);
            File.WriteAllText(Application.persistentDataPath + myPath, JSONString);
        }

        public void LoadProgress()
        {
            string pathToData = Application.persistentDataPath + myPath;
            if (File.Exists(pathToData))
            {
                var dataProgressFromJson =
                    JsonUtility.FromJson<SavedData>(File.ReadAllText(Application.persistentDataPath + myPath));
                if (dataProgressFromJson != null)
                {
                    _savedData = dataProgressFromJson;
                }
            }
        }
    }
}