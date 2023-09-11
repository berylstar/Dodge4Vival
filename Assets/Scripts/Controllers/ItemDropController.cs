using UnityEngine;
using UnityEngine.UI;

public class ItemDropController : MonoBehaviour
{
    [SerializeField] private ItemData[] _items;
    [SerializeField] private GameObject _item;

    [SerializeField] public FloatVariable MonsterDiePositionX;
    [SerializeField] public FloatVariable MonsterDiePositionY;

    [SerializeField] private Vector2 _newPosition = Vector2.zero;

    public void DropRandomItem()
    {
        int index = Random.Range(0, _items.Length);
        _item.GetComponent<SpriteRenderer>().sprite = _items[index].Sprite;
        _item.GetComponent<Item>().SetItemData(_items[index]);
        _newPosition = new Vector2(MonsterDiePositionX.f, MonsterDiePositionY.f);
        Instantiate(_item, _newPosition, Quaternion.identity, transform);
    }
    
}
