using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLink : MonoBehaviour
{
    public Vector3 spawnPosition;
    public RoomLink exitLink;

    public bool isActive;


    public virtual void OpenLink(bool isOpen)
    {
        isActive = isOpen;
    }

    public virtual void UseLink(GameObject player)
    {
        player.transform.position = exitLink.spawnPosition;
    }
}
