using UnityEngine;


[CreateAssetMenu(fileName = "_SettingsCar", menuName = "Configuration/Settings Car", order = 1)]

public class SettingsCar : ScriptableObject
{
    [Header("Дефолтные значения настроек авто")]
    [SerializeField]
    private float _clearance = 0f;
    [SerializeField]
    private float _spring = 7000f;
    [SerializeField]
    private float _damper = 4500f;
    [SerializeField]
    private float _collapse = 35f;

    [ SerializeField]
    private Texture _texture;


    public float GetStiffness => _spring ;
    public float GetClearance => _clearance;
    public float GetCollapse => _collapse;
    public float GetDamper => _damper;
    public Texture GetTexture => _texture ;
}

