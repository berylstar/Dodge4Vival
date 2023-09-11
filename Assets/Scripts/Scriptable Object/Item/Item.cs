using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemData _item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && _item.ItemEvent != null)
        {
            _item.ItemEvent.Raise();
            Destroy(gameObject);
        }
    }
}
