using Managers;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Range(1f, 50f)]
        [SerializeField] private float movementSpeed;

        private Camera _camera;
        private Vector3 _cameraPos;
        private static Vector3 StartPos;

        #region Unity

        private void Awake()
        {
            Initial();
        }

        private void OnEnable()
        {
            GameManager.ResetAction += ResetPosition;
        }

        private void OnDisable()
        {
            GameManager.ResetAction -= ResetPosition;
        }

        private void OnMouseDown()
        {
            if (GameManager.GameState != GameState.GAME)
                GameManager.GameState = GameState.GAME;
        }

        private void OnMouseDrag()
        {
            if (GameManager.GameState == GameState.GAME)
                DragPlayer();
        }

        #endregion

        #region Setup

        private void Initial()
        {
            StartPos = transform.position;
            _camera = Camera.main;
            _cameraPos = _camera.transform.position;
        }

        #endregion

        private void DragPlayer()
        {
            var dragPos = _camera.ScreenToWorldPoint(Input.mousePosition - _cameraPos);

            var dragPosX = Mathf.Clamp(dragPos.x, CameraManager.ViewPointBorderLeft, CameraManager.ViewPointBorderRight);
            var pos = new Vector3(dragPosX, transform.position.y, 0f);

            transform.position = pos;
        }

        private void ResetPosition()
        {
            transform.position = StartPos;
        }
    }
}
