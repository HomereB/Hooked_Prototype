using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CameraAnchor : MonoBehaviour
{
    public AnchorData anchorData;

    public abstract void AnchorSetup();
    public abstract void ComputeAnchorMovement(Vector3 input);
    public abstract void SetAnchorPosition(Vector3 position);
}
