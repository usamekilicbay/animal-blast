using Managers;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Range(1f, 50f)]
        [SerializeField] private float movementSpeed;

        private Camera _camera;
        private Vector3 _cameraPos;
        private static Vector3 StartPos;
        private int _direction;

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

        private void Update()
        {
            if (Input.GetMouseButtonUp(0) || IsReachedToBorder())
                _direction *= -1;

            if (GameManager.GameState == GameState.GAME)
                MovePlayer();

            if (Input.GetMouseButtonDown(0))
                if (GameManager.GameState == GameState.READY)
                    GameManager.GameState = GameState.START;
        }

        private bool IsReachedToBorder()
        {
            return transform.position.x <= CameraManager.ViewPointBorderLeft + 1f
                || transform.position.x >= CameraManager.ViewPointBorderRight - 1f;
        }

        //private void OnMouseDrag()
        //{
        //    if (GameManager.GameState == GameState.GAME)
        //        DragPlayer();
        //}

        private void MovePlayer()
        {
            transform.position += _direction * movementSpeed * Time.deltaTime * Vector3.right;
        }

        #endregion

        #region Setup

        private void Initial()
        {
            StartPos = transform.position;
            _camera = Camera.main;
            _cameraPos = _camera.transform.position;
            _direction = Random.Range(0f, 1f) < 0.5f
                ? -1 : 1;
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
