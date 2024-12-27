using System.Collections;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float objectDistance;

    private Rigidbody rb;
    private Enemy enemy;
    private Animator Animator;
    public Transform target;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        enemy = GetComponent<Enemy>();
        Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (enemy.isAggroed)
        {
            AggroState();
        }
        else
        {
            IdleState();
        }
    }
    private void IdleState()
    {
        Animator.SetBool("isAggro", false);
        transform.position += new Vector3(0, 0, -speed) * Time.deltaTime;
    }
    private void AggroState()
    {
        Animator.SetBool("isAggro", true);
        Vector3 direction = target.position - transform.position;
        float distance = direction.magnitude;
        direction.Normalize();
        if (distance > objectDistance)
        {
            rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
        }
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
    }
}