using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Status")]
    public BulletData data;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") && !collision.CompareTag("Item") && !collision.CompareTag("Trap") && !collision.CompareTag("Bullet"))
        {
            Destroy(this.gameObject);
        }
    }

    // 발사체는 일직선으로 발사
    private void FixedUpdate()
    {
        transform.Translate(data.speed * Time.deltaTime * Vector2.up);
    }
}