using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    private event Action<float> OnScrollEvent;

    private Camera _mainCam;
    private readonly int MIN_CAMERA_DISTANCE = 2;
    private readonly int MAX_CAMERA_DISTANCE = 15;

    [Header("Player")]
    [SerializeField] private SpriteRenderer _playerRenderer;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private Rigidbody2D _playerRigidbody;

    public float playerSpeed = 5f;
    public float attackCooltime = 3f;
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
        _mainCam = Camera.main;
    }

    private void Start()
    {
        OnMoveEvent += MoveEvent;
        OnLookEvent += LookEvent;
        OnAttackEvent += AttackEvent;
        OnScrollEvent += ScrollEvent;

        // OnSkillEvent += SkillEvent;      스킬 얻으면 그때 추가
    }

    private void Update()
    {
        // 카메라가 플레이어를 따라 이동
        _mainCam.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

    #region MOVE
    // InputSystem의 Move
    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>().normalized;
        CallMoveEvent(moveInput);
    }

    private void MoveEvent(Vector2 direction)
    {
        _playerAnimator.SetBool("IsMoving", (direction.magnitude != 0));
        _playerRigidbody.velocity = direction * playerSpeed;
    }
    #endregion

    #region LOOK
    // InputSystem의 Look
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

    #region SHOOT
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
        yield return new WaitForSecondsRealtime(attackCooltime);
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
    // InputSystem의 Skill
    public void OnSkill(InputValue value)
    {
        if (value.isPressed)
        {
            CallSkillEvent();
        }
    }

    private void SkillEvent()
    {

    }
    #endregion
}
