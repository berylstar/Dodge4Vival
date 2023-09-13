using UnityEngine;

public class ItemDropController : MonoBehaviour
{
    [SerializeField] private GameObject[] _items;

    [SerializeField] public FloatVariable MonsterDiePositionX;
    [SerializeField] public FloatVariable MonsterDiePositionY;

    private Vector2 _newPosition = Vector2.zero;

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
        switch (Random.Range(0f, 1f))
        {
            case < 0.002f:     return 0;
            case < 0.03f:      return 1;
            case < 0.06f:      return 2;
            case < 0.11f:      return 3;
            case < 0.16f:      return 4;
            case < 0.21f:      return 5;
            case < 0.31f:      return 6;
            default:           return -1;
        }
    }
}
