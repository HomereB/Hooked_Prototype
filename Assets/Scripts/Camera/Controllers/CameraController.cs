using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CameraData cameraData;

    [SerializeField]
    Camera currentCamera;

    //Target & Anchor

    [SerializeField]
    CameraAnchor currentAnchor;
    [SerializeField]
    Transform currentTarget;

    //Transform modificators

    [SerializeField]
    Vector3 currentPosition;
    [SerializeField]
    Vector3 currentOffset;
    [SerializeField]
    Quaternion currentRotation;

    [SerializeField]
    Vector3 currentDisplacement;
    [SerializeField]
    Quaternion currentSway;
    [SerializeField]
    private bool isTraveling = false;

    //Inputs
    [SerializeField]
    private Vector3 movementInput;
    [SerializeField]
    private Vector3 rotationInput;
    [SerializeField]
    private float zoomInput;

    //Test data (will disappear)

    public CameraAnchor testAnchor;
    public List<Vector3> startpos;
    public List<Vector3> endpos;
    public List<Quaternion> startrot;
    public List<Quaternion> endrot;
    public List<bool> attached;
    public List<float> time;
    public List<float> waitingTime;

    //Movement Coroutine

    private IEnumerator currentMovementCoroutine;

    //FSM Components

    private CameraBaseState currentState;
    private CameraStateManager cameraStates;

    //Get & Set

    public CameraBaseState CurrentState { get => currentState; set => currentState = value; }
    public CameraAnchor CurrentAnchor { get => currentAnchor; set => currentAnchor = value; }
    public Transform CurrentTarget { get => currentTarget; set => currentTarget = value; }
    public Vector3 CurrentOffset { get => currentOffset; set => currentOffset = value; }
    public Vector3 CurrentPosition { get => currentPosition; set => currentPosition = value; }
    public Quaternion CurrentRotation { get => currentRotation; set => currentRotation = value; }
    public bool IsTraveling { get => isTraveling; set => isTraveling = value; }
    public Vector3 MovementInput { get => movementInput; set => movementInput = value; }
    public Vector3 RotationInput { get => rotationInput; set => rotationInput = value; }
    public float ZoomInput { get => zoomInput; set => zoomInput = value; }

    void Awake()
    {
        currentCamera = gameObject.GetComponent<Camera>();
        
        currentPosition = currentCamera.transform.position;
        currentRotation = currentCamera.transform.rotation; 
        
        cameraStates = new CameraStateManager(this);
        InitialStateConfiguration();
    }

    void InitialStateConfiguration() 
    {
        if(currentAnchor != null)       
            currentState = cameraStates.GetState<CameraAnchoredState>();
        else
            currentState = cameraStates.GetState<CameraFreeMovementState>();
        currentState.EnterState();
    }

    // Update is called once per frame
    void Update()
    {
        movementInput = Vector3.zero;
        rotationInput = Vector3.zero;
        zoomInput =0f;

        Debug.Log(currentState);
        currentState.UpdateStates();
        //TODO : Handle obstacles (raycast probably)
    }

    private void LateUpdate()
    {
        ComputeTransform();
    }

    //TODO : Check bounds

    public void RotateCamera(Vector2 rotation)
    {
        if (currentTarget == null)
        {
            Vector3 upVector = Vector3.up;
            if (currentAnchor != null)
            {
                upVector = currentAnchor.transform.up;
            }
            float angleY = rotation.y * cameraData.standardCameraRotationSpeed * Time.deltaTime;
            Quaternion verticalRotation = Quaternion.AngleAxis(angleY, currentCamera.transform.right);
            float previewedAngle = Vector3.Angle(verticalRotation * currentCamera.transform.forward, upVector);
            if (previewedAngle > cameraData.verticalRotationThresholdUp || previewedAngle < cameraData.verticalRotationThresholdDown) //TODO : better threshold handling
            {
                verticalRotation = Quaternion.identity;
            }
            float angleX = rotation.x * cameraData.standardCameraRotationSpeed * Time.deltaTime;
            Quaternion horizontalRotation = Quaternion.AngleAxis(angleX, Vector3.up);
            currentRotation = verticalRotation * horizontalRotation * currentRotation;
        }
    }

    public void RotateCameraAroundTarget(Vector2 rotation)
    {
        if(currentTarget != null && currentAnchor == null)
        {
            Quaternion horizontalRotation = Quaternion.AngleAxis(rotation.x, currentTarget.up);
            Quaternion verticalRotation = Quaternion.AngleAxis(rotation.y, currentCamera.transform.right);
            float previewedAngle = Vector3.Angle(verticalRotation * currentOffset, currentTarget.up);
            if (previewedAngle > cameraData.verticalRotationThresholdUp || previewedAngle < cameraData.verticalRotationThresholdDown)
            {
                verticalRotation = Quaternion.identity;
            }
            currentOffset = horizontalRotation * verticalRotation * currentOffset;
        }
    }

    public void MoveCamera(Vector3 movement)
    {
        currentPosition += movement * cameraData.standardCameraMovementSpeed * Time.deltaTime;
    }

    public void ZoomCamera(float intensity)
    {
        Vector3 previewedOffset = currentOffset + currentCamera.transform.forward * intensity;     
        if(previewedOffset.magnitude <= cameraData.maxZoomThreshold && previewedOffset.magnitude >= cameraData.minZoomThreshold)
        {
            currentOffset = previewedOffset;
        }
    }

    public void ResetZoom()
    {
        currentOffset = Vector3.zero;
    }

    private void ComputeTransform()
    {
        currentCamera.transform.position = currentPosition + currentDisplacement;
        currentCamera.transform.rotation = currentSway * currentRotation;
        currentCamera.transform.position += currentOffset;
        currentDisplacement = Vector3.zero;
        currentSway = Quaternion.identity;
    }

    public void SetDisplacement(Vector3 displacement)
    {
        currentDisplacement = displacement;
    }

    public void SetSway(Quaternion sway)
    {
        currentSway = sway;
    }

    public void AttachToAnchor(CameraAnchor anchor, Vector3 offset, Quaternion rotationOnAttach, float travelTime)
    {
        isTraveling = true;
        if(currentMovementCoroutine!=null)
            StopCoroutine(currentMovementCoroutine); 
        currentMovementCoroutine = MoveCameraTo(transform.position, anchor.transform.position + offset, transform.rotation, rotationOnAttach, anchor, travelTime);
        StartCoroutine(currentMovementCoroutine);
    }

    public void DetachFromAnchor()
    {
        currentAnchor = null;
        currentPosition = transform.position;
    }

    public void LockToTarget(Transform target)
    {
        currentTarget = target;
        LookAtPosition(target.position);
        if(currentAnchor == null)
        {
            currentOffset = currentPosition - currentTarget.position;
            currentPosition = currentTarget.position;
        }
    }

    public void ClearTarget()
    {
        currentTarget = null;
        currentPosition  = transform.position;
        currentOffset = Vector3.zero;
    }

    public void FollowAnchor()
    {
        if(currentAnchor!=null)
            currentPosition = currentAnchor.transform.position;
    }

    public void LookAtPosition(Vector3 position)
    {
        Vector3 direction = position - currentCamera.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        currentRotation = lookRotation;
    }

    public void TeleportCamera(Vector3 position, Quaternion rotation, CameraAnchor cameraAnchor, Transform cameraTarget)
    {
        currentAnchor = cameraAnchor;
        currentTarget = cameraTarget;
        currentCamera.transform.position = position;
        currentRotation = rotation;
    }

    private IEnumerator MoveCameraTo(Vector3 startPosition,Vector3 endPosition, Quaternion startRotation, Quaternion endRotation, CameraAnchor cameraAnchor, float time)
    {
        isTraveling = true;

        currentAnchor = null;
        float elapsedTime = 0;

        while(elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            currentCamera.transform.position =  Vector3.Lerp(startPosition, endPosition, elapsedTime / time) + currentDisplacement;
            currentRotation = currentSway * Quaternion.Slerp(startRotation, endRotation, elapsedTime / time);
            yield return null;
        }
        currentAnchor = cameraAnchor;
        isTraveling = false;
    }

    public IEnumerator MoveCameraAlongPath(List<Vector3> startPositions, List<Vector3> endPositions, List<Quaternion> startRotations, List<Quaternion> endRotations, List<CameraAnchor> cameraAnchors, List<float> time, List<float> waitingTime)
    {
        currentAnchor = null;
        isTraveling = true;
        for (int i = 0; i < startPositions.Count; i++)
        {
            currentMovementCoroutine = MoveCameraTo(startPositions[i], endPositions[i], startRotations[i], endRotations[i], cameraAnchors[i], time[i]);
            StartCoroutine(currentMovementCoroutine);
            yield return new WaitForSeconds(time[i] + waitingTime[i]);
        }
        isTraveling = false;
    }
}