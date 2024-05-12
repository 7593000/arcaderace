using System;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public abstract class CarComponent : MonoBehaviour
{
    [SerializeField]
    protected SettingsCar _settings;
    [SerializeField]
    protected Rigidbody _rb;
    [SerializeField]
    protected Transform _bodyCar;

    private Material _carMaterial;
    protected Movement _movement;

    [SerializeField, Range(100f, 4000f)]
    private float _torque = 500;
    /// <summary>
    /// Усиление торможения
    /// </summary>
    [SerializeField, Range(1f, 650f)]
    private float _brakeAcseleration = 100.0f;
    /// <summary>
    /// TODO => не используется
    /// </summary>
    [SerializeField, Range(1f, 150f)]
    private float _turnSensitivity = 1.0f;
    /// <summary>
    /// Максимальный угол поворота колес
    /// </summary>
    [SerializeField, Range(0f, 60f)]
    private float _maxSteerAngle = 30f;

    [SerializeField]
    private Vector3 _centerOfMass;

    protected bool _useBrake = false;
    protected float _moveInput;
    private float _steerInput;

    [SerializeField]
    private List<Wheel> _wheels = new();

    public List<Wheel> GetListWheel => _wheels;
    /// <summary>
    /// крутящий момент
    /// </summary>
    public float GetTorgue => _torque;
    /// <summary>
    /// Сила торможения
    /// </summary>
    public float GetBrakeAsc => _brakeAcseleration;
    /// <summary>
    /// Максимальный развал колес 
    /// </summary>
    public float GetMaxAngle => _maxSteerAngle;
    /// <summary>
    /// Статус торзмаза I-O
    /// </summary>
    public bool GetBrakeStatus => _useBrake;
    /// <summary>
    /// инпут движения
    /// </summary>
    public float GetInputMove => _moveInput;
    /// <summary>
    /// инпут поворота
    /// </summary>
    public float GetInputTurne => _steerInput;

    /// <summary>
    /// чувствительность поворота
    /// </summary>
    public float GetTurnSensitivity => _turnSensitivity;
    /// <summary>
    /// Получить угол поворта колес 
    /// </summary>
    public float GetAngle { get; set; }
    /// <summary>
    /// Скорость авто magnitude
    /// </summary>
    public float GetVelocity => _rb.velocity.magnitude;

    [Serializable]
    public struct Wheel
    {

        public Transform wheelMode;

        public WheelCollider wheelCollider;

        public Axes axed;

    }

    private void SettingsDefalult()
    {
        SetClearance(_settings.GetClearance);
        SetStiffness(_settings.GetStiffness);
        SetDamper(_settings.GetDamper);
        SetCollapse(_settings.GetCollapse);
        SetTexture(_settings.GetTexture.name);
    }

    protected virtual void SetupCarSettings()
    {
        SettingsDefalult();



    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    protected void SetClearance(float value)
    {

        Vector3 newPosition = _bodyCar.localPosition;
        newPosition.y = value;
        _bodyCar.localPosition = newPosition;
    }
    /// <summary>
    /// Жесткость пружины
    /// </summary>
    /// <param name="value"></param>
    protected void SetStiffness(float value)
    {
        JointSpring suspensionSpring;

        foreach (Wheel wheel in _wheels)
        {
            suspensionSpring = wheel.wheelCollider.suspensionSpring;
            suspensionSpring.spring = value;

            wheel.wheelCollider.suspensionSpring = suspensionSpring;
        }
    }

    protected void SetDamper(float value)
    {

        JointSpring suspensionSpring;

        foreach (Wheel wheel in _wheels)
        {
            suspensionSpring = wheel.wheelCollider.suspensionSpring;
            suspensionSpring.damper = value;

            wheel.wheelCollider.suspensionSpring = suspensionSpring;
        }
    }
    /// <summary>
    /// Развал колеса , максимальный угол поворота колеа. 
    /// </summary>
    /// <param name="value"></param>
    protected void SetCollapse(float value)
    {
        _maxSteerAngle = value;

    }
    public void SetTexture(string texture)
    {
        if (_carMaterial == null)
            return;

        Texture textureCar = Resources.Load<Texture>("Textures/" + texture);

        if (textureCar != null)
        {
            _carMaterial.mainTexture = textureCar;
        }
        else
        {
            Debug.Log("Ошибка загрузки текстуры, выбрана стандартная");
            _carMaterial.mainTexture = _settings.GetTexture;
        }


    }



    /// <summary>
    /// Использовать тормоз 
    /// </summary>
    /// <param name="value"></param>
    public void UseBrake(bool value)
    {
        _useBrake = value;
    }
    /// <summary>
    /// Передача инпута управления
    /// </summary>
    /// <param name="move"></param>
    public void SetMoveInput(float move)
    {
        _moveInput = move;


    }
    /// <summary>
    /// передать инпут поворота 
    /// </summary>
    /// <param name="steer"></param>
    public void SetSteerInput(float steer)
    {

        _movement.Steer(steer);

    }
    protected abstract void GetInput();
 
    protected virtual void OnValidate()
    {
        _rb = GetComponent<Rigidbody>();
    }

    protected virtual void Start()
    {
        _rb.centerOfMass = _centerOfMass;
        _carMaterial = _bodyCar.GetComponent<MeshRenderer>().material;

        SetupCarSettings();
    }


    protected virtual void Update()
    {
        GetInput();

    }

    protected void FixedUpdate()
    {
        _movement.UpdateInstal();

    }




}







