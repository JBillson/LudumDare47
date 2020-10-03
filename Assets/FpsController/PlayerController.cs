using Sirenix.OdinInspector;
using UnityEngine;

namespace FpsController
{
    public class PlayerController : SerializedMonoBehaviour
    {
        private const float Gravity = -9.81f;

        [SerializeField] private float MovementSpeed = 12;
        [SerializeField] private float JumpHeight    = 3f;

        [SerializeField, Range(1, 2)] private float InAirSpeedModifier;

        private CharacterController CharacterController { get; set; }
        private Vector3             _velocity;

        private int _jumpCount;
        private int maxJumpCount = 2;

        private void Awake()
        {
            CharacterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            GroundSanityCheck();

            var x = Input.GetAxis("Horizontal");
            var z = Input.GetAxis("Vertical");

            if (Input.GetKeyDown(KeyCode.Space) && _jumpCount < maxJumpCount)
            {
                _velocity.y = Mathf.Sqrt(JumpHeight * -2f * Gravity);
                _jumpCount++;
            }

            _velocity.y += Gravity * Time.deltaTime;

            var playerTransform = transform;
            var moveVector = playerTransform.right * x + playerTransform.forward * z;

            MoveController(moveVector);
        }

        private void MoveController(Vector3 moveVector)
        {
            if (!CharacterController.isGrounded)
                CharacterController.Move(moveVector * (MovementSpeed / InAirSpeedModifier * Time.deltaTime));
            else
                CharacterController.Move(moveVector * (MovementSpeed * Time.deltaTime));

            CharacterController.Move(_velocity * Time.deltaTime);
        }

        private void GroundSanityCheck()
        {
            if (CharacterController.isGrounded && _velocity.y < 0)
            {
                _jumpCount = 0;
                _velocity.y = -3f;
            }
        }
    }
}