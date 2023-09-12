using UnityEngine;

public class ItemDropController : MonoBehaviour
{
    [SerializeField] private GameObject[] _items;
    [SerializeField] private GameObject _item;

    [SerializeField] public FloatVariable MonsterDiePositionX;
    [SerializeField] public FloatVariable MonsterDiePositionY;

    [SerializeField] private Vector2 _newPosition = Vector2.zero;

    public void DropRandomItem()
    {
        int index = GetRandomIndex();
        if(index >= 0)
        {
            _newPosition = new Vector2(MonsterDiePositionX.f, MonsterDiePositionY.f);
            Instantiate(_items[index], _newPosition, Quaternion.identity, transform);
        }
    }

    private int GetRandomIndex()
    {
        int index = -1;
        int percentNum = Random.Range(0, 100);
        if (percentNum < 50)
        {
            if (percentNum < 20)
                index = 0;
            else if (percentNum < 40)
                index = 1;
            else
                index = 2;

            if (index >= _items.Length)
                index = 0;
        }
        return index;
    }
}
