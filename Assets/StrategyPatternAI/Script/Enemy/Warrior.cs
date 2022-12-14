using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Enemy
{
    [SerializeField]
    private CircleCollider2D attackRange;

    public override void Move(Transform mytransform, Transform targetTransform)
    {
        base.Move(mytransform, targetTransform);
    }

    public override void Attack()
    {

    }

    public override void Die()
    {
        base.Die();
    }


}
