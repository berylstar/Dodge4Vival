using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrapType
{
    Spike,
}

public class Trap : MonoBehaviour
{
    public TrapType type;
    [SerializeField] private BoxCollider2D _bc;

    public void Effect(Player player)
    {
        switch (type)
        {
            case TrapType.Spike:
                player.EventPlayerHit.Raise();
                break;

            default:
                break;
        }
    }
}
