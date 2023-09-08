using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected event Action<Vector2> OnMoveEvent;
    protected event Action<Vector2> OnLookEvent;
    protected event Action OnAttackEvent;
    protected event Action OnSkillEvent;

    protected void CallMoveEvent(Vector2 dir)
    {
        OnMoveEvent?.Invoke(dir);
    }

    protected void CallLookEvent(Vector2 dir)
    {
        OnLookEvent?.Invoke(dir);
    }

    protected void CallAttackEvent()
    {
        OnAttackEvent?.Invoke();
    }

    protected void CallSkillEvent()
    {
        OnSkillEvent?.Invoke();
    }

    protected abstract IEnumerator Hit();
}
