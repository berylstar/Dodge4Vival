using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHP : MonoBehaviour
{
    public IntVariable HP;
    public IntVariable StartingHP;
    public IntVariable LowHP;

    [Header("Events")]
    public UnityEvent DamageEvent;
    public UnityEvent DeathEvent;
    public UnityEvent HealingEvent;
    public UnityEvent LowHPEvent;

    private void Start()
    {
        HP.i = StartingHP.i;
    }

    public void OnTakeDamage()
    {
        HP.i -= 1;
        DamageEvent.Invoke();

        if (HP.i <= 0)
        {
            HP.i = 0;
            DeathEvent.Invoke();
        }
        else if (HP.i <= LowHP.i)
        {
            LowHPEvent.Invoke();
        }
    }

    public void OnHealing()
    {
        // HP.i += 1;
        HealingEvent.Invoke();
    }

}
