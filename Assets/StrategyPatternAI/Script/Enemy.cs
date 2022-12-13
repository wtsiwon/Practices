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

public abstract class Enemy : MonoBehaviour
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
            transform.LookAt(player.transform);
        }
    }

    private void RandomMove()
    {

    }

    public abstract void Attack();

    public virtual void Die()
    {
        if(stat.hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void Start()
    {
        circleCol = GetComponent<CircleCollider2D>();
    }

    protected virtual void Update()
    {
        strategy.Move();
        strategy.Attack();
        strategy.Die();

        
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();

        if (player != null)
        {
            isDetected = true;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();

        if(player != null)
        {
            isDetected = false;
        }
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


