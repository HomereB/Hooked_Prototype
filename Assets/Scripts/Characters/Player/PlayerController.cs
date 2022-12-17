using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : EntityController
{
    //Base Motion
    [SerializeField]
    private float playerSpeed = 2f; //TODO : use player motion data + move behaviour
    [SerializeField]
    private float cameraMovementMultiplicator = 1.5f;
    [SerializeField]
    private float cameraAimMultiplicator = .5f;

    //Managers
    //TODO : ComboManager
    private PlayerDashManager dashManager;
    private PlayerHookManager hookManager;
    private PlayerSpecialManager specialManager;
    [SerializeField]
    private HUDManager playerHUDManager;


    //Crosshair
    [SerializeField]
    private PlayerCrosshair playerCrosshair;


    //Jump //TODO : move logic in a jump behaviour
    [SerializeField]
    private int currentJumpAmount = 0;
    private bool needNewJumpPressed = false;


    //Input registration
    private Vector2 movementInput = Vector2.zero;
    private Vector2 cameraInput = Vector2.zero;
    private bool isJumpPressed = false;
    private bool isDashPressed = false;
    private bool isPunchPressed = false;
    private bool isKickPressed = false;
    private bool isUsePressed = false;
    private bool isGrabPressed = false;
    private bool isHookPressed = false;
    private bool isSpecialPressed = false;


    //Status //TODO? : move in respective status
    private Vector2 ejectionValue = Vector2.zero;
    
    
    //Player Input Map
    [SerializeField]
    private PlayerInput playerInput;
    public PlayerInput PlayerInput => playerInput;


    //Checkers
    [SerializeField]
    private GroundChecker2D groundChecker2D;
    [SerializeField]
    private WallChecker2D wallChecker2D;


    //Data from Scriptable objects
    public PlayerHealthData playerHealthData;
    public PlayerJumpData playerJumpData;
    public PlayerJumpData playerWallJumpData;
    public PlayerGravityData playerGravityData;
    public PlayerGravityData playerWallRidingData;
    public PlayerDashData playerDashData;
    public PlayerHookData playerHookData;
    public PlayerSpecialData playerSpecialData;


    //Movement computation elements
    private Vector2 movementValue = Vector2.zero;
    private List<Vector2> externalForces = new List<Vector2>();

    //Behaviour components
    private IGravityBehaviour gravityBehaviour;
    private IJumpBehaviour jumpBehaviour;


    //FSM Components
    private PlayerBaseState currentState;
    private PlayerStateManager playerStates;


    //Animation
    private Animator playerAnimator;


    //RigidBody
    private Rigidbody2D rb;


    //Get & Set (TODO : sort a bit...)
    public PlayerBaseState CurrentState { get => currentState; set => currentState = value; }
    public bool IsJumpPressed { get => isJumpPressed; set => isJumpPressed = value; }
    public bool IsDashPressed { get => isDashPressed; set => isDashPressed = value; }
    public bool IsPunchPressed { get => isPunchPressed; set => isPunchPressed = value; }
    public bool IsKickPressed { get => isKickPressed; set => isKickPressed = value; }
    public bool IsUsePressed { get => isUsePressed; set => isUsePressed = value; }
    public bool IsGrabPressed { get => isGrabPressed; set => isGrabPressed = value; }
    public bool IsHookPressed { get => isHookPressed; set => isHookPressed = value; }
    public bool IsSpecialPressed { get => isSpecialPressed; set => isSpecialPressed = value; }
    public int CurrentJumpAmount { get => currentJumpAmount; set => currentJumpAmount = value; }
    public bool NeedNewJumpPressed { get => needNewJumpPressed; set => needNewJumpPressed = value; }
    public bool CanJump { get => currentJumpAmount < playerJumpData.maxJumpAmount && !needNewJumpPressed; }
    public bool IsMovementPressed { get => movementInput != Vector2.zero ? true : false; }
    public Vector2 MovementInput { get => movementInput; set => movementInput = value; }
    public bool IsCameraMovementPressed { get => cameraInput != Vector2.zero ? true : false; }
    public Vector2 CameraInput { get => cameraInput; set => cameraInput = value; }
    public bool IsGrounded { get => groundChecker2D.IsGrounded; }
    public bool IsAgainstWallLeft { get => wallChecker2D.IsAgainstWallLeft; }
    public bool IsAgainstWallRight { get => wallChecker2D.IsAgainstWallRight; }
    public bool IsAgainstWall { get => wallChecker2D.IsAgainstWallLeft || wallChecker2D.IsAgainstWallRight; }
    public IGravityBehaviour GravityBehaviour { get => gravityBehaviour; }
    public IJumpBehaviour JumpBehaviour { get => jumpBehaviour; }
    public Vector2 MovementValue { get => movementValue; set => movementValue = value; }
    public List<Vector2> ExternalForces { get => externalForces; set => externalForces = value; }
    public float PlayerSpeed { get => playerSpeed; set => playerSpeed = value; }
    public bool CanDash { get => dashManager.CanDash; }
    public bool NeedNewDashPressed { get => dashManager.NeedNewDashPressed; set => dashManager.NeedNewDashPressed = value; }
    public bool NeedNewHookPressed { get => hookManager.NeedNewHookPressed; set => hookManager.NeedNewHookPressed = value; }
    public PlayerDashManager DashManager { get => dashManager; set => dashManager = value; }
    public Vector3 Position { get => gameObject.transform.position; }
    public Animator PlayerAnimator { get => playerAnimator; set => playerAnimator = value; }
    public PlayerHookManager HookManager { get => hookManager; set => hookManager = value; }
    public PlayerSpecialManager SpecialManager { get => specialManager; set => specialManager = value; }

    public Vector2 EjectionValue { get => ejectionValue; set => ejectionValue = value; }


    //Input events
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = new Vector2 (context.ReadValue<Vector2>().x, 0);
    }

    public void OnCameraMove(InputAction.CallbackContext context)
    {
        cameraInput = context.ReadValue<Vector2>();
        playerCrosshair.Input = cameraInput;
        //Debug.Log(cameraInput.ToString("F5"));
        //TODO : Handle mouse input + Fix jittering
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        isJumpPressed = context.action.triggered;
        needNewJumpPressed = false;
    }

    public void OnPunch(InputAction.CallbackContext context)
    {
        isPunchPressed = context.action.triggered;
    }

    public void OnKick(InputAction.CallbackContext context)
    {
        isKickPressed = context.action.triggered;
    }

    public void OnUse(InputAction.CallbackContext context)
    {
        isUsePressed = context.action.triggered;
    }

    public void OnHook(InputAction.CallbackContext context)
    {
        isHookPressed = context.action.triggered;
        hookManager.NeedNewHookPressed = false;

    }

    public void OnThrow(InputAction.CallbackContext context)
    {
        isGrabPressed = context.action.triggered;
    }

    public void OnSpecialUse(InputAction.CallbackContext context)
    {
        isSpecialPressed = context.action.triggered;
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        isDashPressed = context.action.triggered;
        dashManager.NeedNewDashPressed = false;
    }

    void Awake()
    {
        //Instantiate & Get
        rb = gameObject.GetComponent<Rigidbody2D>();

        groundChecker2D = gameObject.GetComponent<GroundChecker2D>();
        wallChecker2D = gameObject.GetComponent<WallChecker2D>();

        healthManager = gameObject.AddComponent<PlayerHealthManager>();
        dashManager = gameObject.AddComponent<PlayerDashManager>();
        specialManager = gameObject.AddComponent<PlayerSpecialManager>();
        statusEffectManager = gameObject.AddComponent<StatusEffectManager>();
        hookManager = gameObject.GetComponentInChildren<PlayerHookManager>();
        playerCrosshair = gameObject.GetComponentInChildren<PlayerCrosshair>();
        
        playerAnimator = gameObject.GetComponent<Animator>();

        //Behaviour assignation
        gravityBehaviour = gameObject.GetComponent<IGravityBehaviour>();
        jumpBehaviour = gameObject.GetComponent<IJumpBehaviour>();

        playerStates = new PlayerStateManager(this);

        //Setup & Init
        dashManager.PlayerDashManagerSetup(playerDashData, this);
        hookManager.PlayerHookManagerSetup(playerHookData, this);
        specialManager.PlayerSpecialManagerSetup(playerSpecialData, this);
        healthManager.SetupHealthManager(playerHealthData, this);
        statusEffectManager.Context = this;
        currentState = playerStates.GetState<PlayerGroundedState>();
        currentState.EnterState();

        playerHUDManager.PlayerController = this;
    }

    private void FixedUpdate()
    {
        //FSM logic
        currentState.UpdateStates();
        //Debug.Log(currentState);
        //Movement 
        ComputeMovement();
        ComputeCameraOffset();
    }

    private void LateUpdate()
    {
        //UI update
        playerHUDManager.UpdateUI();
    }

    private void ComputeMovement() //TODO? : use movement computation script
    {
        Vector2 externalForce = Vector2.zero;
        foreach (Vector2 force in externalForces)
        {
            externalForce += force;
        }
        rb.velocity = jumpBehaviour.GetValue() + movementValue + gravityBehaviour.GetValue() + externalForce; 
        externalForces.Clear();
    }

    public void ComputeCameraOffset()
    {
        Vector3 movementOffset = (cameraInput * playerCrosshair.CrosshairDistance * cameraAimMultiplicator);
        Vector3 aimOffset = (movementInput * cameraMovementMultiplicator);
        //TODO? : Reference to CameraController?
        GameManager.Instance.CameraManager.currentCameraController.CurrentAnchor.AddCameraMovementInput(movementOffset); 
        GameManager.Instance.CameraManager.currentCameraController.CurrentAnchor.AddCameraMovementInput(aimOffset); 
    }

    public override void Hit(bool downed, Vector2 ejectionForce)
    {
        statusEffectManager.IsDowned = downed;
        ejectionValue = ejectionForce;
    }
}
