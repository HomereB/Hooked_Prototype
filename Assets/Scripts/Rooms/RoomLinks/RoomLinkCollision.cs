using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLinkCollision : RoomLink
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(collider.tag);
        if (collider.CompareTag("Player"))
        {
            UseLink(collider.gameObject);
        }
    }
}
