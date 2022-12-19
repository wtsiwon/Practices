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

    public Enemy enemy = null;

    private IEnumerator CMoveTarget;

    [HideInInspector]
    public Animator animator;

    private void Start()
    {
        cirCol = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();

        StrategyInit(type);

        CMoveTarget = CMoveTargetPos();
        
        StartCoroutine(CMoveTarget);
        StartCoroutine(nameof(CCheck));
    }

    public Coroutine MyStartCoroutine(IEnumerator methodName)
    {
        return StartCoroutine(methodName);
    }

    private void StrategyInit(EAIType type)
    {
        switch (type)
        {
            case EAIType.Warrior:
                enemy = new Warrior(this);
                break;
            case EAIType.Assassin:
                enemy = new Assasin(this);
                break;
            case EAIType.Wizard:
                enemy = new Wizard(this);
                break;
            case EAIType.Archer:
                enemy = new Archer(this);
                break;
            default:
                Debug.Assert(false, "�������� �ʴ� Ÿ���Դϴ�");
                break;
        }
    }

    private void Update()
    {

    }

    //Check 
    private IEnumerator CCheck()
    {
        yield return new WaitForSeconds(1f);
        print(state);
        print(enemy.targetObj);
    }

    private IEnumerator CMoveTargetPos()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.02f);

            yield return StartCoroutine(nameof(Check));
            //Ÿ���� �ִ°�
            //�Ÿ��� �����̻����� �����ٸ� �̵���

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

    private IEnumerator Check()
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
        yield return null;
    }
    
    /// <summary>
    /// ���ݹ����ΰ�
    /// </summary>
    /// <returns></returns>
    private bool AttackDistanceCheck(Vector3 targetPos, Vector3 center, float radius)
    {
        //if(Vector3.Distance(center,targetPos) < radius)

        //���ݹ����ȿ� Ÿ���� �ִ°�
        if (radius >= Vector3.Distance(center, targetPos))
        {
            return true;
        }
        return false;
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
                if(state == EState.RandomMoving)
                {
                    state = EState.TargetingMoving;
                }
            }
        }
    }

    public void Move()
    {
        enemy.Move();
    }

    public void Attack()
    {
        enemy.Attack();
    }

    public void Die()
    {
        enemy.Die();
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
