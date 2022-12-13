using System.Collections;
using System.Collections.Generic;
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

public enum EEnemyState
{
    Moving,
    Attack,

}

public abstract class Enemy
{
    public Stat stat;

    protected CircleCollider2D circleCol;

    protected bool isDetected;

    private Player player;

    protected Strategy strategy = new Strategy();
    public virtual void Move()
    {
        if(isDetected == false)
        {
            RandomMove();
        }
        else
        {
            
        }
    }

    private void RandomMove()
    {

    }

    public abstract void Attack();

    public virtual void Die()
    {
    }

    protected virtual void Start()
    {

    }

}

public class Strategy
{
    private Enemy thisEnemy;

    public void StrategyInit(EAIType type)
    {
        switch (type)
        {
            case EAIType.Warrior:
                
                break;
            case EAIType.Assassin:

                break;
            case EAIType.Wizard:

                break;
            case EAIType.Archer:

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

    public void Attack()
    {
        thisEnemy.Attack();
    }

    public void Die()
    {
        thisEnemy.Die();
    }

}


