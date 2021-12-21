using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    private PlayerMovment playerMovment;
    private float cooldownTimer = Mathf.Infinity;

    [Header("Collider")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float colliderDistance;
    [SerializeField] private float range;

    [Header("Attack")]
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float damage;

    [Header("Enemy Layer")]
    [SerializeField] private LayerMask enemyLayer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovment = GetComponent<PlayerMovment>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && cooldownTimer > attackCooldown && playerMovment.canAttack())
            Attack();
        cooldownTimer += Time.deltaTime; 
    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;
    }

    private bool EnemyInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * directionX() * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, directionX() > 1 ? Vector2.left : Vector2.right, 0, enemyLayer);

        print(hit.collider != null && hit.collider.gameObject.GetComponent<Health>());

        if (hit.collider != null && hit.collider.gameObject.GetComponent<Health>() != null)
        {
            if (hit.collider.gameObject.GetComponent<Health>().TakeDamage(damage))
                SoundManager.instance.PlaySound(hitSound);
        }

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * directionX() * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private int directionX()
    {
        return transform.localScale.x > 0.01f ? 1 : -1;
    }

}
