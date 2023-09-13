using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFireController : MonoBehaviour
{
    private float _bulletCoolTime = 0.12f;
    private GameObject _bullet;

    [SerializeField] private Transform _bulletSpawnPoint;

    public void CreatTeiredBullet(GameObject bullet, float rotZ, int tier)
    {
        _bullet = bullet;
        switch (tier)
        {
            case 1:
                TeirOne(rotZ);
                break;
            case 2:
                StartCoroutine(TeirTwoCo(rotZ));
                break;
            case 3:
                StartCoroutine(TeirThree(rotZ));
                break;
            case 4:
                StartCoroutine(TeirFour(rotZ));
                break;
            default:
                TeirOne(rotZ);
                break;
        }
    }

    private void TeirOne(float rotZ)
    {
        Instantiate(_bullet, _bulletSpawnPoint.position, Quaternion.Euler(0, 0, rotZ));
    }

    private IEnumerator TeirTwoCo(float rotZ)
    {
        Instantiate(_bullet, _bulletSpawnPoint.position, Quaternion.Euler(0, 0, rotZ + 5));
        yield return new WaitForSecondsRealtime(_bulletCoolTime);
        Instantiate(_bullet, _bulletSpawnPoint.position, Quaternion.Euler(0, 0, rotZ - 5));
    }

    private IEnumerator TeirThree(float rotZ)
    {
        Instantiate(_bullet, _bulletSpawnPoint.position, Quaternion.Euler(0, 0, rotZ + 5));
        yield return new WaitForSecondsRealtime(_bulletCoolTime);
        Instantiate(_bullet, _bulletSpawnPoint.position, Quaternion.Euler(0, 0, rotZ));
        yield return new WaitForSecondsRealtime(_bulletCoolTime);
        Instantiate(_bullet, _bulletSpawnPoint.position, Quaternion.Euler(0, 0, rotZ - 5));
    }

    private IEnumerator TeirFour(float rotZ)
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
