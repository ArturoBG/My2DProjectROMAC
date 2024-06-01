using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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

    [SerializeField]
    private bool isAttacking = false;

    public bool ShooterClass = false;

    [SerializeField]
    private GameObject arrowPrefab;

    [SerializeField]
    private Transform spawnerArrows;

    public float fireRate = 1f;
    public float nextFireTime = 0f;

    private void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        enemyRB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!isAttacking)
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
        else//if is shooter
        {
            Debug.Log("Shoot!");
            Shoot();
        }
    }

    public void Move()
    {
        Debug.Log("Move");
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

    public void Shoot()
    {
        if (Time.time >= nextFireTime)
        {
            Debug.Log("arrow shoot");
            GameObject arrow = Instantiate(arrowPrefab, spawnerArrows.position, Quaternion.identity);
            arrow.GetComponent<Projectile>().SetDirection(movingToB);
            nextFireTime = Time.time + fireRate;
        }
    }

    public void PlayerFound()
    {
        Debug.Log("Encontre a jugador!");
        isAttacking = true;
        enemyRB.velocity = Vector2.zero;
        enemyAnimator.SetBool("run", false);
        enemyAnimator.SetBool("attack", true);

        Shoot();
    }

    public void PlayerLost()
    {
        Debug.Log("perdi a jugador!");
        isAttacking = false;
        enemyAnimator.SetBool("run", true);
        enemyAnimator.SetBool("attack", false);
    }

    /// <summary>
    /// detectar arma del player
    /// recibir daño
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("collision entered " + collision.name);
        if (collision.tag == "playerWeapons")
        {
            Debug.Log("damage!");
        }
    }
}