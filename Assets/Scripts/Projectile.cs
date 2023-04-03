using Configs;
using Enemy;
using Helper;
using Managers;
using Player.Shooting;
using UnityEngine;

namespace Projectile
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private ProjectileConfig ProjectileConfig;
        [SerializeField] private Rigidbody2D rb;

        #region Unity

        private void OnEnable()
        {
            rb.AddForce(ProjectileConfig.ProjectileSpeed * Vector2.up);
        }

        private void OnDisable()
        {
            rb.velocity = Vector2.zero;
        }

        private void FixedUpdate()
        {
            if (transform.position.y > 20f)
                BulletPool.FeedPoolAction?.Invoke(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var layer = collision.gameObject.layer;

            var canDamage = GameManager.GameState == GameState.GAME
                && layer == (int)Layer.ENEMY;

            if (canDamage)
            {
                //CameraManager.ShakeCameraAction?.Invoke();
                EnemySpawner.HurtEnemyAction?.Invoke(ProjectileConfig.Damage);
                ParticleManager.SpawnParticleAction?.Invoke(ParticleType.ENEMY_HURT, transform.position);
                ScoreManager.ScoreAction?.Invoke(Shooter.CurrentDamage);
                BulletPool.FeedPoolAction?.Invoke(gameObject);
            }
        }

        #endregion
    }
}
