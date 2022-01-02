using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowerPatrol : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movment parameters")]
    [SerializeField] private float speed;
    private Vector3 initialScale;
    private bool movingLeft;
    private int randomDirection = 0;
    [SerializeField] private float walkDuration;
    private float walkTimer;

    [Header("Idle Behavior")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    [Header("Animator")]
    [SerializeField] private Animator anim;

    [Header("Player Follow")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float followSpeed;
    [SerializeField] private float viewRange;

    private void Awake()
    {
        initialScale = enemy.localScale;
    }

    private void Update()
    {
        float playerDistanceDiference = enemy.position.x - playerTransform.position.x;

        if(Mathf.Abs(playerDistanceDiference) < viewRange)
        {
            FollowBehavior(playerDistanceDiference);
        }
        else
        {
            PatrolBehavior();
        }
    }

    private void FollowBehavior(float _playerDistanceDiference)
    {
        int direction = VerifyPlayerDirection(_playerDistanceDiference);

        anim.SetBool("mooving", true);

        if(direction != 0)
        {
            enemy.localScale = new Vector3(Mathf.Abs(initialScale.x) * direction, initialScale.y, initialScale.z);

        }

        enemy.position = new Vector3(enemy.position.x + direction * followSpeed * Time.deltaTime, enemy.position.y, enemy.position.z);

        print(enemy.position.x);

        idleTimer = 0;
        walkTimer = 0;
    }

    private int VerifyPlayerDirection(float _playerDistanceDiference)
    {
        if(Mathf.Abs(_playerDistanceDiference) < 0.5f)
        {
            return 0;
        }
        return _playerDistanceDiference > 0 ? -1 : 1;
    }

    private void PatrolBehavior()
    {

        if (randomDirection == 0)
        {
            Wait();
        }
        else
        {
            MoveInDirection(randomDirection);
        }
    }

    private void GenerateNewRandomDirection()
    {
        randomDirection = Random.Range(-1, 2);
        idleTimer = 0;
        walkTimer = 0;
    }

    private void Wait()
    {
        if (idleTimer > idleDuration)
        {
            GenerateNewRandomDirection();
            return;
        }

        anim.SetBool("mooving", false);
        idleTimer++;
    }

    private void MoveInDirection(int _direction)
    {

        if (walkTimer > walkDuration)
        {
            GenerateNewRandomDirection();
            return;
        }
        anim.SetBool("mooving", true);

        enemy.localScale = new Vector3(Mathf.Abs(initialScale.x) * _direction, initialScale.y, initialScale.z);

        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed, enemy.position.y, enemy.position.z);
        walkTimer++;
    }
}
