using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player I;

    private event Action<Vector2> OnMoveEvent;
    private event Action<Vector2> OnLookEvent;
    private event Action OnAttackEvent;
    private event Action<float> OnScrollEvent;
    private event Action OnSkillEvent;

    private Camera _mainCam;
    private readonly int MIN_CAMERA_DISTANCE = 2;
    private readonly int MAX_CAMERA_DISTANCE = 15;

    [Header("Player")]
    [SerializeField] private SpriteRenderer _playerRenderer;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private Rigidbody2D _playerRigidbody;

    public IntVariable playerSpeed;
    public FloatVariable attackCooltime;
    private bool _canAttack = true;

    private float _rotZ = 0f;

    [Header("Weapon")]
    [SerializeField] private SpriteRenderer _weaponRenderer;
    [SerializeField] private Transform _weaponTransform;

    [Header("Bullet")]
    public GameObject bullet;
    [SerializeField] private Transform _bulletSpawnPoint;

    private void Awake()
    {
        I = this;

        _mainCam = Camera.main;
    }

    private void Start()
    {
        OnMoveEvent += MoveEvent;
        OnLookEvent += LookEvent;
        OnAttackEvent += AttackEvent;
        OnScrollEvent += ScrollEvent;

        OnSkillEvent += SkillEvent;
    }

    private void FixedUpdate()
    {
        // 카메라가 플레이어를 따라 이동
        _mainCam.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            StartCoroutine(HitCo());
        }
    }

    private IEnumerator HitCo()
    {
        _playerAnimator.SetTrigger("IsHit");
        yield return null;
    }

    #region MOVE
    private void CallMoveEvent(Vector2 dir)
    {
        OnMoveEvent?.Invoke(dir);
    }

    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>().normalized;
        CallMoveEvent(moveInput);
    }

    private void MoveEvent(Vector2 direction)
    {
        _playerAnimator.SetBool("IsMoving", (direction.magnitude != 0));
        _playerRigidbody.velocity = direction * playerSpeed.i;
    }
    #endregion

    #region LOOK
    private void CallLookEvent(Vector2 dir)
    {
        OnLookEvent?.Invoke(dir);
    }

    public void OnLook(InputValue value)
    {
        Vector2 worldPos = _mainCam.ScreenToWorldPoint(value.Get<Vector2>());
        Vector2 newAim = (worldPos - (Vector2)transform.position).normalized;

        if (newAim.magnitude >= 0.5f)
            CallLookEvent(newAim);
    }

    private void LookEvent(Vector2 direction)
    {
        _rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _weaponRenderer.flipY = Mathf.Abs(_rotZ) > 90f;
        _playerRenderer.flipX = Mathf.Abs(_rotZ) > 90f;
        _weaponTransform.rotation = Quaternion.Euler(0, 0, _rotZ);
    }
    #endregion

    #region ATTACK
    private void CallAttackEvent()
    {
        OnAttackEvent?.Invoke();
    }

    public void OnAttack(InputValue value)
    {
        if (value.isPressed)
        {
            CallAttackEvent();
        }
    }

    private void AttackEvent()
    {
        if (_canAttack)
        {
            Instantiate(bullet, _bulletSpawnPoint.position, Quaternion.Euler(0, 0, _rotZ - 90));
            StartCoroutine(ShootCoolTimeCo());
        }
    }

    private IEnumerator ShootCoolTimeCo()
    {
        _canAttack = false;
        yield return new WaitForSecondsRealtime(attackCooltime.f);
        _canAttack = true;
    }
    #endregion

    #region SCROLL
    protected void CallScrollEvent(float value)
    {
        OnScrollEvent?.Invoke(value);
    }

    // InputSystem의 Scroll
    public void OnScroll(InputValue value)
    {
        CallScrollEvent(value.Get<float>());
    }

    private void ScrollEvent(float value)
    {
        if (value > 0)
        {
            _mainCam.orthographicSize -= (_mainCam.orthographicSize > MIN_CAMERA_DISTANCE) ? 0.5f : 0f;
        }
        else if (value < 0)
        {
            _mainCam.orthographicSize += (_mainCam.orthographicSize < MAX_CAMERA_DISTANCE) ? 0.5f : 0f;
        }
    }
    #endregion

    #region SKill
    private void CallSkillEvent()
    {
        OnSkillEvent?.Invoke();
    }

    public void OnSkill(InputValue value)
    {
        if (value.isPressed)
        {
            CallSkillEvent();
        }
    }

    private void SkillEvent()
    {
        Debug.Log("SKILL");
    }
    #endregion
}
