using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 5f;
    public float jumpHeight = 5f;

    [SerializeField]
    private Rigidbody2D rigidbody2D;

    [SerializeField]
    private bool moveOnX = false;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private int counter = 0;

    private Vector2 moveInput;
    public bool jumping = false;
    public bool onGround = false;
    public LayerMask groundLayer;

    // Start is called before the first frame update
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        PlayerMove();
        TurnPlayer();
        OnAir();
    }

    #region Functions

    private void PlayerMove()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * playerSpeed, rigidbody2D.velocity.y);
        rigidbody2D.velocity = playerVelocity;

        //Animator setTrigger RUn!
        if (moveOnX)
        {
            animator.SetBool("run", true);
        }
        else
        {
            animator.SetBool("run", false);
        }

        //animator.SetBool("run", moveOnX);
    }

    private void TurnPlayer()
    {
        moveOnX = Mathf.Abs(rigidbody2D.velocity.x) > Mathf.Epsilon;
        if (moveOnX)
        {
            transform.localScale = new Vector3(Mathf.Sign(rigidbody2D.velocity.x), 1f); //* transform.localScale.y
        }
    }

    #endregion Functions

    #region InputActions

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        //Debug.Log("On Move value " + moveInput);
    }

    private void OnAttack(InputValue value)
    {
        if (value.isPressed)
        {
            counter++;
            Debug.Log("Attack!");
            animator.SetTrigger("swordIdle");
            animator.SetTrigger("attack1");

            //elegir random un atack y triggerearlo

            StartCoroutine(attackRoutine(counter));
        }
    }

    private IEnumerator attackRoutine(int counter)
    {
        float timer = 2f;

        //mientras que no acabe timer
        //counter va sumando trigger de attack
        //cuando timer acabe, reiniciamos counter

        yield return null;
    }

    private void OnJump(InputValue value)
    {
        if (value.isPressed && onGround)
        {
            jumping = true;
            Debug.Log("On Jump value " + value);
            rigidbody2D.velocity = new Vector2(0, jumpHeight);
            animator.SetTrigger("jump");
        }
    }

    private void OnAir()
    {
        if (rigidbody2D.IsTouchingLayers(groundLayer))
        {
            onGround = true;
            jumping = false;
        }
        else
        {
            onGround = false;
            if (!jumping)
            {
                PlayDesiredState("player_onAir");
            }
        }

        animator.SetBool("onAir", !onGround);
    }

    private void OnSlide(InputValue value)
    {
        //only while onMoveX is true
    }

    private void OnDefend(InputValue value)
    {
        if (value.isPressed)
        {
            animator.SetBool("defend", true);
        }
        ///TODO
        ///defend turn to false
    }

    private void PlayDesiredState(string stateName)
    {
        AnimatorStateInfo currentStateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (!currentStateInfo.IsName(stateName))
        {
            int stateHash = Animator.StringToHash(stateName);
            animator.Play(stateName, -1, 0f);
        }
    }

    #endregion InputActions
}