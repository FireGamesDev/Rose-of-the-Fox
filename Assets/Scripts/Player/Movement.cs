using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.Player
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private Animator _anim;
        [SerializeField] private float speed;
        [SerializeField] private VariableJoystick _movementJoystick;

        private CharacterInput inputs;
        private SpriteRenderer img;
        private Vector2 _direction;
        private Coroutine flipRoutine;

        private bool isFacingRight = false;
        private bool isMobile = false;

        private void Awake()
        {
            inputs = new CharacterInput();
            img = GetComponent<SpriteRenderer>();
            CheckIfIsMobile();
        }

        private void Update()
        {
            GetDirection();
            SetAnim();
            SetSpriteFacing();
        }

        private void FixedUpdate()
        {
            _rb.MovePosition(_rb.position + _direction.normalized * speed * Time.fixedDeltaTime);
        }

        private void OnEnable()
        {
            inputs.Enable();
        }

        private void OnDisable()
        {
            inputs.Disable();
        }

        #region CustomMethods

        private void Flip()
        {
            _anim.SetTrigger("turn");
            isFacingRight = !isFacingRight;
            if (flipRoutine != null) StopCoroutine(flipRoutine);
            flipRoutine = StartCoroutine(FlipTimer());
        }

        private IEnumerator FlipTimer()
        {
            yield return new WaitForSeconds(0.35f);
            img.flipX = isFacingRight;
        }

        private void CheckIfIsMobile()
        {
            isMobile = SetMobile.isMobile;
            _movementJoystick.gameObject.SetActive(isMobile);
        }

        private void GetDirection()
        {
            if (isMobile)
            {
                _direction = _movementJoystick.Direction;
            }
            else
            {
                _direction = inputs.Player.Movement.ReadValue<Vector2>().normalized;
            }
        }

        private void SetAnim()
        {
            _anim.SetFloat("speed", Mathf.Abs(_direction.x) + Mathf.Abs(_direction.y));
        }

        private void SetSpriteFacing()
        {
            if ((!isFacingRight && _direction.x > 0f) || (isFacingRight && _direction.x < 0f))
            {
                Flip();
            }
        }
        #endregion
    }
}
