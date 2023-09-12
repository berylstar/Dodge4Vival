using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] private GameEvent _gameEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (_gameEvent != null)
                _gameEvent.Raise();
            Destroy(gameObject);
        }
    }
}
