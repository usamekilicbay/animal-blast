using Helper;
using Managers;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private BoxCollider2D bc2d;

        #region Unity

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            bc2d = GetComponent<BoxCollider2D>();
        }

        private void OnEnable()
        {
            GameManager.ResetAction += ResetPlayer;
        }

        private void OnDisable()
        {
            GameManager.ResetAction -= ResetPlayer;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == (int)Layer.ENEMY)
                Death();
        }

        #endregion

        private void Death()
        {
            spriteRenderer.enabled = false;
            bc2d.enabled =false;
            ParticleManager.SpawnParticleAction?.Invoke(ParticleType.PLAYER_DEATH, transform.position);
            GameManager.GameState = GameState.GAMEOVER;
        }

        private void ResetPlayer()
        {
            spriteRenderer.enabled = true;
            bc2d.enabled = true;
        }
    }
}
