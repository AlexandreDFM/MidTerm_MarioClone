using Managers;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        private static readonly int Jump = Animator.StringToHash("Jump");
        private static readonly int Walk = Animator.StringToHash("Walk");
        private static readonly int IsDead = Animator.StringToHash("isDead");
        private static readonly int IsPowerUpFire = Animator.StringToHash("IsPowerUpFire");
        private static readonly int IsThrowFire = Animator.StringToHash("IsThrowFire");

        private Animator _anim;
        private Rigidbody2D _myBody;

        private int _hp = 1;
        private float _direction = 1f;
        private float _playerSpeed = 3.5f;
        private bool _isPowerUpFire = false;
        private const float PlayerJumpPower = 8f;


        private void Awake()
        {
            _anim = GetComponent<Animator>();
            _myBody = GetComponent<Rigidbody2D>();
        }

        private void ChangeDirection(float direction)
        {
            Vector3 tempScale = transform.localScale;
            tempScale.x = direction;
            transform.localScale = tempScale;
        }

        private void Update()
        {
            PlayerJump();
            ThrowFire();
        }

        private void FixedUpdate()
        {
            PlayerWalk();
            PlayerSprint();
        }

        private void PlayerWalk()
        {
            var h = Input.GetAxisRaw("Horizontal");

            if (h != 0) {
                transform.position += new Vector3(h, 0f, 0f) * (_playerSpeed * Time.deltaTime);
                ChangeDirection(h > 0 ? _direction : -_direction);
                if (!_anim.GetBool(Walk)) _anim.SetBool(Walk, true);
            } else {
                if (_anim.GetBool(Walk)) _anim.SetBool(Walk, false);
            }
        }


        private void PlayerSprint()
        {
            _playerSpeed = Input.GetKey(KeyCode.LeftShift) ? 5.5f : 3.5f;
        }

        private void PlayerJump()
        {
            if (Input.GetKeyDown(KeyCode.Space)) {
                if (!(Mathf.Abs(_myBody.velocity.y) < 0.05f)) return;
                _anim.SetBool(Jump, true);
                SoundManager.Instance.PlayJumpSound();
                _myBody.velocity = new Vector2(_myBody.velocity.x, PlayerJumpPower);
            } else {
                switch (_myBody.velocity.y) {
                    case < 0.05f:
                        _anim.SetBool(Jump, false);
                        break;
                    case > 0.05f:
                        _anim.SetBool(Jump, true);
                        break;
                    default:
                        _anim.SetBool(Jump, false);
                        break;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D target)
        {
            if (!target.CompareTag("Coin")) return;
            GameManager.Instance.UpdateCoinCount(1);
            GameManager.Instance.UpdateScore(100);
            SoundManager.Instance.PlayCoinSound();
            Destroy(target.gameObject);
        }

        public void Grow()
        {
            _hp = 2;
            _direction = _direction > 0 ? 1.5f : -1.5f;
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }

        public void Shrink()
        {
            if (_isPowerUpFire) _isPowerUpFire = false;
            _hp -= 1;
            if (_hp > 1) return;
            _direction = _direction > 0 ? 1f : -1f;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        public void Fire()
        {
            if (_hp == 1) Grow();
            _hp = 3;
            _isPowerUpFire = true;
            _anim.SetBool(IsPowerUpFire, true);
        }

        private void ThrowFire()
        {
            if (!_isPowerUpFire) return;
            if (Input.GetKeyDown(KeyCode.F)) {
                _anim.SetBool(IsThrowFire, true);
                ThrowFireball();
            } else {
                _anim.SetBool(IsThrowFire, false);
            }
        }

        private void ThrowFireball()
        {
            GameObject fireBall = Instantiate(GameAssetsManager.Instance.fireBall,
                transform.position + new Vector3(transform.localScale.x / 2, 0f, 0f), Quaternion.identity);
            fireBall.GetComponent<FireBall>().SetDirection(transform.localScale);
        }

        public int GetPlayerHp()
        {
            return _hp;
        }

        public void DeadAnimation()
        {
            _anim.SetTrigger(IsDead);
        }
    }
}