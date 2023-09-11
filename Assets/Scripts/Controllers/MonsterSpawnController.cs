using System.Collections;
using UnityEngine;

public class MonsterSpawnController : MonoBehaviour
{
    [SerializeField] private GameObject _monster;
    [SerializeField] private IntVariable _fullCount;
    private Vector2 _spawnPosition = Vector2.zero;
    private float _spawnCooldownTime = 1f;
    private bool _canSpawn = true;

    private void Start()
    {
        StartCoroutine(Spawn(_fullCount.i, _spawnPosition, _spawnCooldownTime));
    }

    private IEnumerator Spawn(int fullCount, Vector2 spawnPosition, float spawnCooldownTime)
    {
        while (_canSpawn)
        {
            if (transform.childCount < fullCount)
            {

                float x = Random.Range(-8.0f, 8.0f);
                float y = Random.Range(-5.0f, 5.0f);
                spawnPosition.x += x;
                spawnPosition.y += y;
                Instantiate(_monster, spawnPosition, Quaternion.identity, transform);
            }

            yield return new WaitForSecondsRealtime(spawnCooldownTime);
        }
    }

    public void DestroyAllMonster()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    #region Set
    public void SetSpawnPosition(Vector2 spawnPosition)
    {
        _spawnPosition = spawnPosition;
    }

    public void SetSpawnCooldownTime(float spawnCooldownTime)
    {
        if (spawnCooldownTime >= 0)
        {
            _spawnCooldownTime = spawnCooldownTime;
        }
    }
    public void SetCanSpawn(bool canSpawn)
    {
        _canSpawn = canSpawn;
    }
    #endregion
}
