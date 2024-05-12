using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderUICollapse : SliderUIComponent
{
    public override void EnterText(Slider slider)
    {
        float value = slider.value;
        int tempValue = (int)(slider.value );
        _text.text = tempValue.ToString();
    }
}
