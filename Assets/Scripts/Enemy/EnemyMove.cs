using System.Collections;
using System.Collections.Generic;
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
        else
        {
            Animator.SetBool("isAggro", false);
            transform.position += new Vector3(0, 0, -speed) * Time.deltaTime;
        }
    }
}
