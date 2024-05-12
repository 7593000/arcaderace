using UnityEngine.UI;

public class SliderUIDamper : SliderUIComponent
{

    public override void EnterText(Slider slider)
    {
        int tempValue = (int)(slider.value * 10000);
        _text.text = tempValue.ToString();
    }



}
