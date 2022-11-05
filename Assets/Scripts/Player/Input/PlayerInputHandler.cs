using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public System.Action onInteractionInput;
    public Vector2 RawMovementInput { get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }

    private InputHandler input;

    [SerializeField]
    private float inputHoldTime = 0.2f;

    private float jumpInputStartTime;

    private void Awake()
    {
        input = new InputHandler();
        input.Gameplay.Jump.performed += OnJumpPerformed;
        input.Gameplay.Jump.canceled += OnJumpCanceled;
        input.Gameplay.Movement.performed += OnMoveInput;
        input.Gameplay.Movement.canceled += OnMoveInput;
        input.Gameplay.Interact.performed += OnInteractionPerformed;
        input.Gameplay.QuestList.performed += OnQuestListPerformed;
        input.Gameplay.QuestList.canceled += OnQuestListCanceled;
        input.Gameplay.Help.performed += OnHelpPerformed;
        input.Gameplay.Help.canceled += OnHelpCanceled;
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void Update()
    {
        CheckJumpInputHoldTime();
    }

    public void OnJumpPerformed(InputAction.CallbackContext context)
    {
        JumpInput = true;
        JumpInputStop = false;
        jumpInputStartTime = Time.time;
    }

    public void OnJumpCanceled(InputAction.CallbackContext context)
    {
        JumpInputStop = true;
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        NormInputX = Mathf.RoundToInt(RawMovementInput.x);
        NormInputY = Mathf.RoundToInt(RawMovementInput.y);

    }
    public void OnInteractionPerformed(InputAction.CallbackContext context)
    {
        onInteractionInput();
    }

    public void UseJumpInput() => JumpInput = false;

    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }

    public void OnQuestListPerformed(InputAction.CallbackContext context)
    {
        QuestList.instance.Show();
    }

    public void OnQuestListCanceled(InputAction.CallbackContext context)
    {
        QuestList.instance.Close();
    }

    public void OnHelpPerformed(InputAction.CallbackContext context)
    {
        Help.instance.Show();
    }

    public void OnHelpCanceled(InputAction.CallbackContext context)
    {
        Help.instance.Hide();
    }

}

