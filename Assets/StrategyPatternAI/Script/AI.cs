using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class AI : MonoBehaviour
{
    public EAIType type;

    public EState state;

    private CircleCollider2D cirCol;

    
    public Enemy enemy;

    private void Start()
    {
        cirCol = GetComponent<CircleCollider2D>();

        strategy = new Strategy(this);
        strategy.StrategyInit(type);
        
        enemy = strategy.GetEnemy();

        StartCoroutine(nameof(CMoveTargetPos));
    }

    private Strategy strategy;

    private void Update()
    {

    }

    private IEnumerator CMoveTargetPos()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            //타겟이 있는가
            //거리가 일정이상으로 가깝다면 이동끝

            if (enemy.targetObj == null || state != EState.Attack)
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
