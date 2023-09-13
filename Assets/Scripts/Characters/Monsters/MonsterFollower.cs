using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFollower : Monster
{
    private void FixedUpdate()
    {
        Vector3 targetVector = (Vector3)data.player.pos - transform.position;

        _sr.flipX = targetVector.x < 0;

        if (targetVector.magnitude < 0.25f || _isHit)
            _rb.velocity = Vector2.zero;
        else
            Move(targetVector);
    }

    protected override void Move(Vector3 dir)
    {
        _rb.velocity = (speed * dir.normalized);
    }
}
