using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Monster")]
public class MonsterData : ScriptableObject
{
    public int hp;
    public float speed;
    public Vector2Variable player;
}
