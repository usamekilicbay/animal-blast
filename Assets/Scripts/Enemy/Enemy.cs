using DG.Tweening;
using Helper;
using Managers;
using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private CircleCollider2D _circleCollider;

        #region Unity

        private void Awake()
        {
            Setup();
        }

        private void Start()
        {
            Initial();
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            var layer = collision.gameObject.layer;

            if (layer == (int)Layer.BORDER)
            {
                BounceFromWall();
            }
            else if (layer == (int)Layer.FLOOR)
            {
                BounceFromGround();
            }
        }

        #endregion

        #region Setup

        private void Setup()
        {
            transform.position = new Vector2(0.5f, 3f);
            _rb = GetComponent<Rigidbody2D>();
            _circleCollider = GetComponent<CircleCollider2D>();
            _circleCollider.enabled = false;
            _rb.gravityScale = 0f;
        }

        #endregion

        private void Initial()
        {
            var randomStartPosX = Random.Range(0f, 1f) < 0.5f
            ? CameraManager.ViewPointBorderLeft - 2f
            : CameraManager.ViewPointBorderRight + 2f;

            transform.position = new Vector2(randomStartPosX, transform.position.y);
            var dir = randomStartPosX > 0f
                ? 1
                : -1;

            transform.DOMoveX(0f, 1f)
                .OnComplete(() =>
                {
                    _circleCollider.enabled = true;
                    _rb.gravityScale = 1f;
                    _rb.AddForce(-dir * 150f * Vector2.right);
                    _rb.AddTorque(Random.Range(-20f, 20f));
                });
        }

        private void BounceFromGround()
        {
            _rb.velocity = new Vector2(_rb.velocity.x, 12f);

            if (_rb.angularVelocity <= 0f)
                _rb.AddTorque(Random.Range(-20f, 20f));
            else
                _rb.AddTorque(-_rb.angularVelocity * 1.1f);
            //_rb.AddTorque(-_rb.angularVelocity * Random.Range(0.8f, 2f));
        }

        private void BounceFromWall()
        {
            float posX = transform.position.x;

            if (posX > 0)
                _rb.AddForce(Vector2.left * 150f);
            else
                _rb.AddForce(Vector2.right * 150f);

            _rb.AddTorque(posX * 1.2f);
        }
    }
}
