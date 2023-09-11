using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHP : MonoBehaviour
{
    public IntVariable HP;
    public IntVariable StartingHP;
    public IntVariable LowHP;
    public FloatVariable InvincibleTime;

    public UnityEvent DamageEvent;
    public UnityEvent DeathEvent;
    public UnityEvent HealingEvent;
    public UnityEvent LowHPEvent;

    private bool _isInvincible = false;
    private Coroutine _coroutine;

    private void Start()
    {
        HP.SetValue(StartingHP);
    }

    public void OnTakeDamage()
    {
        if (!_isInvincible)
        {
            HP.ApplyChange(-1);
            DamageEvent.Invoke();
        }


        if (HP.Value > 0 && HP.Value <= LowHP.Value)
        {
            LowHPEvent.Invoke();
        }

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

    public void OnInvencible()
    {
        if(_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _coroutine = StartCoroutine(Invincible());
    }

    private IEnumerator Invincible()
    {
        _isInvincible = true;
        yield return new WaitForSecondsRealtime(InvincibleTime.Value);
        _isInvincible = false;
    }
}
