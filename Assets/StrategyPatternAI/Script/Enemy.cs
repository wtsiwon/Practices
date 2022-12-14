using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public enum EAIType
{
    Warrior,
    Assassin,
    Wizard,
    Archer,
}

public struct Stat
{
    public string name;
    public float hp;
    public float dmg;
    public float moveSpd;
    public float atkRange;
    public float atkSpd;
}

//Enemy의 상태
public enum EEnemyState
{
    Moving,
    Attack,
}

public enum EAttackType
{
    Far,
    Close,
}

public abstract class Enemy : Entity
{
    public Stat stat;

    protected CircleCollider2D circleCol;

    private Player player;

    public Vector2 targetPos;

    public IEnumerator moveTargetPos;

    //protected Strategy strategy = new Strategy();

    protected AI context;
    protected GameObject gameobject;
    protected Transform transform;

    public Enemy(AI _context)
    {
        context = _context;

        gameobject = context.gameObject;
        transform = context.transform;

        moveTargetPos = CMoveTargetPos();
    }

    T GetComponent<T>()
    {
        return context.GetComponent<T>();
    }

    public virtual void Move()
    {

    }

    private Vector2 RandomPos()
    {
        var screenheight = Screen.height / 2;
        var screenwidth = Screen.width / 2;

        var randtargetPosx = Random.Range(-screenwidth, screenwidth);
        var randtargetPosy = Random.Range(-screenheight, screenheight);

        Vector2 targetPos = Camera.main.ScreenToViewportPoint(new Vector2(randtargetPosx, randtargetPosy));
        return targetPos;
    }

    private IEnumerator CMoveTargetPos()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            if (player) yield break;
            //거리가 일정이상으로 가깝다면 이동끝
            if (Vector2.Distance(RandomPos(), transform.position) <= stat.atkRange)
            {
                yield break;
            }

            //가다가 Player를 감지 했을 경우 공격

            Vector2 vPos = transform.position;
            Vector2 vDist = RandomPos() - vPos;
            Vector2 vDir = vDist.normalized;

            transform.LookAt(RandomPos());

            transform.position = vDir * stat.moveSpd;

        }
    }

    public abstract void Attack();

    public virtual void Die()
    {

    }

}

public class Strategy
{
    Enemy thisEnemy = null;
    AI context;
    public Strategy(AI _context)
    {
        context = _context;
    }


    public void StrategyInit(EAIType type)
    {
        switch (type)
        {
            case EAIType.Warrior:
                thisEnemy = new Warrior(context);
                break;
            case EAIType.Assassin:
                thisEnemy = new Assasin(context);
                break;
            case EAIType.Wizard:
                thisEnemy = new Wizard(context);
                break;
            case EAIType.Archer:
                thisEnemy = new Archer(context);
                break;
            default:
                Debug.Assert(false, "존재하지 않는 타입입니다");
                break;
        }
    }

    public void Move()
    {
        thisEnemy.Move();
    }

    private IEnumerator CTargetMoving(float posx, float posy)
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);

        }
    }

    public void Attack()
    {
        thisEnemy.Attack();
    }

    public void Die()
    {
        thisEnemy.Die();
    }

}


