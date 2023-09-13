using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Monster : MonoBehaviour
{
    [Header("Status")]
    public MonsterData data;
    protected int HP;
    protected float speed;

    [Header("Event")]
    public GameEvent EventMonsterDie;

    [SerializeField] protected Rigidbody2D _rb;
    [SerializeField] private PolygonCollider2D _col;
    [SerializeField] protected SpriteRenderer _sr;
    [SerializeField] private Animator _ani;
    [SerializeField] public FloatVariable MonsterDiePositionX;
    [SerializeField] public FloatVariable MonsterDiePositionY;

    protected bool _isHit = false;

    private void Awake()
    {
        HP = data.hp;
        speed = data.speed;
    }

    protected abstract void Move(Vector3 dir);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            HP -= collision.GetComponent<Bullet>().data.damage;

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
        _rb.velocity = Vector2.zero;
        _sr.color = new Color32(255, 255, 255, 100);

        yield return new WaitForSecondsRealtime(0.5f);

        _sr.color = new Color32(255, 255, 255, 50);

        yield return new WaitForSecondsRealtime(0.5f);

        Destroy();
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

    private void Destroy()
    {
        MonsterDiePositionX.Set(transform.position.x);
        MonsterDiePositionY.Set(transform.position.y);
        EventMonsterDie.Raise();
        Destroy(this.gameObject);
    }
}
