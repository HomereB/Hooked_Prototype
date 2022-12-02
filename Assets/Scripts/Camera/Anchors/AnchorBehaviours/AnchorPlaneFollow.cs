using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorPlaneFollow : CameraAnchor
{
    private Vector3 desiredPosition;

    [SerializeField]
    private GameObject target;

    [SerializeField]
    private Vector3 offset; // TODO? : useful?

    [SerializeField]
    private Vector2 anchorBoundsX;
    [SerializeField]
    private Vector2 anchorBoundsY;
    [SerializeField]
    private Vector2 anchorBoundsZ;

    private List<Vector3> cameraMovementInputs;

    public GameObject Target { get => target; set => target = value; }

    // Start is called before the first frame update
    void Start()
    {
        AnchorSetup();
    }

    public void FollowTarget()
    {
        desiredPosition = target.transform.position + offset;
    }

    public override void SetAnchorPosition(Vector3 position)
    {
        desiredPosition = position;
    }

    private void BoundDesiredPosition(ref Vector3 currentPosition)
    {
        float currentX = currentPosition.x;
        if (anchorBoundsX != null)
            currentX = Mathf.Clamp(currentPosition.x, anchorBoundsX.x, anchorBoundsX.y);

        float currentY = currentPosition.y;
        if (anchorBoundsY != null)
            currentY = Mathf.Clamp(currentPosition.y, anchorBoundsY.x, anchorBoundsY.y);

        float currentZ = currentPosition.z;
        if (anchorBoundsZ != null)
            currentZ = Mathf.Clamp(currentPosition.z, anchorBoundsZ.x, anchorBoundsZ.y);

        currentPosition = new Vector3(currentX, currentY, currentZ);
    }

    public override void AnchorSetup()
    {
        desiredPosition = transform.position;
        ComputeBounds();
        cameraMovementInputs = new List<Vector3>();
    }

    private void ComputeBounds()
    {
        //TODO : automation
    }

    public override void AddCameraMovementInput(Vector3 input)
    {
        cameraMovementInputs.Add(input);
    }

    public override void ComputeAnchorMovement()
    {
        if (Target != null)
            FollowTarget(); //TODO? : necessary or just position?

        Vector3 input = Vector3.zero;

        foreach(Vector3 movement in cameraMovementInputs)
        {
            input += movement;
        }
        Debug.Log(input);
        Vector3 currentPosition = Vector3.Lerp(transform.position, desiredPosition + input, anchorData.lerpIntensity);

        BoundDesiredPosition(ref currentPosition);

        transform.position = currentPosition;       
        
        cameraMovementInputs.Clear();
    }
}
