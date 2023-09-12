using UnityEngine;

public class ItemDropController : MonoBehaviour
{
    [SerializeField] private GameObject[] _items;

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
        if (percentNum < 55)
        {
            if (percentNum < 10)
                index = 0;
            else if (percentNum < 20)
                index = 1;
            else if (percentNum < 25)
                index = 2;
            else if(percentNum < 35)
                index = 3;
            else if (percentNum < 45)
                index = 4;
            else if (percentNum < 55)
                index = 5;


            if (index >= _items.Length)
                index = 0;
        }
        return index;
    }
}
