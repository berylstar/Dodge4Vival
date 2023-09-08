using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnController : MonoBehaviour
{
    [SerializeField] private GameObject _monster;
    private Vector2 _spawnPosition = Vector2.zero;
    private bool _canSpawn = true;
    private int _fullCount = 0;

    private void Start()
    {
        StartCoroutine(Spawn(_fullCount, _spawnPosition));
    }

    private IEnumerator Spawn(int fullCount, Vector2 spawnPosition)
    {
        while (_canSpawn)
        {
            if (transform.childCount <= fullCount)
            {

                float x = Random.Range(-8.0f, 8.0f);
                float y = Random.Range(-5.0f, 5.0f);
                spawnPosition.x += x;
                spawnPosition.y += y;
                Instantiate(_monster, spawnPosition, Quaternion.identity, transform);
            }

            yield return new WaitForSecondsRealtime(3f);
        }
    }

    public void SetFullCount(int fullCount)
    {
        if(fullCount >= 0) {
            _fullCount = fullCount;
        }
    }

    public void SetSpawnPosition(Vector2 spawnPosition)
    {
        _spawnPosition = spawnPosition;
    }

}
