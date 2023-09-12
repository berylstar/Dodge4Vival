using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _bulletSpawnPoint;

    public void CreatTeiredBullet(float rotZ, int teir)
    {
        switch (teir)
        {
            case 1:
                TeirOne(rotZ);
                break;
            case 2:
                TeirTwo(rotZ);
                break;
            case 3:
                TeirThree(rotZ);
                break;
            case 4:
                TeirFour(rotZ);
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

    private void TeirTwo(float rotZ)
    {
        Instantiate(_bullet, _bulletSpawnPoint.position, Quaternion.Euler(0, 0, rotZ + 10));
        Instantiate(_bullet, _bulletSpawnPoint.position, Quaternion.Euler(0, 0, rotZ - 10));
    }

    private void TeirThree(float rotZ)
    {

    }

    private void TeirFour(float rotZ)
    {

    }
}
