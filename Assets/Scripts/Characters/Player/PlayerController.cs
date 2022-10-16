using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IHitable
{
    //Base Motion
    [SerializeField]
    private float playerSpeed = 2f; //TODO : use player motion data


    //Managers
    //TODO : Health Manager / ComboManager
    private PlayerDashManager dashManager;
    private PlayerHookManager hookManager;
    private PlayerSpecialManager specialManager;
    private PlayerHealthManager healthManager;
    [SerializeField]
    private HUDManager playerHUDManager;


    //Crosshair
    [SerializeField]
    private PlayerCrosshair playerCrosshair;


    //Jump
    [SerializeField]
    private int currentJumpAmount = 0;
    [SerializeField]
    private float currentJumpTime = 0;
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
    public PlayerGravityData playerGravityData;
    public PlayerDashData playerDashData;
    public PlayerHookData playerHookData;
    public PlayerSpecialData playerSpecialData;


    //Movement computation elements
    private Vector2 gravityValue = Vector2.zero;
    private Vector2 movementValue = Vector2.zero;
    private Vector2 jumpValue = Vector2.zero;


    //FSM Components
    private PlayerBaseState currentState;
    private PlayerStateManager playerStates;


    //Animation
    private Animator playerAnimator;


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
    public float CurrentJumpTime { get => currentJumpTime; set => currentJumpTime = value; }
    public bool NeedNewJumpPressed { get => needNewJumpPressed; set => needNewJumpPressed = value; }
    public bool CanJump { get => (currentJumpAmount < playerJumpData.maxJumpAmount && !needNewJumpPressed) ? true : false; }
    public bool IsMovementPressed { get => movementInput != Vector2.zero ? true : false; }
    public Vector2 MovementInput { get => movementInput; set => movementInput = value; }
    public bool IsCameraMovementPressed { get => cameraInput != Vector2.zero ? true : false; }
    public Vector2 CameraInput { get => cameraInput; set => cameraInput = value; }
    public bool IsGrounded { get => groundChecker2D.IsGrounded; }
    public bool IsAgainstWallLeft { get => wallChecker2D.IsAgainstWallLeft; }
    public bool IsAgainstWallRight { get => wallChecker2D.IsAgainstWallRight; }
    public bool IsAgainstWall { get => wallChecker2D.IsAgainstWallLeft || wallChecker2D.IsAgainstWallRight; }
    public Vector2 GravityValue { get => gravityValue; set => gravityValue = value; }
    public Vector2 MovementValue { get => movementValue; set => movementValue = value; }
    public Vector2 JumpValue { get => jumpValue; set => jumpValue = value; }
    public float PlayerSpeed { get => playerSpeed; set => playerSpeed = value; }
    public bool CanDash { get => dashManager.CanDash; }
    public bool NeedNewDashPressed { get => dashManager.NeedNewDashPressed; set => dashManager.NeedNewDashPressed = value; }
    public bool NeedNewHookPressed { get => hookManager.NeedNewHookPressed; set => hookManager.NeedNewHookPressed = value; }
    public PlayerDashManager DashManager { get => dashManager; set => dashManager = value; }
    public Vector3 Position { get => gameObject.transform.position; }
    public Animator PlayerAnimator { get => playerAnimator; set => playerAnimator = value; }
    public PlayerHookManager HookManager { get => hookManager; set => hookManager = value; }
    public PlayerSpecialManager SpecialManager { get => specialManager; set => specialManager = value; }
    public PlayerHealthManager HealthManager { get => healthManager; set => healthManager = value; }


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
        groundChecker2D = gameObject.GetComponent<GroundChecker2D>();
        wallChecker2D = gameObject.GetComponent<WallChecker2D>();

        healthManager = gameObject.AddComponent<PlayerHealthManager>();
        dashManager = gameObject.AddComponent<PlayerDashManager>();
        specialManager = gameObject.AddComponent<PlayerSpecialManager>();

        hookManager = gameObject.GetComponentInChildren<PlayerHookManager>();
        playerCrosshair = gameObject.GetComponentInChildren<PlayerCrosshair>();
        
        playerAnimator = gameObject.GetComponent<Animator>();

        playerStates = new PlayerStateManager(this);

        //Setup & Init
        dashManager.PlayerDashManagerSetup(playerDashData, this);
        hookManager.PlayerHookManagerSetup(playerHookData, this);
        specialManager.PlayerSpecialManagerSetup(playerSpecialData, this);
        //Debug.Log(healthManager);
        healthManager.SetupHealthManager(playerHealthData, this);


        currentState = playerStates.GetState<PlayerGroundedState>();
        currentState.EnterState();

        playerHUDManager.PlayerController = this;
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //FSM logic
        currentState.UpdateStates();
        Debug.Log(currentState);
        //Movement 
        gameObject.GetComponent<Rigidbody2D>().velocity = jumpValue + movementValue + gravityValue; //TODO : use movement computation
    }

    private void LateUpdate()
    {
        //UI update
        playerHUDManager.UpdateUI();
    }

    public void Hit()
    {
        throw new System.NotImplementedException();
    }
}
