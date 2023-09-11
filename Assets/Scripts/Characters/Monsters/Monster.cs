using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    [Header("Status")]
    public int HP;
    public float speed;

    protected Rigidbody2D _rb;
    private PolygonCollider2D _col;
    protected SpriteRenderer _sr;
    private Animator _ani;

    protected Transform _target;

    protected bool _isHit = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<PolygonCollider2D>();
        _sr = GetComponent<SpriteRenderer>();
        _ani = GetComponent<Animator>();

        _target = GameObject.Find("Player").transform;
    }

    protected abstract void Move(Vector3 dir);

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
        else if (collision.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }

    private IEnumerator Disappear()
    {
        _isHit = true;
        _col.enabled = false;
        _sr.color = new Color32(255, 255, 255, 100);

        yield return new WaitForSecondsRealtime(0.5f);

        _sr.color = new Color32(255, 255, 255, 50);

        yield return new WaitForSecondsRealtime(0.5f);

        Destroy(this.gameObject);
    }

    IEnumerator HitCo()
    {
        _ani.SetTrigger("IsHit");

        _isHit = true;
        _sr.color = new Color32(200, 100, 100, 255);

        yield return new WaitForSecondsRealtime(0.2f);

        _isHit = false;
        _sr.color = Color.white;
    }
}
