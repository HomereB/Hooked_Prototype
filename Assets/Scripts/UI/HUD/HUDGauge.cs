using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class HUDGauge : MonoBehaviour
{
    private float gaugeFillPercentage;

    public List<Image> gaugeImages;
    public List<Image> gaugeFramesImages;
    public List<Image> gaugeUpdateImages;
    private TMP_Text barText;

    //public Animator gaugeAnimator;

    // Update is called once per frame
    void Update()
    {
/*        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            float p = Random.Range(0f, gaugeImages.Count);
            Debug.Log(p);
            FillGauge(p);
        }*/
    }

    public void FillGauge(float percentage)
    {
        for(int i = 0; i < gaugeImages.Count; i++)
        {
            if(percentage >= i + 1)
                gaugeImages[i].fillAmount = 1;
            else if (percentage < i + 1 && percentage > i)
                gaugeImages[i].fillAmount = percentage - i;
            else
                gaugeImages[i].fillAmount = 0;
        }
    }
}
