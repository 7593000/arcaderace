using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class SliderUIComponent : MonoBehaviour
{


    [SerializeField]
    protected TMP_Text _text;
    [SerializeField]
    private DataSettings _typeSleder;

    public void LoadEngine(GarageEngine garage)
    {

        IReadOnlyDictionary<DataSettings, string> temp = garage.GetDictionarySettings;



        string value = temp.FirstOrDefault(pair => pair.Key == _typeSleder).Value;

        LoadData(value);


    }

    protected void LoadData(string value)
    {

        _text.text = value.ToString();

        float parseData = float.Parse(value);
        if (parseData > 100) //для пропуска клиренса и угла колес 
        {
            parseData /= 10000;
        }
        GetComponent<Slider>().value = parseData;

    }


    public abstract void EnterText(Slider slider);


}
