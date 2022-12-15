using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class AI : MonoBehaviour
{
    public EAIType type;

    public EState state;

    public bool isAttack;

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
            Check();
            yield return new WaitForSeconds(0.02f);

            //Ÿ���� �ִ°�
            //�Ÿ��� �����̻����� �����ٸ� �̵���

            if (enemy.targetObj == null || isAttack == true)
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
    /// ���ݹ����ΰ�
    /// </summary>
    /// <returns></returns>
    private bool AttackDistanceCheck(Vector3 targetPos, Vector3 center, float radius)
    {
        //if(Vector3.Distance(center,targetPos) < radius)

        //���ݹ����ȿ� Ÿ���� �ִ°�
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
            isAttack 
                = AttackDistanceCheck(enemy.targetObj.transform.position, transform.position, enemy.stat.atkRange);
        }
    }

    /// <summary>
    /// Ÿ���� ã���ִ� �Լ�
    /// </summary>
    private void CheckGetTarget()
    {
        //���� ���� �ȿ� �ִ� ������ ������
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, enemy.stat.detectRange);

        foreach(Collider2D collider in cols)
        {
            //�װ� Player�ΰ� Ȯ��
            var target = collider.GetComponent<Player>();

            if(target != null)
            {
                //������ Ÿ�ټ���
                enemy.targetObj = collider.gameObject;
            }
            else
            {
                enemy.targetObj = null;
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
