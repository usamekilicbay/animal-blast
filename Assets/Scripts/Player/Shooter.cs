using Enemy;
using Managers;
using System;
using UnityEngine;

namespace Player.Shooting
{
    public class Shooter : MonoBehaviour
    {
        [Range(0f, 10f)]
        [SerializeField] private float fireRate;

        private static float _Timer;

        public static int CurrentDamage = 1;

        public static Action<int> UpdateDamageAction { get; private set; }

        #region Unity

        private void Awake()
        {
            _Timer = fireRate;
        }

        private void OnEnable()
        {
            UpdateDamageAction += UpdateDamage;
        }

        private void OnDisable()
        {
            UpdateDamageAction -= UpdateDamage;
        }

        private void Update()
        {
            if (GameManager.GameState == GameState.GAME)
                Shoot();
        }

        #endregion

        private void Shoot()
        {
            _Timer -= Time.deltaTime;

            var canShoot = _Timer <= 0
                && Input.GetMouseButton(0);

            if (canShoot)
            {
                SfxManager.PlaySfxAction?.Invoke(SfxType.SHOOTING);
                BulletPool.LendProjectileAction?.Invoke();
                _Timer = fireRate;
            }
        }

        private void UpdateDamage(int killedEnemyCount)
        {
            CurrentDamage = (int)Mathf.Pow(killedEnemyCount + 1, 2);
        }

    }
}
