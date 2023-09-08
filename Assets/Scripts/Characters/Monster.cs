using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Character
{
    [Header("Status")]
    public int HP;
    public float speed;

    private Rigidbody2D _rb;
    private PolygonCollider2D _col;
    private SpriteRenderer _sr;
    private Animator _ani;

    private Transform _target;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<PolygonCollider2D>();
        _sr = GetComponent<SpriteRenderer>();
        _ani = GetComponent<Animator>();
    }

    private void Start()
    {
        OnMoveEvent += MonsterMove;

        _target = Player.I.transform;

        CallMoveEvent(Vector2.up);
    }

    private void FixedUpdate()
    {
        Vector3 targetVector = _target.position - transform.position;

        _sr.flipX = targetVector.x < 0;

        if (targetVector.magnitude < 1f)
            _rb.velocity = Vector2.zero;
        else
            _rb.velocity = (speed * targetVector.normalized);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            HP -= 1;

            if (HP <= 0)
                StartCoroutine(Disappear());
            else
                StartCoroutine(HitCo());
        }
    }

    private IEnumerator Disappear()
    {
        speed = 0f;
        _col.enabled = false;
        _sr.color = new Color32(255, 255, 255, 100);

        yield return new WaitForSecondsRealtime(0.5f);

        _sr.color = new Color32(255, 255, 255, 50);

        yield return new WaitForSecondsRealtime(0.5f);

        Destroy(this.gameObject);
    }

    protected override IEnumerator HitCo()
    {
        _ani.SetTrigger("IsHit");

        float temp = speed;
        speed = 0f;
        _sr.color = new Color32(200, 100, 100, 255);

        yield return new WaitForSecondsRealtime(0.2f);

        speed = temp;
        _sr.color = Color.white;
    }

    private void MonsterMove(Vector2 dir)
    {
        Debug.Log("Monster Move");
    }
}
