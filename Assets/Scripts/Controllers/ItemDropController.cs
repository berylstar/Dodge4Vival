using UnityEngine;
using UnityEngine.UI;

public class ItemDropController : MonoBehaviour
{
    [SerializeField] private ItemData[] _items;
    [SerializeField] private GameObject _item;
    [SerializeField] private GameObject _monster;

    public void DropRandomItem()
    {
        int index = Random.Range(0, _items.Length);
        _item.GetComponent<SpriteRenderer>().sprite = _items[index].Sprite;
        _item.GetComponent<Item>().SetItemData(_items[index]);
        Vector2 newPosition = _monster.transform.position;
        Instantiate(_item, newPosition, Quaternion.identity, transform);
    }
}
