using Enemy;
using System;
using UnityEngine;

public enum GameState
{
    READY,
    START,
    GAME,
    GAMEOVER,
    PAUSE,
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
                    case GameState.START:
                        StartGame();
                        break;
                    case GameState.GAME:
                        Time.timeScale = 1f;
                        break;
                    case GameState.GAMEOVER:
                        GameOver();
                        break;
                    case GameState.PAUSE:
                        Pause();
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

        private static void StartGame()
        {
            EnemySpawner.SpawnEnemyAction();
            GameState = GameState.GAME;
        }

        private static void Pause()
        {
            Time.timeScale = 0f;
        }

        private static void GameOver()
        {
            UIManager.UpdateShownPanelAction(PanelType.GAMEOVER);
            GameOverAction();
        }
    }
}
