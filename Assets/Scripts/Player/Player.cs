using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance { get; private set; }

    public Vector3 DialoguePosition => dialoguePosition.position;
    public bool CanMove { get; set; } = true;

    [SerializeField] private PlayerData playerData;
    [SerializeField] private Transform groundCheck, dialoguePosition;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private GameObject interactionBubble;

    private Animator anim;
    private Rigidbody2D rb;
    private PlayerInputHandler inputHandler;
    private int facingDirection;
    private int xInput;
    private Vector2 currentVelocity;
    private bool isJumping;
    private IInteractable interactTarget;

    void Awake()
    {
        instance = this;
        anim = GetComponent<Animator>();
        inputHandler = GetComponent<PlayerInputHandler>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        facingDirection = 1;
        inputHandler.onInteractionInput = OnInteractionInput;
    }

    private void Update()
    {
        if (CanMove)
        {
            xInput = inputHandler.NormInputX;
            currentVelocity = rb.velocity;

            CheckIfShouldFlip(xInput);
            SetVelocityX(playerData.movementVelocity * xInput);
            if (inputHandler.JumpInput && CanJump())
            {
                isJumping = true;
                inputHandler.UseJumpInput();
                SetVelocityY(playerData.jumpVelocity);
            }
        }
        CheckJumpMultiplier();
    }

    private void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != facingDirection)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingDirection *= -1;
        rb.transform.Rotate(0.0f, 180.0f, 0.0f);
        interactionBubble.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void SetVelocityX(float velocity)
    {
        currentVelocity.Set(velocity, currentVelocity.y);
        rb.velocity = currentVelocity;
    }

    private void SetVelocityY(float velocity)
    {
        currentVelocity.Set(currentVelocity.x, velocity);
        rb.velocity = currentVelocity;
    }

    private bool CanJump()
    {

        return IsGround();

    }

    private void CheckJumpMultiplier()
    {
        if (isJumping)
        {
            if (inputHandler.JumpInputStop)
            {
                SetVelocityY(currentVelocity.y * playerData.variableJumpHeightMultiplier);
                isJumping = false;
            }
            else if (currentVelocity.y <= 0f)
            {
                isJumping = false;
            }

        }
    }

    public bool IsGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    private void OnInteractionInput()
    {
        if (interactTarget != null)
            interactTarget.Activate();
    }

    public void CanInteract(IInteractable target)
    {
        interactionBubble.SetActive(true);
        interactTarget = target;
    }

    public void CanNotInteract(IInteractable target)
    {
        if (interactTarget == target)
        {
            interactionBubble.SetActive(false);
            interactTarget = null;
        }
    }

    private void OnDisable()
    {
        if (gameObject != null)
            rb.velocity = Vector2.zero;
    }
}
