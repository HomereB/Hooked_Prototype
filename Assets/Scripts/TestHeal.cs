using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHeal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EntityHealthManager healthManager = collision.gameObject.GetComponent<EntityHealthManager>();
        if (healthManager != null)
            healthManager.AddToCurrentHealth(20);
    }
}
