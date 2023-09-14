using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTierController : MonoBehaviour
{
    private float _bulletCoolTime = 0.12f;
    private GameObject _bullet;

    [SerializeField] private Transform _bulletSpawnPoint;

    public void CreatTieredBullet(GameObject bullet, float rotZ, int tier)
    {
        _bullet = bullet;
        switch (tier)
        {
            case 1:
                TierOne(rotZ);
                break;
            case 2:
                StartCoroutine(TierTwoCo(rotZ));
                break;
            case 3:
                StartCoroutine(TierThree(rotZ));
                break;
            case 4:
                StartCoroutine(TierFour(rotZ));
                break;
            default:
                TierOne(rotZ);
                break;
        }
    }

    private void TierOne(float rotZ)
    {
        Instantiate(_bullet, _bulletSpawnPoint.position, Quaternion.Euler(0, 0, rotZ));
    }

    private IEnumerator TierTwoCo(float rotZ)
    {
        Instantiate(_bullet, _bulletSpawnPoint.position, Quaternion.Euler(0, 0, rotZ + 5));
        yield return new WaitForSecondsRealtime(_bulletCoolTime);
        Instantiate(_bullet, _bulletSpawnPoint.position, Quaternion.Euler(0, 0, rotZ - 5));
    }

    private IEnumerator TierThree(float rotZ)
    {
        Instantiate(_bullet, _bulletSpawnPoint.position, Quaternion.Euler(0, 0, rotZ + 5));
        yield return new WaitForSecondsRealtime(_bulletCoolTime);
        Instantiate(_bullet, _bulletSpawnPoint.position, Quaternion.Euler(0, 0, rotZ));
        yield return new WaitForSecondsRealtime(_bulletCoolTime);
        Instantiate(_bullet, _bulletSpawnPoint.position, Quaternion.Euler(0, 0, rotZ - 5));
    }

    private IEnumerator TierFour(float rotZ)
    {
        Instantiate(_bullet, _bulletSpawnPoint.position, Quaternion.Euler(0, 0, rotZ + 10));
        yield return new WaitForSecondsRealtime(_bulletCoolTime);
        Instantiate(_bullet, _bulletSpawnPoint.position, Quaternion.Euler(0, 0, rotZ + 5));
        yield return new WaitForSecondsRealtime(_bulletCoolTime);
        Instantiate(_bullet, _bulletSpawnPoint.position, Quaternion.Euler(0, 0, rotZ));
        yield return new WaitForSecondsRealtime(_bulletCoolTime);
        Instantiate(_bullet, _bulletSpawnPoint.position, Quaternion.Euler(0, 0, rotZ - 5));
        yield return new WaitForSecondsRealtime(_bulletCoolTime);
        Instantiate(_bullet, _bulletSpawnPoint.position, Quaternion.Euler(0, 0, rotZ - 10));
    }
}