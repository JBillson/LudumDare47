using UnityEngine;
using UnityEngine.Serialization;

namespace FpsController
{
    public class PlayerLook : MonoBehaviour
    {
        [SerializeField] private float mouseSensitivity = 150f;

        private Transform _playerBody;
        private float     _currentXRotation = 0f;

        private void Awake()
        {
            _playerBody = FindObjectOfType<PlayerController>().gameObject.transform;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            GetLookVector();

            if (Input.GetKeyDown(KeyCode.Mouse0))
                Cursor.lockState = CursorLockMode.Locked;
        }

        private void GetLookVector()
        {
            var xRot = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            var yRot = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
            RotateGameObject(yRot, xRot, _playerBody);
        }

        private void RotateGameObject(float yRot, float xRot, Transform toTurn)
        {
            _currentXRotation -= yRot;
            _currentXRotation = Mathf.Clamp(_currentXRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(_currentXRotation, 0f, 0f);
            toTurn.Rotate(Vector3.up * xRot);
        }
    }}