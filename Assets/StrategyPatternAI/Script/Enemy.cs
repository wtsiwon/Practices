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
    public float detectRange;
}

//Enemy의 상태
public enum EState
{
    RandomMoving,
    Attack,
    TargetingMoving,
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

    public Vector3 targetPos = Vector3.zero;

    public IEnumerator moveTargetPos;

    [Tooltip("타겟 Object")]
    public GameObject targetObj;

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

    public T GetComponent<T>() where T : MonoBehaviour
    {
        return context.GetComponent<T>();
    }

    public virtual void Move()
    {
        //감지한 Object가 없을때
        if (targetObj == null)
        {
            if (transform.position == targetPos || targetPos == Vector3.zero)
            {
                RandomPos();
            }
        }
        else
        {
            Debug.Log("ㅠ");
            targetPos = targetObj.transform.position;
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPos, stat.moveSpd * Time.deltaTime);
        //Vector3 vPos = transform.position;
        //Vector3 vDist = RandomPos() - vPos;
        //Vector3 vDir = vDist.normalized;

        //transform.LookAt(RandomPos());

        //transform.position = vDir * stat.moveSpd;
    }

    /// <summary>
    /// 랜덤으로 Position선정
    /// </summary>
    public void RandomPos()
    {
        var screenheight = Screen.height;
        var screenwidth = Screen.width;

        var randtargetPosx = Random.Range(0, screenwidth);
        var randtargetPosy = Random.Range(0, screenheight);

        var randPosition = Camera.main.ScreenToWorldPoint(new Vector2(randtargetPosx, randtargetPosy));
        targetPos = randPosition + new Vector3(0, 0, 10);//카메라 Pos라서 +10;
    }

    private IEnumerator CMoveTargetPos()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            //타겟이 있는가
            //거리가 일정이상으로 가깝다면 이동끝

            if (targetObj == null || context.state.Equals(EState.TargetingMoving) == true)
            {
                Move();
            }
            else
            {
                Attack();
                yield return new WaitForSeconds(stat.atkSpd);
            }
        }
    }
    
    public virtual void Attack()
    {
        //공격 범위가 타겟과의 거리보다 크다면(안에 들어옴)
        if (stat.atkRange >= Vector3.Distance(targetPos, transform.position))
        {
            context.state = EState.Attack;
        }
        //Animation
    }

    public virtual void GiveDamage(float dmg)
    {
        targetObj.GetComponent<Player>().hp -= dmg;
    }

    public virtual void Die()
    {

    }

}

public class Strategy
{
    private Enemy thisEnemy = null;
    private AI context;
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

    public Enemy GetEnemy()
    {
        return thisEnemy;
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


