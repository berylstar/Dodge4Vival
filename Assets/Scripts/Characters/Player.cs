using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player I;

    private Camera _mainCam;
    private readonly int MIN_CAMERA_DISTANCE = 2;
    private readonly int MAX_CAMERA_DISTANCE = 15;
    private bool _canAttack = true;
    private bool _isInvincible = false;
    private bool _isRoll = false;
    private float _rotZ = 0f;

    private Vector2 _moveInput;
    private BulletController _bulletController;

    [Header("Player")]
    [SerializeField] private SpriteRenderer _playerRenderer;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private Rigidbody2D _playerRigidbody;

    [Header("Weapon")]
    [SerializeField] private SpriteRenderer _weaponRenderer;
    [SerializeField] private Transform _weaponTransform;

    [Header("Bullet")]
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _bulletSpawnPoint;

    [Header("Variable")]
    public IntVariable HP;
    public IntVariable MaxHP;
    public IntVariable LowHP;
    public IntVariable Speed;
    public IntVariable StartSpeed;
    public IntVariable SpeedUpAmount;
    public FloatVariable PlayerAttackCooltime;
    public FloatVariable PlayerRollCooltime;
    public FloatVariable InvincibleTime;
    public FloatVariable SpeedUpTime;

    [Header("Event")]
    public GameEvent EventPlayerHit;
    public GameEvent EventPlayerDie;
    public GameEvent EventPlayerHeal;
    public GameEvent EventPlayerLowHP;
    public GameEvent EventPlayerSpeedUp;
    public GameEvent EventPlayerInvincible;
    public GameEvent EventPlayerRoll;

    private void Awake()
    {
        I = this;
        HP.Set(MaxHP.i);
        Speed.Set(StartSpeed.i);
        _mainCam = Camera.main;
        _bulletController = GetComponent<BulletController>();
    }

    private void FixedUpdate()
    {
        _mainCam.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster") && !_isInvincible)
        {
            EventPlayerHit.Raise();

            if (HP.i <= 0)
            {
                HP.i = 0;
                EventPlayerDie.Raise();
            }
            else if (HP.i <= LowHP.i)
            {
                EventPlayerLowHP.Raise();
            }

        }
        else if (collision.CompareTag("Trap"))
        {
            collision.GetComponent<Trap>().Effect(this);
        }
    }

    public void OnHitAnimator()
    {
        _playerAnimator.SetTrigger("IsHit");
        _playerRenderer.color = new Color32(200, 100, 100, 255);
        _isInvincible = true;
        StartCoroutine(HitCo());
    }

    private IEnumerator HitCo()
    {
        yield return new WaitForSecondsRealtime(1f);
        _playerRenderer.color = Color.white;
        _isInvincible = false;
    }

    public void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>().normalized;

        _playerAnimator.SetBool("IsMoving", (_moveInput.magnitude != 0));
        _playerRigidbody.velocity = _moveInput * Speed.i;
    }

    public void OnLook(InputValue value)
    {
        Vector2 worldPos = _mainCam.ScreenToWorldPoint(value.Get<Vector2>());
        Vector2 newAim = (worldPos - (Vector2)transform.position).normalized;

        if (newAim.magnitude >= 0.5f)
        {
            _rotZ = Mathf.Atan2(newAim.y, newAim.x) * Mathf.Rad2Deg;
            _weaponRenderer.flipY = Mathf.Abs(_rotZ) > 90f;
            _playerRenderer.flipX = Mathf.Abs(_rotZ) > 90f;
            _weaponTransform.rotation = Quaternion.Euler(0, 0, _rotZ);
        }
    }

    public void OnAttack(InputValue value)
    {
        if (value.isPressed && _canAttack)
        {
            //Instantiate(_bullet, _bulletSpawnPoint.position, Quaternion.Euler(0, 0, _rotZ - 90));
            _bulletController.CreatTeiredBullet(_rotZ - 90, 2);
            StartCoroutine(AttackCoolTimeCo());
        }
    }

    private IEnumerator AttackCoolTimeCo()
    {
        _canAttack = false;

        yield return new WaitForSecondsRealtime(PlayerAttackCooltime.f);

        _canAttack = true;
    }

    public void OnScroll(InputValue value)
    {
        float f = value.Get<float>();

        if (f > 0)
        {
            _mainCam.orthographicSize -= (_mainCam.orthographicSize > MIN_CAMERA_DISTANCE) ? 0.5f : 0f;
        }
        else if (f < 0)
        {
            _mainCam.orthographicSize += (_mainCam.orthographicSize < MAX_CAMERA_DISTANCE) ? 0.5f : 0f;
        }
    }

    public void OnRoll(InputValue value)
    {
        if (value.isPressed && !_isRoll)
        {
            _playerAnimator.SetTrigger("IsRoll");
            EventPlayerRoll.Raise();

            StartCoroutine(RollCo());
        }
    }

    private IEnumerator RollCo()
    {
        _isInvincible = true;
        _isRoll = true;
        _playerRigidbody.velocity = _moveInput * Speed.i * 2;

        yield return new WaitForSecondsRealtime(0.3f);

        _isInvincible = false;
        _playerRigidbody.velocity = _moveInput * Speed.i;

        yield return new WaitForSecondsRealtime(PlayerRollCooltime.f);
        _isRoll = false;
    }

    public void OnHeal()
    {
        if (HP.i < MaxHP.i)
            HP.Change(1);
    }

    public void OnSpeedUP()
    {
        StartCoroutine(SpeedUPCo());
    }

    private IEnumerator SpeedUPCo()
    {
        Speed.Change(SpeedUpAmount.i);

        yield return new WaitForSecondsRealtime(SpeedUpTime.f);

        Speed.Change(-SpeedUpAmount.i);
    }

    public void OnInvincible()
    {
        if (_isInvincible)
            return;

        StartCoroutine(InvincibleCo());
    }

    private IEnumerator InvincibleCo()
    {
        _isInvincible = true;
        _playerRenderer.color = new Color32(255, 255, 255, 100);

        yield return new WaitForSecondsRealtime(InvincibleTime.f);

        _isInvincible = false;
        _playerRenderer.color = Color.white;
    }

    public void SetBullet(GameObject bullet)
    {
        _bullet = bullet;
    }
}
