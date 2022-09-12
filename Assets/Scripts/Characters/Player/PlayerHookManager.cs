using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHookManager : MonoBehaviour
{
    private PlayerHookData playerHookData;
    private bool needNewHookPressed;

    private float currentCooldownTimer;

    private Vector3 hookDirection;
    private bool hookHit;
    private PlayerController playerController;

    private LineRenderer ropeRenderer;
    private SpriteRenderer hookRenderer;
    private Collider2D hookCollider;

    private Vector3 hitPosition;
    private float currentTravelTimer;

    public HookStatus hookStatus;
    public bool CanStartHook { get => (!needNewHookPressed && hookStatus == HookStatus.Available); }
    public bool HookHit { get => hookHit; set => hookHit = value; }
    public bool NeedNewHookPressed { get => needNewHookPressed; set => needNewHookPressed = value; }
    public float CurrentTravelTimer { get => currentTravelTimer; set => currentTravelTimer = value; }
    public Vector3 HitPosition { get => hitPosition; set => hitPosition = value; }
    public Vector3 HookDirection { get => hookDirection; set => hookDirection = value; }

    // Start is called before the first frame update
    void Start()
    {
        ropeRenderer = GetComponent<LineRenderer>();
        hookRenderer = GetComponent<SpriteRenderer>();
        hookCollider = GetComponent<Collider2D>();
        needNewHookPressed = false;
        ropeRenderer.enabled = false;
        hookRenderer.enabled = false;
        hookCollider.enabled = false;
        gameObject.transform.position = Vector3.zero;

        currentCooldownTimer = playerHookData.hookCooldown;
        hookStatus = HookStatus.Available;
    }

    public void InitializeHookThrow()
    {
        currentTravelTimer = 0;
        hookStatus = HookStatus.Startup;
        ropeRenderer.enabled = true;
        hookRenderer.enabled = true;
        hookCollider.enabled = true;
        GetComponent<Rigidbody2D>().WakeUp();
        hookCollider.transform.position = playerController.Position;
        hookDirection = playerController.CameraInput;
        if(hookDirection == Vector3.zero) //TODO : change for player direction
        {
            hookDirection = playerController.transform.right;
        }
    }

    public void HookCatch()
    {
        hookCollider.enabled = false;
        GetComponent<Rigidbody2D>().Sleep();
        hookStatus = HookStatus.Travel;
        Vector3 travelVector = hitPosition - playerController.Position;
        RaycastHit2D hit = Physics2D.Raycast(playerController.Position, travelVector, travelVector.magnitude, playerHookData.hookLayerMask);
        Debug.DrawRay(playerController.Position, HookDirection * 10f, Color.green, 5);
        if(hit.point!=Vector2.zero)
            hitPosition = hit.point;
        transform.position = hitPosition/* -  HookDirection * 0.15f*/;
    }

    public void EndHook(float cooldown)
    {
        currentTravelTimer = 0;
        hookStatus = HookStatus.Cooldown;
        ropeRenderer.enabled = false;
        hookRenderer.enabled = false;
        gameObject.transform.position = playerController.Position;
        currentCooldownTimer = playerHookData.hookCooldown - cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        ropeRenderer.SetPosition(0, transform.position);
        ropeRenderer.SetPosition(1, transform.parent.position);

        if (hookStatus == HookStatus.Cooldown)
        {
            currentCooldownTimer += Time.deltaTime;
            if (currentCooldownTimer > playerHookData.hookCooldown)
            {
                hookStatus = HookStatus.Available;
                if (playerController.IsHookPressed)
                    NeedNewHookPressed = true;
            }
        }
    }

    public void PlayerHookManagerSetup(PlayerHookData hookData, PlayerController controller)
    {
        playerController = controller;
        playerHookData = hookData;
    }

    public void SetHookVisibility(bool isVisible)
    {
        ropeRenderer.enabled = isVisible;
        hookRenderer.enabled = isVisible;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //TODO : sort contact points

        //Debug.Log(collision.contacts[0].point.ToString("F5"));
        hitPosition = collision.contacts[0].point;
        //Debug.DrawRay(hitPosition, Vector3.up, Color.cyan);
        HookCatch();
    }
}

public enum HookStatus
{
    Available,
    Startup,
    Travel,
    Cooldown
}
