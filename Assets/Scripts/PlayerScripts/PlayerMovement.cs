using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 5f;
    public float jumpHeight = 5f;

    [SerializeField]
    private Rigidbody2D rigidbody2D;

    [SerializeField]
    private Animator animator;

    private Vector2 moveInput;

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
    }

    #region Functions

    private void PlayerMove()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * playerSpeed, rigidbody2D.velocity.y);
        rigidbody2D.velocity = playerVelocity;

        //Animator setTrigger RUn!
    }

    #endregion Functions

    #region InputActions

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        //Debug.Log("On Move value " + moveInput);
    }

    private void OnAttack()
    {
    }

    private void OnJump(InputValue value)
    {
        Debug.Log("On Jump value " + value);
    }

    private void OnSlide()
    {
    }

    private void OnDefend()
    {
    }

    #endregion InputActions
}