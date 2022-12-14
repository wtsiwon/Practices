using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Enemy
{
    [SerializeField]
    private CircleCollider2D attackRange;

    [SerializeField]
    private float secondAttackDelay;

    public Warrior(AI _context) : base(_context)
    {
        stat.moveSpd = 10f;
        stat.atkSpd = 1.6f;
        stat.name = "Warrior";
        stat.atkRange = 1.5f;
        stat.detectRange = 5f;
        secondAttackDelay = 0.3f;
    }

    public override void Move()
    {
        base.Move();
    }

    public override void Attack()
    {
        context.MyStartCoroutine(CSettQ());
    }

    private IEnumerator CSettQ()
    {
        //transform.LookAt(targetObj.transform.position);
        targetObj.GetComponent<Player>().Hp -= stat.dmg;
        yield return new WaitForSeconds(secondAttackDelay);
        targetObj.GetComponent<Player>().Hp -= stat.dmg;
    }

    public override void Die()
    {
        base.Die();
    }
}
