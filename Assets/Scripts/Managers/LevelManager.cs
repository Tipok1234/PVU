using UnityEngine;
using Assets.Scripts.DataSo;
using TMPro;

namespace Assets.Scripts.Managers
{

    public class LevelManager : MonoBehaviour
    {
        public LevelDataSO[] Levels => _levels;

        [SerializeField] private LevelDataSO[] _levels;
        [SerializeField] private TMP_Text _levelText;
        public LevelDataSO GetLevelByIndex(int index)
        {
            Debug.LogError("LEVEL: " + index);

            if (index >= _levels.Length)
            {
                Debug.LogError("Wrong Level Index");
                return _levels[0];
            }

            return _levels[index];
        }

        public void UpdateLevel(int indexLevel)
        {
            indexLevel++;

            _levelText.text = "Level " + indexLevel.ToString();
        }
    }
}
