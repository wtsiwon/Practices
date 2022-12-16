using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

public class AI : MonoBehaviour
{
    public EAIType type;

    public EState state;

    private CircleCollider2D cirCol;


    public Enemy enemy => strategy != null ? strategy.GetEnemy() : null;

    private IEnumerator CMoveTarget;

    private void Start()
    {
        cirCol = GetComponent<CircleCollider2D>();

        strategy = new Strategy(this);
        strategy.StrategyInit(type);

        CMoveTarget = CMoveTargetPos();
        
        StartCoroutine(CMoveTarget);
    }

    private Strategy strategy;

    private void Update()
    {

    }

    private IEnumerator CUpdate()
    {
        yield return new WaitForSeconds(1f);
        print(state);
        print(enemy.targetObj);
        
    }

    private IEnumerator CMoveTargetPos()
    {
        while (true)
        {
            Check();
            yield return new WaitForSeconds(0.02f);
            print(enemy.targetObj);
            //타겟이 있는가
            //거리가 일정이상으로 가깝다면 이동끝

            if (enemy.targetObj == null || state == EState.TargetingMoving)
            {
                Move();
            }
            else
            {
                Attack();
                yield return new WaitForSeconds(enemy.stat.atkSpd);
            }
        }
    }

    /// <summary>
    /// 공격범위인가
    /// </summary>
    /// <returns></returns>
    private bool AttackDistanceCheck(Vector3 targetPos, Vector3 center, float radius)
    {
        //if(Vector3.Distance(center,targetPos) < radius)

        //공격범위안에 타겟이 있는가
        if (Mathf.Pow(radius, 2) > Mathf.Pow(targetPos.x, 2) - Mathf.Pow(center.x, 2) +
            Mathf.Pow(targetPos.y, 2) - Mathf.Pow(center.y, 2))
        {
            return true;
        }
        return false;
    }

    private void Check()
    {
        if(enemy.targetObj == null)
        {
            CheckGetTarget();
        }
        else
        {
            bool check = AttackDistanceCheck(enemy.targetObj.transform.position, transform.position, enemy.stat.atkRange);
            if(check == true)
            {
                state = EState.Attack;
            }
        }
    }

    /// <summary>
    /// 타겟을 찾아주는 함수
    /// </summary>
    private void CheckGetTarget()
    {
        //감지 범위 안에 있는 모든것을 가져옴
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, enemy.stat.detectRange);

        foreach(Collider2D collider in cols)
        {
            //그게 Player인가 확인
            var target = collider.GetComponent<Player>();

            if(target != null)
            {
                //맞으면 타겟선정
                enemy.targetObj = collider.gameObject;
                if(state == EState.RandomMoving)
                {
                    state = EState.TargetingMoving; 
                }
            }
        }
    }

    public void Move()
    {
        strategy.Move();
    }

    public void Attack()
    {
        strategy.Attack();
    }

    public void Die()
    {
        strategy.Die();
    }

    private void OnDrawGizmos()
    {
        if(enemy != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, enemy.stat.detectRange);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, enemy.stat.atkRange);
        }
    }
}
