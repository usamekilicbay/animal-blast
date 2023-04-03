using Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Player.Shooting
{
    public class BulletPool : MonoBehaviour
    {
        [Range(1, 1000)]
        [SerializeField] private int poolSize;
        [Space(10)]
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private Transform shooterTransform;

        private static List<GameObject> _projectiles;

        public static Action<GameObject> FeedPoolAction { get; private set; }
        public static Action LendProjectileAction { get; private set; }

        #region Unity

        private void Awake()
        {
            SetPool();
        }

        private void OnEnable()
        {
            GameManager.ResetAction += ResetPool;
            FeedPoolAction += FeedPool;
            LendProjectileAction += LendProjectile;
        }

        private void OnDisable()
        {
            GameManager.ResetAction -= ResetPool;
            FeedPoolAction -= FeedPool;
            LendProjectileAction -= LendProjectile;
        }

        #endregion

        #region Setup

        private void SetPool()
        {
            _projectiles = new List<GameObject>();

            for (var i = 0; i < poolSize; i++)
            {
                var projectile = Instantiate(projectilePrefab, shooterTransform);
                projectile.SetActive(false);
                _projectiles.Add(projectile);
            }
        }

        #endregion

        private void FeedPool(GameObject projectile)
        {
            projectile.SetActive(false);
            projectile.transform.position = shooterTransform.position;
            _projectiles.Add(projectile);
        }

        private static void LendProjectile()
        {
            var projectile = _projectiles.Last();
            _projectiles.Remove(projectile);
            projectile.SetActive(true);
            // TODO: Call particle spawn here 
        }

        private void ResetPool()
        {
            for (var i = _projectiles.Count - 1; i > 0; i--)
                Destroy(_projectiles[i]);

            _projectiles.Clear();

            SetPool();
        }
    }
}
