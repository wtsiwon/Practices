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
        else //Ojbect가 감지 되었을 때 
        {
            if (context.state != EState.Attack)
            {
                targetPos = targetObj.transform.position;
            }
        }

        if (context.state != EState.Attack)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, stat.moveSpd * Time.deltaTime);
        }
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

        //Animation
    }

    public virtual void GiveDamage(float dmg)
    {

    }

    public virtual void Die()
    {

    }

}