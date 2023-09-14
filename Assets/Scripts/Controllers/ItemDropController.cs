using UnityEngine;

public class ItemDropController : MonoBehaviour
{
    [SerializeField] private ItemProbabilityData data;
    [SerializeField] private GameObject[] _items;

    [SerializeField] public FloatVariable MonsterDiePositionX;
    [SerializeField] public FloatVariable MonsterDiePositionY;

    public void DropRandomItem()
    {
        int index = GetRandomIndex();
        Debug.Log(index);

        if (index == -1)
            return;
        
        Instantiate(_items[index], new Vector2(MonsterDiePositionX.f, MonsterDiePositionY.f), Quaternion.identity, transform);
        
    }

    private int GetRandomIndex()
    {
        int iRand = Random.Range(0, 100);

        if      (iRand < data.bomb)                 return 0;
        else if (iRand < data.invinciblePotion)     return 1;
        else if (iRand < data.axe)                  return 2;
        else if (iRand < data.sword)                return 3;
        else if (iRand < data.healingPotion)        return 4;
        else if (iRand < data.speedUpPotion)        return 5;
        else if (iRand < data.bow)                  return 6;
        else                                        return -1;
    }
}
