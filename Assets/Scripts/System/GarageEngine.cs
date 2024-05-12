using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GarageEngine : MonoBehaviour
{
    [SerializeField]
    private List<HoverImage> _colorCar = new();

    private SliderUIComponent[] _sliderUI;
    
    [SerializeField, Range( 0f , 1f )]
    private float _opacityDefault = 0.5f;
    
    [SerializeField]
    private SettingsCar _settingsCar;
    
    [Space]
    [Header( "Настройки авто" )]
    [SerializeField]
    private CarGarage _car;
    private Material _carMaterial;

    private float _stiffness;
    private float _collapse;
    private float _damper;
    private float _clearance;
    private string _texture;

    private SceneLoader _loader;

    private Dictionary<DataSettings , string> _dictionarySettings = new();

    public IReadOnlyCollection<HoverImage> GetImagesList => _colorCar;

    public IReadOnlyDictionary<DataSettings , string> GetDictionarySettings => _dictionarySettings;


    private void OnValidate()
    {
        _sliderUI = FindObjectsOfType<SliderUIComponent>();
        _loader =  GetComponentInChildren<SceneLoader>();
    }


    private void Start()
    {
        _carMaterial = _car.GetBody.GetComponent<MeshRenderer>().material;

        if ( DataPlayerPrefs.CheckedHasKey( SaveDataType.SettingsPlayer ) )
        {
            Dictionary<string , string> dictionary = DataPlayerPrefs.ParceHasKey();


            float collapse = float.IsNaN( DataPlayerPrefs.ParceFloat( dictionary , DataSettings.Collapse ) ) ? _settingsCar.GetCollapse : DataPlayerPrefs.ParceFloat( dictionary , DataSettings.Collapse );
            float stiffness = float.IsNaN( DataPlayerPrefs.ParceFloat( dictionary , DataSettings.Stiffness ) ) ? _settingsCar.GetStiffness : DataPlayerPrefs.ParceFloat( dictionary , DataSettings.Stiffness );
            float damper = float.IsNaN( DataPlayerPrefs.ParceFloat( dictionary , DataSettings.Damper ) ) ? _settingsCar.GetDamper : DataPlayerPrefs.ParceFloat( dictionary , DataSettings.Damper );
            float clearance = float.IsNaN( DataPlayerPrefs.ParceFloat( dictionary , DataSettings.Clearance ) ) ? _settingsCar.GetClearance : DataPlayerPrefs.ParceFloat( dictionary , DataSettings.Clearance );

            dictionary.TryGetValue( DataSettings.Texture.ToString() , out string texture );

            LoadSettings( stiffness , collapse , clearance , damper, texture );


        }
        else
        {
            LoadSettings( _settingsCar.GetStiffness , _settingsCar.GetCollapse , _settingsCar.GetClearance , _settingsCar.GetDamper, _settingsCar.GetTexture.name );

        }


        foreach ( HoverImage color in _colorCar )
        {

            if ( color.GetTextureName == _dictionarySettings[ DataSettings.Texture ] )
            {
                color.GetComponent<HoverImage>().ColorDefault( _opacityDefault , true , this );
                color.GetComponent<Image>().color = new Color( 1f , 1f , 1f , 1f );
              
            }
            else
            {
                color.GetComponent<HoverImage>().ColorDefault( _opacityDefault , false , this );
                color.GetComponent<Image>().color = new Color( 1f , 1f , 1f , _opacityDefault );
            }
           
        }
        foreach ( SliderUIComponent slider in _sliderUI )
        {
            slider.LoadEngine( this );
        }

        _car.SetTexture( _dictionarySettings[ DataSettings.Texture ] );
    }
    
    private void LoadSettings( float stiffness , float collapse , float clearance ,float damper,  string texture )
    {

        AddDataToDictionary( DataSettings.Collapse , collapse.ToString() );
        AddDataToDictionary( DataSettings.Clearance , clearance.ToString() );
        AddDataToDictionary( DataSettings.Stiffness , stiffness.ToString() );
        AddDataToDictionary( DataSettings.Damper , damper.ToString() );
        AddDataToDictionary( DataSettings.Texture , texture );

    }

    private string CreatingStringFromDictionary( Dictionary<DataSettings , string> data )
    {
        string stringData = "";

        foreach ( KeyValuePair<DataSettings , string> setting in data )
        {
            string tempData = $"{setting.Key}:{setting.Value}";

            if ( data.Keys.Last() != setting.Key )
            {
                tempData += ";";
            }

            stringData += tempData;
        }
        return stringData;
    }

    /// <summary>
    /// Назначить текстуру для авто игрока
    /// </summary>
    /// <param name="texture"></param>
    public void SetTexture( Texture texture )
    {
        if (_carMaterial == null)
            Debug.Log("ERORR MATERIAL");
     
        _carMaterial.mainTexture = texture;
        _texture = texture.name;


    }

    public void SetStiffness( Slider slider )
    {
        float scaledValue = 10000f;

        float value = slider.value * scaledValue;
        value = Mathf.Floor( value );

        _stiffness = value;

    }

    public void SetDamper( Slider slider )
    {
        float scaledValue =  10000f;

        float value = slider.value * scaledValue;

        value = Mathf.Floor( value );

        _damper = value;

    }

    /// <summary>
    /// Установить максимальный угол поворота колеса
    /// </summary>
    /// <param name="slider"></param>
    public void SetCollapse( Slider slider )
    {
        float value = slider.value;
        _car.SetCollapse( slider.value );
        value = MathfRound( value , 100 );
        _collapse = value;
     }

    /// <summary>
    /// Установить клиренс
    /// </summary>
    public void SetClearance( Slider slider )
    {
        float value = slider.value;
        _car.SetClearance( value );

        _clearance = value;
    }
    
    /// <summary>
    /// Добавить данные в словарь 
    /// </summary>
    /// <param name="type">Тип данных</param>
    /// <param name="str"> данные</param>
    private void AddDataToDictionary( DataSettings type , string str )
    {
        if ( _dictionarySettings.ContainsKey( type ) )
        {
            _dictionarySettings[ type ] = str;
        }
        else
        {
            _dictionarySettings.Add( type , str );

        }
    }

    private float MathfRound( float number , int rounding ) => Mathf.Round( number * rounding ) / rounding;
  
    
    public void SaveData()
    {
        LoadSettings( _stiffness , _collapse , _clearance , _damper , _texture );

        string stringData = CreatingStringFromDictionary( _dictionarySettings );

        DataPlayerPrefs.Save( SaveDataType.SettingsPlayer , stringData );

    }
    public void StartGame()
    {
        _loader.LoadSceneAsync( "Track01" );
    }
    #region Exit Game
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE_WIN && !UNITY_EDITOR
    Application.Quit();
#endif
    }
    #endregion



}
