using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float followDistance = 10f;
    public float attackDistance = 2f;
    public float moveSpeed = 3f;
    public float rotationSpeed = 5f;
    public int damageAmount = 3;
    public float attackInterval = 2f;

    private Animator animator;
    private float nextAttackTime = 0f;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        
        if (distanceToPlayer <= attackDistance)
        {
            AttackPlayer();
        }
        
        else if (distanceToPlayer <= followDistance)
        {
            FollowPlayer();
        }
        else
        {
            StopMovement();
        }
    }

    void FollowPlayer()
    {
        if (animator != null)
        {
            animator.SetBool("IsWalking", true);
        }
    
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    void AttackPlayer()
    {
        if (animator != null)
        {
            animator.SetBool("IsWalking", false);
            animator.SetTrigger("Attack");
        }
        
        if (Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + attackInterval;
            
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
                ShowDamage("-" + damageAmount);
            }
        }
    }

    void StopMovement()
    {
        if (animator != null)
        {
            animator.SetBool("IsWalking", false);
        }
    }

    void ShowDamage(string damageText)
    {
        DamageUIManager.Instance.ShowDamage(transform.position, damageText);
    }
}
