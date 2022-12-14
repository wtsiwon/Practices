using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assasin : Enemy
{
    public Assasin(AI _context) : base(_context)
    {

    }



    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    public override void Die()
    {
        base.Die();
    }
}
