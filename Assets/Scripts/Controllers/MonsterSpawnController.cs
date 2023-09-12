using System.Collections;
using UnityEngine;

public class MonsterSpawnController : MonoBehaviour
{
    public IntVariable MonsterFullCount;
    public FloatVariable SpawnCooldownTime;

    [SerializeField] private GameObject[] _monsters;
    [SerializeField] private GameObject _player;

    private Vector2 _spawnPosition;
    private bool _canSpawn = true;

    private void Start()
    {
        _spawnPosition = _player.transform.position;
        StartCoroutine(Spawn(MonsterFullCount.i, SpawnCooldownTime.f));
    }

    private IEnumerator Spawn(int fullCount, float spawnCooldownTime)
    {
        while (_canSpawn)
        {
            _spawnPosition = _player.transform.position;
            if (transform.childCount < fullCount)
            {
                float x = RandomPositionValue();
                float y = RandomPositionValue();
                _spawnPosition.x += x;
                _spawnPosition.y += y;
                Instantiate(_monsters[0], _spawnPosition, Quaternion.identity, transform);
            }

            yield return new WaitForSecondsRealtime(spawnCooldownTime);
        }
    }

    public void DestroyAllMonster()
    {
        foreach (Transform child in transform)
        {
            if(child.CompareTag(_monsters[0].tag))
                Destroy(child.gameObject);
        }
    }

    private float RandomPositionValue()
    {
        float value = Random.Range(-8.0f, 8.0f);
        if (value <= 0 && -2f < value)
            value = -3f;
        if (value > 0 && 2f > value)
            value = 3f;
        return value;
    }

    #region Set
    public void SetSpawnPosition(Vector2 spawnPosition)
    {
        _spawnPosition = spawnPosition;
    }

    public void SetCanSpawn(bool canSpawn)
    {
        _canSpawn = canSpawn;
    }
    #endregion
}
