using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class AI : MonoBehaviour
{
    public EAIType type;

    public Player target;

    private CircleCollider2D cirCol;

    public Enemy enemy;

    private void Start()
    {
        cirCol = GetComponent<CircleCollider2D>();

        strategy = new Strategy(this);
        strategy.StrategyInit(type);
    }

    private Strategy strategy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            target = player;
        }
    }

    private void Update()
    {

    }

    public void Move()
    {
        strategy.Move();
        StartCoroutine(enemy.moveTargetPos);
    }

    public void Attack()
    {
        strategy.Attack();
    }

    public void Die()
    {
        strategy.Die();
    }
}
