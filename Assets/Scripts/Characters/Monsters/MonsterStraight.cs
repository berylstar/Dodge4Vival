using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStraight : Monster
{
    protected override void Move(Vector3 dir)
    {
        _rb.velocity = (speed * dir.normalized);
    }

    private void Start()
    {
        Vector3 targetVector = (Vector3)data.player.pos - transform.position;

        _sr.flipX = targetVector.x < 0;

        Move(targetVector);
    }
}
