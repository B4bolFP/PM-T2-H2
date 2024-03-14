using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemyFSM : MonoBehaviour
{
    public enum MindStates
    {
        kWait,
        kSeek,
        kPursuit,
        kAttack,
        kFlee
    }
   
    public MindStates current_mind_state_;
    public Sight sight_sensor_;
    public NavMeshAgent agent_;

    public float attack_distance_ = 0.0f;

    public float stop_attack_distance_multiplier = 1.2f;

    public float stun_time_ = 2.0f;

    public float attackCooldown;
    private float auxAttackCooldown = 0f;
    private bool canAttack = true;

    public float health;
    float maxHealth;

    public bool divides;
    public GameObject redEnemy;
    public GameObject grayEnemy;
    public Transform purpleSpawnPoint;

    [SerializeField] FloatingHealthBar healthBar_;

    private GameObject player;
    private PlayerHealth playerHealth;


    private void Awake()
    {
        agent_ = GetComponentInParent<NavMeshAgent>();
        healthBar_ = GetComponentInChildren<FloatingHealthBar>();
    }

    // Start is called before the first frame update
    void Start()
    {
        healthBar_.UpdateHealthBar(health, maxHealth);
        maxHealth = health;
        player = GameObject.Find("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        auxAttackCooldown += Time.deltaTime;

        if (auxAttackCooldown >= attackCooldown)
        {
            canAttack = true;
        }

        if(current_mind_state_ == MindStates.kWait)
        {
            MindWait();
        }else if(current_mind_state_ == MindStates.kSeek)
        {
            MindSeek();
        }else if(current_mind_state_ == MindStates.kPursuit)
        {
            MindPursuit();
        }else if(current_mind_state_ == MindStates.kAttack)
        {
            MindAttack();
        }else if(current_mind_state_ == MindStates.kFlee)
        {
            MindFlee();
        }

    }
    
    void MindWait()
    {
        BodyWait();
        //añadir corrutina stun time
        current_mind_state_ = MindStates.kSeek;

    }
    void MindSeek()
    {
        BodySeek();

        if(sight_sensor_.detected_object_ != null)
        {
            current_mind_state_ = MindStates.kPursuit;
        }

    }
    void MindPursuit()
    {
        BodyPursuit();

        if(sight_sensor_.detected_object_ == null)
        {
            current_mind_state_ = MindStates.kWait;
            return;
        }

        float distance_to_target = Vector3.Distance(transform.position, sight_sensor_.detected_object_.transform.position);
        if(distance_to_target <= attack_distance_)
        {
            current_mind_state_ = MindStates.kAttack;
        }
        
    }
    void MindAttack()
    {
        if (canAttack)
        {
            BodyAttack();
            canAttack = false;
            auxAttackCooldown = 0f;
        }

        if (sight_sensor_.detected_object_ == null)
        {
            current_mind_state_ = MindStates.kWait;
            return;
        }

        float distance_to_target = Vector3.Distance(transform.position, sight_sensor_.detected_object_.transform.position);
        if (distance_to_target > attack_distance_ * stop_attack_distance_multiplier)
        {
            current_mind_state_ = MindStates.kWait;
        }
    }
    void MindFlee()
    {
        BodyFlee();
    }

    void BodyWait()
    {
        agent_.isStopped = true;
    }
    void BodySeek()
    {
        //rotate o algo

    }
    void BodyPursuit()
    {
        if (agent_ != null && sight_sensor_.detected_object_ != null)
        {
            agent_.isStopped = false;
            agent_.SetDestination(sight_sensor_.detected_object_.transform.position);
        }
           
    }
    void BodyAttack()
    {
        agent_.isStopped = true;
        //atacar o algo
        playerHealth.damage(10);
    }
    void BodyFlee()
    {

    }

    public void TakeDamage(float damage)
    {
        health  -= damage;
        healthBar_.UpdateHealthBar(health, maxHealth);

        if (divides && health <= 0)
        {
            DiePurple();
        } else
        {
            Die();
        }
    }

    void Die()
    {
        if (divides && redEnemy.gameObject != null && grayEnemy.gameObject != null)
        {
            redEnemy.SetActive(true);
            grayEnemy.SetActive(true);
            Instantiate(redEnemy, purpleSpawnPoint);
            Instantiate(grayEnemy, purpleSpawnPoint);
        } else
        {
            Destroy(gameObject);
        }

    }

    void DiePurple()
    {
        if (redEnemy.gameObject == null && grayEnemy.gameObject == null)
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attack_distance_);
    }
}
