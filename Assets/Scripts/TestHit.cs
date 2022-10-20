using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHit : MonoBehaviour
{
    public bool downing;
    public EffectData stunData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController controller = collision.gameObject.GetComponent<PlayerController>();
        if (controller != null)
        {   
            StunEffect stun = controller.gameObject.AddComponent<StunEffect>();
            stun.EffectData = stunData;
            controller.StatusEffectManager.AddEffect(stun);
            controller.Hit(true, Vector2.up * 10f + Vector2.right * 4f);
        }
    }
}
