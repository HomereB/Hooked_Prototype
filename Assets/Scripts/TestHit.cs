using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHit : MonoBehaviour
{
    public bool downing;
    public float stunTime;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IHitable hitable = collision.gameObject.GetComponent<IHitable>();
        if (hitable != null)
            hitable.Hit(stunTime, true, Vector2.up * 10f + Vector2.right * 4f);
    }
}
