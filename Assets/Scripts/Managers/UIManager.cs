using DG.Tweening;
using Managers;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Helper.Constant;

public enum PanelType
{
    GAME,
    PAUSE,
    GAMEOVER,
    WIN
}

public class UIManager : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] GameObject gamePanel;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject winPanel;
    [Header("UI")]
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private GameObject highScore;
    [Header("Score")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [Header("Enemy Health Bar")]
    [SerializeField] Button pauseButton;
    [SerializeField] Button resumeButton;
    [SerializeField] Button restartButton;
    [SerializeField] Button resetButton;
    [Header("Enemy Health Bar")]
    [SerializeField] private Slider healthBar;

    private static float _FontSize;

    public static Action<PanelType> UpdateShownPanelAction;
    public static Action<int> UpdateScoreTextAction;
    public static Action<int> UpdateHighScoreTextAction;
    public static Action<float> UpdateHealthBarMaxLimitAction;
    public static Action<float> UpdateHealthBarAction;

    #region Unity

    private void Awake()
    {
        _FontSize = scoreText.fontSize;
        ResetUI();
    }

    private void OnEnable()
    {
        pauseButton.onClick.AddListener(PauseGame);
        resumeButton.onClick.AddListener(ResumeGame);
        restartButton.onClick.AddListener(RestartGame);
        resetButton.onClick.AddListener(ResetGameProgress);

        GameManager.ResetAction += ResetUI;
        UpdateShownPanelAction += UpdateShownPanel;
        UpdateScoreTextAction += UpdateScoreText;
        UpdateHighScoreTextAction += UpdateHighScoreText;
        UpdateHealthBarMaxLimitAction += UpdateHealthBarMaxLimit;
        UpdateHealthBarAction += UpdateHealthBar;
    }

    private void OnDisable()
    {
        pauseButton.onClick.RemoveAllListeners();
        resumeButton.onClick.RemoveAllListeners();
        restartButton.onClick.RemoveAllListeners();
        resetButton.onClick.RemoveAllListeners();

        GameManager.ResetAction -= ResetUI;
        UpdateShownPanelAction -= UpdateShownPanel;
        UpdateScoreTextAction -= UpdateScoreText;
        UpdateHighScoreTextAction -= UpdateHighScoreText;
        UpdateHealthBarMaxLimitAction -= UpdateHealthBarMaxLimit;
        UpdateHealthBarAction -= UpdateHealthBar;
    }

    #endregion

    private void UpdateScoreText(int score)
    {
        scoreText.SetText(score.ToString());
    }

    private void UpdateHighScoreText(int score)
    {
        highScore.SetActive(true);
        highScoreText.SetText(score.ToString());
    }

    private void UpdateHealthBarMaxLimit(float maxHealth)
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }

    private void UpdateHealthBar(float health)
    {
        healthBar.value = health;
    }

    private void PauseGame()
    {
        GameManager.GameState = GameState.PAUSE;
        UpdateShownPanel(PanelType.PAUSE);
    }

    private void ResumeGame()
    {
        GameManager.GameState = GameState.GAME;
        UpdateShownPanel(PanelType.GAME);
    }

    private void RestartGame()
    {
        GameManager.ResetAction();
        GameManager.GameState = GameState.READY;
        UpdateShownPanel(PanelType.GAME);
        ResetUI();
    }

    private void ResetUI()
    {
        highScore.SetActive(false);
    }

    private void UpdateShownPanel(PanelType panelType)
    {
        gamePanel.SetActive(panelType == PanelType.GAME);
        pausePanel.SetActive(panelType == PanelType.PAUSE);
        gameOverPanel.SetActive(panelType == PanelType.GAMEOVER);
        winPanel.SetActive(panelType == PanelType.WIN);
    }

    private void ResetGameProgress()
    {
        PlayerPrefs.SetInt(PlayerPrefsKey.KilledEnemyCount, 0);
        RestartGame();
    }
}
