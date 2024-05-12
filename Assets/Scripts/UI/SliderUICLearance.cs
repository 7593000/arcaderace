using UnityEngine.UI;

public class SliderUICLearance : SliderUIComponent
{

    public override void EnterText(Slider slider)
    {
        int tempValue = (int)(slider.value * 1000);
        _text.text = tempValue.ToString();
    }
}
