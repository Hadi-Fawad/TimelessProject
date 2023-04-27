using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Transform attackPointRight;
    public Transform attackPointLeft;


    public float attackRange = 0.5f;

    public LayerMask enemyLayers;
    public int attackDamage = 10;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HeroAttack();
        }
    }

    void Attack()
    {
        Collider2D[] hitEnemiesRight = Physics2D.OverlapCircleAll(attackPointRight.position, attackRange, enemyLayers);
        Collider2D[] hitEnemiesLeft = Physics2D.OverlapCircleAll(attackPointLeft.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemiesRight)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }

        foreach (Collider2D enemy in hitEnemiesLeft)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    void HeroAttack()
    {
        Collider2D[] hitEnemiesRight = Physics2D.OverlapCircleAll(attackPointRight.position, attackRange, enemyLayers);
        Collider2D[] hitEnemiesLeft = Physics2D.OverlapCircleAll(attackPointLeft.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemiesRight)
        {
            enemy.GetComponent<HeroKnight>().Hurt(attackDamage);
        }

        foreach (Collider2D enemy in hitEnemiesLeft)
        {
            enemy.GetComponent<HeroKnight>().Hurt(attackDamage);
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPointRight == null)
            return;

        Gizmos.DrawWireSphere(attackPointRight.position, attackRange);

        if (attackPointLeft == null)
            return;

        Gizmos.DrawWireSphere(attackPointLeft.position, attackRange);
    }

}
