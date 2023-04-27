using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 30;
    int currentHealth;
    public Animator animator;
    public float distanceThreshold = 10.0f; // Distance threshold to determine when player is nearby
    public int attackDamage = 10;

    public HeroKnight knight;

    // Reference to the AIPath component for pathfinding

    void Start()
    {
        currentHealth = maxHealth;

    }
   

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("Attack");
            HeroAttack();
        }

        if (collision.gameObject.CompareTag("Death Barrier"))
        {
            // Character has collided with the bottom border, so we trigger their death here
            Die();
        }
    }

   

    void HeroAttack()
    {

        knight.Hurt(attackDamage);

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetTrigger("Death");

        GetComponent<Collider2D>().enabled = false;

        this.enabled = false;
    }

    
}