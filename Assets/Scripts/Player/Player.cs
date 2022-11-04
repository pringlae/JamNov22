using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask whatIsGround;

    private Animator anim;
    private Rigidbody2D rb;
    private PlayerInputHandler inputHandler;
    private int facingDirection;
    private int xInput;
    private Vector2 currentVelocity;
    private bool isJumping;

    void Start()
    {
        anim = GetComponent<Animator>();
        inputHandler = GetComponent<PlayerInputHandler>();
        rb = GetComponent<Rigidbody2D>();

        facingDirection = 1;
    }

    private void Update()
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
        CheckJumpMultiplier();
    }

    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != facingDirection)
        {
            Flip();
        }
    }

    public void Flip()
    {
        facingDirection *= -1;
        rb.transform.Rotate(0.0f, 180.0f, 0.0f);
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

    public bool CanJump()
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
}
