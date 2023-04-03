using System;
using UnityEngine;

public enum ParticleType
{
    ENEMY_COLLISION,
    ENEMY_HURT,
    ENEMY_DEATH,
    PLAYER_DEATH,
    PLAYER_HURT,
}

namespace Managers
{
    public class ParticleManager : MonoBehaviour
    {
        [SerializeField] private ParticleSystem EnemyHurtParticles;
        [SerializeField] private ParticleSystem EnemyDeathParticles;
        [SerializeField] private ParticleSystem EnemyCollisionParticles;
        [SerializeField] private ParticleSystem PlayerHurtParticles;
        [SerializeField] private ParticleSystem PlayerDeathParticles;

        public static Action<ParticleType, Vector2> SpawnParticleAction { get; private set; }

        #region Unity

        private void OnEnable()
        {
            SpawnParticleAction += SpawnParticle;
        }

        private void OnDisable()
        {
            SpawnParticleAction -= SpawnParticle;
        }

        #endregion

        private void SpawnParticle(ParticleType particleType, Vector2 spawnPosition)
        {
            var particleRef = new ParticleSystem();

            switch (particleType)
            {
                case ParticleType.ENEMY_COLLISION:
                    particleRef = EnemyCollisionParticles;
                    break;
                case ParticleType.ENEMY_HURT:
                    particleRef = EnemyHurtParticles;
                    break;
                case ParticleType.ENEMY_DEATH:
                    particleRef = EnemyDeathParticles;
                    break;
                case ParticleType.PLAYER_DEATH:
                    particleRef = PlayerDeathParticles;
                    break;
                case ParticleType.PLAYER_HURT:
                    particleRef = PlayerHurtParticles;
                    break;
            }

            var particle = Instantiate(particleRef, transform);
            particle.transform.position = spawnPosition;
        }
    }
}
