using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrosshair : MonoBehaviour
{
    private Vector2 position;
    private Vector2 input;
    [SerializeField]
    private Renderer crosshairRenderer;
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private float crosshairDistance;
    [SerializeField]
    private GameObject crosshair;

    public Vector2 Position { get => position; set => position = value; }
    public Vector2 Input { get => input; set => input = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CrosshairVisibility();
        ComputeCrosshairPosition();
    }

    void ComputeCrosshairPosition()
    {
        Vector3 inGameLocalPos = new Vector3(input.x, input.y, 0) ;
        crosshair.transform.position = playerController.Position + inGameLocalPos * crosshairDistance;
    }

    void CrosshairVisibility()
    {
        if(input == Vector2.zero)
        {
            crosshairRenderer.enabled = false;
        }
        else
        {
            crosshairRenderer.enabled = true;
        }
    }
}
