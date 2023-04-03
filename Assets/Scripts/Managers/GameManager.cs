using System;
using UnityEngine;
using static Helper.Constant;

public enum GameState
{
    READY,
    GAME,
    GAMEOVER,
    PAUSE
}

namespace Managers
{
    [RequireComponent(typeof(UIManager))]
    [RequireComponent(typeof(ScoreManager))]
    [RequireComponent(typeof(ParticleManager))]
    [RequireComponent(typeof(SfxManager))]
    [RequireComponent(typeof(CameraManager))]
    public class GameManager : MonoBehaviour
    {
        private static GameState _gamestate;
        public static GameState GameState
        {
            get => _gamestate;
            set
            {
                _gamestate = value;

                switch (GameState)
                {
                    case GameState.READY:
                        break;
                    case GameState.GAME:
                        Time.timeScale = 1;
                        break;
                    case GameState.GAMEOVER:
                        break;
                    case GameState.PAUSE:
                        Time.timeScale = 0;
                        break;
                    default:
                        break;
                }
            }
        }

        public static Action ResetAction { get; set; }
        public static Action GameOverAction { get; set; }

        private void Awake()
        {
            GameState = GameState.READY;
        }

        private void Start()
        {
            UIManager.UpdateShownPanelAction(PanelType.GAME);
        }

        private void Update()
        {
            switch (GameState)
            {
                case GameState.READY:
                    break;
                case GameState.GAME:
                    Time.timeScale = 1;
                    break;
                case GameState.GAMEOVER:
                    GameOver();
                    break;
                case GameState.PAUSE:
                    Time.timeScale = 0;
                    break;
                default:
                    break;
            }
        }

        private static void GameOver()
        {
            UIManager.UpdateShownPanelAction(PanelType.GAMEOVER);
            GameOverAction();
        }
    }
}
