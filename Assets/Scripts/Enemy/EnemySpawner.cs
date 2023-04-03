using Configs;
using DG.Tweening;
using Managers;
using Player.Shooting;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Helper.Constant;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject enemy;
        [Space(10)]
        [SerializeField] private List<EnemyConfig> EnemyConfigs;

        private Tween _getHurtTween;

        private static SpriteRenderer _EnemySpriteRenderer;
        private static float _EnemyHealth;
        private static GameObject _Enemy;
        private static EnemyConfig _EnemyConfig;

        private static int _KilledEnemyCount;

        public static Action<float> HurtEnemyAction { get; private set; }

        #region Unity

        private void Awake()
        {
            _KilledEnemyCount = PlayerPrefs.GetInt(PlayerPrefsKey.KilledEnemyCount);
        }

        private void Start()
        {
            SpawnEnemy();
        }

        private void OnEnable()
        {
            GameManager.GameOverAction += ResetEnemy;
            GameManager.ResetAction += SpawnEnemy;
            HurtEnemyAction += Hurt;
        }

        private void OnDisable()
        {
            GameManager.GameOverAction -= ResetEnemy;
            GameManager.ResetAction -= SpawnEnemy;
            HurtEnemyAction -= Hurt;
        }

        #endregion

        private void Hurt(float damage)
        {
            _EnemyHealth -= damage;

            UIManager.UpdateHealthBarAction?.Invoke(_EnemyHealth);

            PlayGetHurtAnimation();

            if (_EnemyHealth <= 0f)
                KillEnemy();
        }

        private void PlayGetHurtAnimation()
        {
            _getHurtTween.Restart();
        }

        private void KillEnemy()
        {
            _getHurtTween.Kill();
            ParticleManager.SpawnParticleAction?.Invoke(ParticleType.ENEMY_DEATH, _Enemy.transform.position);
            SfxManager.PlaySfxAction?.Invoke(SfxType.SHOOTING);
            CameraManager.ShakeCameraAction?.Invoke();
            ScoreManager.ScoreAction?.Invoke(_EnemyConfig.KillPoint);
            _KilledEnemyCount++;
            Shooter.UpdateDamageAction?.Invoke(_KilledEnemyCount);
            PlayerPrefs.SetInt(PlayerPrefsKey.KilledEnemyCount, _KilledEnemyCount);
            Destroy(_Enemy);

            if (_KilledEnemyCount >= 16)
                UIManager.UpdateShownPanelAction(PanelType.WIN);
            else
                SpawnEnemy();
        }

        private void SpawnEnemy()
        {
            _KilledEnemyCount = PlayerPrefs.GetInt(PlayerPrefsKey.KilledEnemyCount);
            _EnemyConfig = EnemyConfigs[_KilledEnemyCount];
            _Enemy = Instantiate(enemy, transform);
            _Enemy.name = _EnemyConfig.Name;
            _EnemySpriteRenderer = _Enemy.GetComponentInChildren<SpriteRenderer>();
            _EnemySpriteRenderer.sprite = _EnemyConfig.Artwork;
            _EnemyHealth = _EnemyConfig.Health;
            UIManager.UpdateHealthBarMaxLimitAction(_EnemyHealth);
            _getHurtTween = _EnemySpriteRenderer.transform.DOScale(1.3f * transform.localScale, 0.05f);
            _getHurtTween.SetEase(Ease.Linear);
            _getHurtTween.OnComplete(() => _getHurtTween.Rewind());
        }

        private void ResetEnemy()
        {
            Destroy(_Enemy);
        }
    }
}
