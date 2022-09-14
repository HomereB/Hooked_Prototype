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
    public TMP_Text barText;

    //public Animator gaugeAnimator;
    bool isDisplayed = true;
    // Update is called once per frame
    void Update()
    {
/*        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            float p = Random.Range(0f, 3f);
            FillGauge(p,true,true);
            UpdateGaugeText(p.ToString());
            isDisplayed = !isDisplayed;
            DisplayText(isDisplayed);
        }*/
    }

    public void FillGauge(float percentage,bool fillGauge,bool fillBackground)
    {
        if(fillGauge)
            Fill(gaugeImages, percentage);
        if(fillBackground)
            Fill(gaugeUpdateImages, percentage);
    }

    private void Fill(List<Image> gauges, float percentage)
    {
        for (int i = 0; i < gauges.Count; i++)
        {
            if (percentage >= i + 1)
                gauges[i].fillAmount = 1;
            else if (percentage < i + 1 && percentage > i)
                gauges[i].fillAmount = percentage - i;
            else
                gauges[i].fillAmount = 0;
        }
    }

    public void DisplayText(bool isDisplayed)
    {
        if(barText != null)
            barText.enabled = isDisplayed;
    }

    public void UpdateGaugeText(string newText)
    {
        if (barText != null)
            barText.text = newText;
    }
}
