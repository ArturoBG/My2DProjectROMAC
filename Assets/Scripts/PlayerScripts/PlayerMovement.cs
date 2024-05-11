using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public PlayerAction PlayerInputs;
    public PlayerAction.PlayerActions playerActions;

    public float playerSpeed = 5f;
    public float jumpHeight = 5f;
    public float counterAttack = 0;
    public bool isAttacking = false;
    public bool isDefending = false;
    public float attackTimer = 3f;

    [SerializeField]
    private Rigidbody2D rigidbody2D;

    [SerializeField]
    private bool moveOnX = false;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private SwordCollider swordCollider;

    [SerializeField]
    private int counter = 0;

    private Vector2 moveInput;
    public bool jumping = false;
    public bool onGround = false;
    public LayerMask groundLayer;

    // Start is called before the first frame update
    private void Awake()
    {
        PlayerInputs = new PlayerAction();
        playerActions = PlayerInputs.Player;

        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        //subscriptions
        playerActions.Attack.performed += ctx => Attack(ctx);
        //defend
        playerActions.Defend.performed += ctx => Defend(true);
        playerActions.Defend.canceled += ctx => Defend(false);
    }

    private void OnEnable()
    {
        playerActions.Enable();
    }

    private void OnDisable()
    {
        playerActions.Disable();
    }

    // Update is called once per frame
    private void Update()
    {
        PlayerMove(playerActions.Move.ReadValue<Vector2>());

        if (counterAttack > 3)
        {
            counterAttack = 0;
        }
        TurnPlayer();
        OnAir();
    }

    #region Functions

    private void PlayerMove(Vector2 moveInput)
    {
        if (!isDefending)
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
        }
        else
        {
            rigidbody2D.velocity = Vector2.zero;
        }
    }

    private void TurnPlayer()
    {
        moveOnX = Mathf.Abs(rigidbody2D.velocity.x) > Mathf.Epsilon;
        if (moveOnX)
        {
            transform.localScale = new Vector3(Mathf.Sign(rigidbody2D.velocity.x), 1f) * transform.localScale.y; //* transform.localScale.y
        }
    }

    #endregion Functions

    #region InputActions

    private void Attack(InputAction.CallbackContext ctx)
    {
        //Debug.Log("attack left click mouse");
        if (ctx.performed)
        {
            //raise sword
            animator.SetTrigger("swordIdle");
            //sword collider on
            swordCollider.TurnOnCollider();
            counterAttack++;
            StopCoroutine(attackSwordRoutine());
        }
        StartCoroutine(attackSwordRoutine());
        animator.SetTrigger("attack" + counterAttack);
    }

    private IEnumerator attackSwordRoutine()
    {
        float timer = attackTimer;
        while (timer > 0f)
        {
            //Debug.Log("timer " + timer);
            yield return new WaitForSeconds(.5f);
            timer--;
        }
        counterAttack = 0;
        swordCollider.TurnOffCollider();
        animator.ResetTrigger("attack1");
        animator.ResetTrigger("attack2");
        animator.ResetTrigger("attack3");

        // Debug.Log("Timer ended");
    }

    private void Defend(bool raise)
    {
        Debug.Log("Defend");
        if (raise && onGround)
        {
            Debug.Log("raise sword");
            animator.SetBool("defend", true);
            isDefending = true;
        }
        else
        {
            Debug.Log("lower sword");
            animator.SetBool("defend", false);
            isDefending = false;
        }
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        //Debug.Log("On Move value " + moveInput);
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