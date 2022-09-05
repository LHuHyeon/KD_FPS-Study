using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakAnimal : Animal
{
    // 뛰기 (피격 될때만)
    public void Run(Vector3 _targetPos)
    {
        direction = Quaternion.LookRotation(transform.position - _targetPos).eulerAngles;

        applySpeed = runSpeed;
        currentTime = runTime;
        isWalking = false;
        isRunning = true;

        anim.SetBool("Running", isRunning);
    }

    public override void Damage(int _dmg, Vector3 _targetPos)
    {
        base.Damage(_dmg, _targetPos);
        if (!isDead)
            Run(_targetPos);
    }
}
