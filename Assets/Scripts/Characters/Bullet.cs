using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Status")]
    public int damage;
    public float speed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") && !collision.CompareTag("Item"))
        {
            Destroy(this.gameObject);
        }
    }

    // 발사체는 일직선으로 발사
    private void FixedUpdate()
    {
        transform.Translate(speed * Time.deltaTime * Vector2.up);
    }
}
