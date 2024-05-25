using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed;

    private Rigidbody2D enemyRB;
    private Animator enemyAnimator;
    private bool moveOnX;

    [SerializeField]
    private bool movingToB = true;

    private void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        enemyRB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
        Flip();

        if (moveOnX)
        {
            enemyAnimator.SetBool("run", true);
        }
        else
        {
            enemyAnimator.SetBool("run", false);
        }
    }

    public void Move()
    {
        if (movingToB)
        {
            enemyRB.velocity = new Vector2(speed, enemyRB.velocity.y);
            //Debug.Log("going to B " + Vector2.Distance(transform.position, pointB.position));
            if (Vector2.Distance(transform.position, pointB.position) < 0.1f)
            {
                movingToB = false;
            }
        }
        else
        {
            enemyRB.velocity = new Vector2(-speed, enemyRB.velocity.y);
            if (Vector2.Distance(transform.position, pointA.position) < 0.1f)
            {
                movingToB = true;
            }
        }
    }

    private void Flip()
    {
        moveOnX = Mathf.Abs(enemyRB.velocity.x) > Mathf.Epsilon;
        if (moveOnX)
        {
            transform.localScale = new Vector3(Mathf.Sign(enemyRB.velocity.x), 1f) * transform.localScale.y; //* transform.localScale.y
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision entered " + collision.name);
        if (collision.tag == "playerWeapons")
        {
            Debug.Log("damage!");
        }
    }
}