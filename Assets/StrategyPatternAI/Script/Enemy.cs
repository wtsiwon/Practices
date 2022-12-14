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

    public Vector2 targetPos;

    //protected Strategy strategy = new Strategy();



    public virtual void Move(Transform mytransform, Transform targetTransform)
    {
        if (isDetected == false)
        {

        }
        else
        {
            mytransform.LookAt(targetTransform);

        }
    }

    private void RandomPos()
    {
        var screenheight = Screen.height / 2;
        var screenwidth = Screen.width / 2;

        var randtargetPosx = Random.Range(-screenwidth, screenwidth);
        var randtargetPosy = Random.Range(-screenheight, screenheight);

        targetPos = Camera.main.ScreenToViewportPoint(new Vector2(randtargetPosx, randtargetPosy));
    }

    private IEnumerator CMoveTargetPos()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            //가다가 Player를 감지 했을 경우 공격

            
        }
    }

    public abstract void Attack();

    public virtual void Die()
    {

    }

    public void SetEnemy(Enemy enemy)
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
                thisEnemy = new Warrior();
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

    public void Move(Transform mytransform, Transform targetTransform = null)
    {
        thisEnemy.Move(mytransform, targetTransform);


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


