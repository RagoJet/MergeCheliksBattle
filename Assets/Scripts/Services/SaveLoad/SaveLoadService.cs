using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string myPath = "/MyGame.json";
        private List<ISaveable> _saveables = new List<ISaveable>();

        private DataProgress _dataProgress;
        public DataProgress DataProgress => _dataProgress;

        public SaveLoadService()
        {
            _dataProgress = new DataProgress();
        }

        public void SaveProgress()
        {
            foreach (var isavable in _saveables)
            {
                isavable.Save(_dataProgress);
            }

            string JSONString = JsonUtility.ToJson(_dataProgress);
            File.WriteAllText(Application.persistentDataPath + myPath, JSONString);
        }

        public void LoadProgress()
        {
            string pathToData = Application.persistentDataPath + myPath;
            if (File.Exists(pathToData))
            {
                var dataProgressFromJson =
                    JsonUtility.FromJson<DataProgress>(File.ReadAllText(Application.persistentDataPath + myPath));
                if (dataProgressFromJson != null)
                {
                    _dataProgress = dataProgressFromJson;
                }
            }
        }

        public void Register(ISaveable saveable)
        {
            _saveables.Add(saveable);
        }
    }
}