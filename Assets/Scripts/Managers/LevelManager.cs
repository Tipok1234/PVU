using UnityEngine;
using Assets.Scripts.DataSo;
using Assets.SimpleLocalization;
using Assets.Scripts.UI;
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

            _levelText.text = LocalizationManager.Localize(LocalizationConst.GameMenu + "Level", indexLevel);
        }
    }
}
