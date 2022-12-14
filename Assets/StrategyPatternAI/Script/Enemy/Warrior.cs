using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Enemy
{
    [SerializeField]
    private CircleCollider2D attackRange;

    public Warrior(AI _context) : base(_context)
    {

    }

    public override void Move()
    {
        base.Move();
    }

    public override void Attack()
    {

    }

    public override void Die()
    {
        base.Die();
    }


}
