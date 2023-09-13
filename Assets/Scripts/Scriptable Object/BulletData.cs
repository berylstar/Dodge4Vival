using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Bullet")]
public class BulletData : ScriptableObject
{
    public int damage;
    public float speed;
    public AudioClip sound;
}
