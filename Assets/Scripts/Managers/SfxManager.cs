using System;
using UnityEngine;

public enum SfxType
{
    ENEMY_COLLISION,
    SHOOTING,
    ENEMY_DEATH,
    PLAYER_DEATH,
    PLAYER_HURT,
}

namespace Managers
{
    [RequireComponent(typeof(AudioSource))]
    public class SfxManager : MonoBehaviour
    {
        [SerializeField] private AudioClip ShootingSfx;
        [SerializeField] private AudioClip EnemyDeathSfx;
        [SerializeField] private AudioClip EnemyCollisionSfx;
        [SerializeField] private AudioClip PlayerHurtSfx;
        [SerializeField] private AudioClip PlayerDeathSfx;

        [SerializeField] private AudioSource audioSource;

        public static Action<SfxType> PlaySfxAction { get; private set; }

        #region Unity

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            PlaySfxAction += PlaySfx;
        }

        private void OnDisable()
        {
            PlaySfxAction -= PlaySfx;
        }

        #endregion

        private void PlaySfx(SfxType sfxType)
        {
            var sfx = EnemyCollisionSfx;

            switch (sfxType)
            {
                case SfxType.ENEMY_COLLISION:
                    sfx = EnemyCollisionSfx;
                    break;
                case SfxType.SHOOTING:
                    sfx = ShootingSfx;
                    break;
                case SfxType.ENEMY_DEATH:
                    sfx = EnemyDeathSfx;
                    break;
                case SfxType.PLAYER_DEATH:
                    sfx = PlayerDeathSfx;
                    break;
                case SfxType.PLAYER_HURT:
                    sfx = PlayerHurtSfx;
                    break;
            }

            audioSource.PlayOneShot(sfx);
        }
    }
}
