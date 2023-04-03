using System;
using UnityEngine;
using static Helper.Constant;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        public static int _Score;
        private static int _HighScore;

        public static Action<int> ScoreAction { get; private set; }

        #region Unity

        private void Awake()
        {
            _HighScore = PlayerPrefs.GetInt(PlayerPrefsKey.HighScore, 0);
        }

        private void Start()
        {
            UIManager.UpdateScoreTextAction?.Invoke(_Score);
        }

        private void OnEnable()
        {
            GameManager.ResetAction += ResetScore;
            GameManager.GameOverAction += UpdateHighScore;
            ScoreAction += Score;
        }

        private void OnDisable()
        {
            GameManager.ResetAction -= ResetScore;
            GameManager.GameOverAction -= UpdateHighScore;
            ScoreAction -= Score;
        }

        #endregion

        private void Score(int score)
        {
            _Score += score;
            UIManager.UpdateScoreTextAction?.Invoke(_Score);
        }

        private void UpdateHighScore()
        {
            if (_Score <= _HighScore)
                return;

            _HighScore = _Score;
            UIManager.UpdateHighScoreTextAction?.Invoke(_HighScore);

            PlayerPrefs.SetInt(PlayerPrefsKey.HighScore, _HighScore);
        }

        private void ResetScore()
        {
            _Score = 0;
            UIManager.UpdateScoreTextAction?.Invoke(_Score);
        }
    }
}
