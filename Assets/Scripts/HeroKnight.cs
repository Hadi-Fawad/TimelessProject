using UnityEngine;
using System.Collections;

public class HeroKnight : MonoBehaviour {

    float      m_speed = 6.0f;
    float      m_jumpForce = 10.0f;
    float      m_rollForce = 6.0f;

    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private Sensor_HeroKnight m_groundSensor;
    private Sensor_HeroKnight m_wallSensorR1;
    private Sensor_HeroKnight m_wallSensorR2;
    private Sensor_HeroKnight m_wallSensorL1;
    private Sensor_HeroKnight m_wallSensorL2;


    private bool m_grounded = false;
    private bool m_rolling = false;
    private bool isOnGround = false;
    private int m_currentAttack = 0;
    private float m_timeSinceAttack = 0.0f;
    private float m_delayToIdle = 0.0f;
    private float m_rollDuration = 8.0f / 14.0f;
    private float m_rollCurrentTime;
    private int m_facingDirection = 1;


    public int maxHealth = 10;
    public int currentHealth;
    public HealthBar healthBar;
    public DeathScreen deathScreen;
    public FinishScreen finishScreen;
    public bool isAlive = true;

    // Start of game settings
    void Start ()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Sensor_HeroKnight>();

    }

    void Update ()
    {
        if (isAlive)
        {
            
            // Increase timer that controls attack combo
            m_timeSinceAttack += Time.deltaTime;

            // Increase timer that checks roll duration
            if (m_rolling)
                m_rollCurrentTime += Time.deltaTime;

            // Disable rolling if timer extends duration
            if (m_rollCurrentTime > m_rollDuration)
                m_rolling = false;

            //Check if character just landed on the ground
            if (!m_grounded && m_groundSensor.State())
            {
                m_grounded = true;
                m_animator.SetBool("Grounded", m_grounded);
            }

            //Check if character just started falling
            if (m_grounded && !m_groundSensor.State())
            {
                m_grounded = false;
                m_animator.SetBool("Grounded", m_grounded);
            }

            // Used to handle movement
            float inputX = Input.GetAxis("Horizontal");

            // Swap direction of Hero depending on walk direction
            if (inputX > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
                m_facingDirection = 1;
            }

            else if (inputX < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
                m_facingDirection = -1;
            }

            // Move Animation
            if (!m_rolling)
                m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

            //Set AirSpeed in animator
            m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

            //Attack Animation
            if (Input.GetMouseButtonDown(0) && m_timeSinceAttack > 0.25f && !m_rolling)
            {
                m_currentAttack++;

                // Loop back to one after third attack
                if (m_currentAttack > 3)
                    m_currentAttack = 1;

                // Reset Attack combo if time since last attack is too large
                if (m_timeSinceAttack > 1.0f)
                    m_currentAttack = 1;

                // Call one of three attack animations "Attack1", "Attack2", "Attack3"
                m_animator.SetTrigger("Attack" + m_currentAttack);

                // Reset timer
                m_timeSinceAttack = 0.0f;
            }

            // Roll Animation
            else if (Input.GetKeyDown("left shift"))
            {
                m_rolling = true;
                m_animator.SetTrigger("Roll");
                m_body2d.velocity = new Vector2(m_facingDirection * m_rollForce, m_body2d.velocity.y);
            }

            else if (Input.GetMouseButtonUp(1))
                m_animator.SetBool("IdleBlock", false);

            //Jump Animation
            else if (Input.GetKeyDown("space") && m_grounded && !m_rolling)
            {
                if (isOnGround)
                {
                    m_animator.SetTrigger("Jump");
                    m_grounded = false;
                    m_animator.SetBool("Grounded", m_grounded);
                    m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
                    m_groundSensor.Disable(0.2f);
                    isOnGround = false;
                }
            }

            //Run Animation
            else if (Mathf.Abs(inputX) > Mathf.Epsilon)
            {
                // Reset timer
                m_delayToIdle = 0.05f;
                m_animator.SetInteger("AnimState", 1);
            }

            //Idle Animation
            else
            {
                // Prevents flickering transitions to idle
                m_delayToIdle -= Time.deltaTime;
                if (m_delayToIdle < 0)
                    m_animator.SetInteger("AnimState", 0);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Death Barrier"))
        {
            // Character has collided with the bottom border, so we trigger their death here
            Die();
            Hurt(10);
        }
        else if (collision.gameObject.CompareTag("Hurt Barrier"))
        {
            // Character has collided with a Hurt Barrier, so we trigger their hurt
            isOnGround = true;
            Hurt(2);
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            isOnGround = true;
        }
        else if (collision.gameObject.CompareTag("Finish"))
        {
            Finish();
        }
    }

    void Finish()
    {
        isAlive = false;
        finishScreen.finish(0);
    }

    void Die()
    {
        isAlive = false;
        m_animator.SetTrigger("Death");
        deathScreen.die(0);
    }

    public void Hurt(int damage)
    {
        m_animator.SetTrigger("Hurt");
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }


}
