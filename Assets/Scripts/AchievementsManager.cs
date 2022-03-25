using UnityEngine;
using TMPro;


    public class AchievementsManager : MonoBehaviour
    {
        #region Singleton
        private static AchievementsManager _instance;
        
        public static AchievementsManager Instance => _instance;
        
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            } else {
                _instance = this;
            }
        }
        

        #endregion
        
        [SerializeField] private TextMeshProUGUI firstAchievement;
        [SerializeField] private TextMeshProUGUI secondAchievement;
        [SerializeField] private int killCountCondition;
        private int _totalKillCount = 0;
        private int _totalButtonPress = 0;

        public void IncreaseKillCount()
        {
            _totalKillCount++;
            if (_totalKillCount >= killCountCondition)
            {
                secondAchievement.text = "Complete";
            }
        }

        public void IncreaseButtonPress()
        {
            _totalButtonPress++;
            firstAchievement.text = "Complete";
        }
    }
