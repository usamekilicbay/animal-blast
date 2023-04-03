using DG.Tweening;
using System;
using UnityEngine;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        [Range(0f, 1f)]
        [SerializeField] private float shakePositionStrength;
        [Range(0f, 1f)]
        [SerializeField] private float shakeRotationStrength;
        [Range(0f, 1f)]
        [SerializeField] private float shakeDuration;

        private Camera _camera;

        public static float ViewPointBorderLeft;
        public static float ViewPointBorderRight;

        public static Action ShakeCameraAction { get; private set; }

        #region Unity

        private void Awake()
        {
            _camera = Camera.main;
            ViewPointBorderLeft = _camera.transform.position.x - _camera.orthographicSize * 0.5f;
            ViewPointBorderRight = _camera.transform.position.x + _camera.orthographicSize * 0.5f;
        }

        private void OnEnable()
        {
            ShakeCameraAction += ShakeCamera;
        }

        private void OnDisable()
        {
            ShakeCameraAction -= ShakeCamera;
        }

        #endregion

        private void ShakeCamera()
        {
            _camera.DOComplete();
            _camera.DOShakePosition(shakeDuration, shakePositionStrength * Vector3.one);
            _camera.DOShakeRotation(shakeDuration, shakeRotationStrength * Vector3.one);
        }
    }
}
