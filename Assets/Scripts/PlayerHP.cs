using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHP : MonoBehaviour
{
    public IntVariable HP;

    public IntVariable StartingHP;
    public UnityEvent DamageEvent;
    public UnityEvent DeathEvent;
    public UnityEvent HealingEvent;

    private void Start()
    {
        HP.SetValue(StartingHP);
    }

    public void OnTakeDamage()
    {
        HP.ApplyChange(-1);
        DamageEvent.Invoke();

        if (HP.Value <= 0)
        {
            HP.SetValue(0);
            DeathEvent.Invoke();
        }
    }

    public void OnHealing()
    {
        HP.ApplyChange(1);
        HealingEvent.Invoke();
    }

}
