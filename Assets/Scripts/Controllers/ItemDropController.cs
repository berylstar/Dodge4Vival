using UnityEngine;

public class ItemDropController : MonoBehaviour
{
    [SerializeField] private ItemProbabilityData data;
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
        int iRand = Random.Range(0, 100);
        Debug.Log(iRand);

        if      (iRand <= data.bomb)                                            return 0;
        else if (data.bomb < iRand && iRand <= data.invinciblePotion)           return 1;
        else if (data.invinciblePotion < iRand && iRand <= data.axe)            return 2;
        else if (data.axe < iRand && iRand <= data.sword)                       return 3;
        else if (data.healingPotion < iRand && iRand <= data.healingPotion)     return 4;
        else if (data.speedUpPotion < iRand && iRand <= data.speedUpPotion)     return 5;
        else if (data.bow < iRand && iRand <= data.bow)                         return 6;
        else                                                                    return - 1;
    }
}
