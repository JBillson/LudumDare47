using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace FpsController
{
    public class PlayerController : SerializedMonoBehaviour
    {
        private const float Gravity = -9.81f;

        [SerializeField] private float movementSpeed = 12;
        [SerializeField] private float jumpHeight    = 3f;
        [SerializeField] private float dashSpeed     = 8f;
        [SerializeField] private float dashDuration  = 0.3f;
        [SerializeField] private float dashCooldown  = 2f;

        private float dashCountdown;

        public enum DashDirection
        {
            Forward,
            Left,
            Right,
            Backwards
        }

        [FormerlySerializedAs("InAirSpeedModifier")] [SerializeField, Range(1, 2)]
        private float inAirSpeedModifier;

        private CharacterController CharacterController { get; set; }
        private Vector3             _velocity;

        private int   _jumpCount;
        private int   _maxJumpCount = 2;
        private bool  _dashing;
        private float _dashTimer = 0;

        private void Awake()
        {
            CharacterController = GetComponent<CharacterController>();
            dashCountdown = dashCooldown;
        }

        private void Update()
        {
            GroundSanityCheck();

            if (_dashing)
            {
                _dashTimer += Time.deltaTime;
                return;
            }

            dashCountdown -= Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.LeftShift) && dashCountdown <= 0 && Input.GetKey(KeyCode.A))
            {
                StartCoroutine(Dash(DashDirection.Left));
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift) && dashCountdown <= 0 && Input.GetKey(KeyCode.D))
            {
                StartCoroutine(Dash(DashDirection.Right));
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift) && dashCountdown <= 0 && Input.GetKey(KeyCode.S))
            {
                StartCoroutine(Dash(DashDirection.Backwards));
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift) && dashCountdown <= 0)
            {
                StartCoroutine(Dash(DashDirection.Forward));
            }

            var x = Input.GetAxis("Horizontal");
            var z = Input.GetAxis("Vertical");

            if (Input.GetKeyDown(KeyCode.Space) && _jumpCount < _maxJumpCount)
            {
                _velocity.y = Mathf.Sqrt(jumpHeight * -2f * Gravity);
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
                CharacterController.Move(moveVector * (movementSpeed / inAirSpeedModifier * Time.deltaTime));
            else
                CharacterController.Move(moveVector * (movementSpeed * Time.deltaTime));

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

        public IEnumerator Dash(DashDirection dashDirection)
        {
            _dashing = true;
            dashCountdown = dashCooldown;
            var moveVector = new Vector3();

            while (_dashTimer <= dashDuration)
            {
                if (dashDirection == DashDirection.Forward)
                {
                    var playerTransform = transform;
                    moveVector = playerTransform.right * 0 + playerTransform.forward * dashSpeed;
                }

                if (dashDirection == DashDirection.Left)
                {
                    var playerTransform = transform;
                    moveVector = playerTransform.right * 0 + -playerTransform.right * dashSpeed;
                }

                if (dashDirection == DashDirection.Right)
                {
                    var playerTransform = transform;
                    moveVector = playerTransform.right * 0 + playerTransform.right * dashSpeed;
                }
                
                if (dashDirection == DashDirection.Backwards)
                {
                    var playerTransform = transform;
                    moveVector = playerTransform.right * 0 + -playerTransform.forward * dashSpeed;
                }

                MoveController(moveVector);

                yield return null;
            }

            _dashTimer = 0;
            _dashing = false;
        }
    }
}