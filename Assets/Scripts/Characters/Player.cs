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

    [Header("Player")]
    [SerializeField] private SpriteRenderer _playerRenderer;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private Rigidbody2D _playerRigidbody;

    private bool _canAttack = true;
    private bool _invincible = false;
    private float _rotZ = 0f;

    [Header("Weapon")]
    [SerializeField] private SpriteRenderer _weaponRenderer;
    [SerializeField] private Transform _weaponTransform;

    [Header("Bullet")]
    public GameObject bullet;
    [SerializeField] private Transform _bulletSpawnPoint;

    [Header("Variable")]
    public IntVariable HP;
    public IntVariable MaxHP;
    public IntVariable LowHP;
    public IntVariable PlayerSpeed;
    public FloatVariable PlayerAttackCooltime;

    [Header("Event")]
    public GameEvent OnPlayerHit;
    public GameEvent OnPlayerDie;
    public GameEvent OnPlayerHeal;
    public GameEvent OnPlayerLowHP;
    public GameEvent OnPlayerInvincible;

    private void Awake()
    {
        I = this;
        HP.Set(MaxHP.i);
        _mainCam = Camera.main;
    }

    private void FixedUpdate()
    {
        _mainCam.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster") && !_invincible)
        {
            StartCoroutine(HitCo());
        }
    }

    private IEnumerator HitCo()
    {
        HP.i -= 1;

        if (HP.i <= 0)
        {
            HP.i = 0;
            OnPlayerDie.Raise();
        }
        else
        {
            OnPlayerHit.Raise();
        }

        if (HP.i <= LowHP.i)
            OnPlayerLowHP.Raise();

        _playerAnimator.SetTrigger("IsHit");
        _playerRenderer.color = new Color32(200, 100, 100, 255);
        _invincible = true;
        yield return new WaitForSecondsRealtime(1f);
        _playerRenderer.color = Color.white;
        _invincible = false;
    }

    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>().normalized;

        _playerAnimator.SetBool("IsMoving", (moveInput.magnitude != 0));
        _playerRigidbody.velocity = moveInput * PlayerSpeed.i;
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
            Instantiate(bullet, _bulletSpawnPoint.position, Quaternion.Euler(0, 0, _rotZ - 90));
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

    public void OnSkill(InputValue value)
    {
        if (value.isPressed)
        {
            //Debug.Log("SKILL");
            OnPlayerHeal.Raise();
        }
    }

    public void OnHeal()
    {
        if (HP.i <= MaxHP.i)
            HP.Change(1);
    }
}
