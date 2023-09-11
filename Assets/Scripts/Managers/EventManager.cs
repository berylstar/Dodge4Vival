using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static EventManager I;

    [Header("Events")]
    public UnityEvent PlayerHitEvent;
    public UnityEvent PlayerDieEvent;
    public UnityEvent PlayerHealingEvent;
    public UnityEvent PlayerLowHPEvent;
    public UnityEvent MonsterAllDestroyed;
    public UnityEvent MonsterDieEvent;

    private void Awake()
    {
        I = this;
    }
}
