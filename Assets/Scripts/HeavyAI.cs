using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HeavyAI : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float distanceBetween;
    private float distance;
    public Animator animator;


    void Start()
    {
        transform.localScale = new Vector3(3, 3, 3); // Reset the sprite's scale to default
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (distance < distanceBetween)
        {
            animator.SetInteger("AnimState", 2); // Set to "Run" state or any appropriate state
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);

            // Flip the enemy's sprite based on the direction
            if (direction.x > 0) // If the player is on the left side
            {
                transform.localScale = new Vector3(-3, 3, 3); // Flip the sprite horizontally
            }
            else if (direction.x < 0) // If the player is on the right side
            {
                transform.localScale = new Vector3(3, 3, 3); // Reset the sprite's scale to default
            }
        }
        else
        {
            animator.SetInteger("AnimState", 0); // Set to "Idle" state or any appropriate state
        }
    }
}
