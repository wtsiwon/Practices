using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public EAIType type;

    public Player target;

    public Enemy[] enemies;

    private CircleCollider2D cirCol;

    private void Start()
    {
        cirCol = GetComponent<CircleCollider2D>();
        
        strategy.StrategyInit(type);
    }

    private Strategy strategy = new Strategy();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            target = collision.GetComponent<Player>();
        }
    }

    public void Move()
    {
        strategy.Move(transform);
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
