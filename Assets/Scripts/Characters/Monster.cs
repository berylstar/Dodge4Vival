using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Character
{
    [Header("Status")]
    public int HP;
    public float speed;

    private Rigidbody2D _rb;
    private BoxCollider2D _bc;
    private SpriteRenderer _sr;
    private Animator _ani;

    private Transform _target;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _bc = GetComponent<BoxCollider2D>();
        _sr = GetComponent<SpriteRenderer>();
        _ani = GetComponent<Animator>();
    }

    private void Start()
    {
        OnMoveEvent += MonsterMove;
    }

    protected override IEnumerator Hit()
    {
        yield return null;
    }

    private void MonsterMove(Vector2 dir)
    {
        Debug.Log("Monster Move");
    }
}
