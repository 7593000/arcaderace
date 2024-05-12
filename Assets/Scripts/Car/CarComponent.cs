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
    /// �������� ����������
    /// </summary>
    [SerializeField, Range(1f, 650f)]
    private float _brakeAcseleration = 100.0f;
    /// <summary>
    /// TODO => �� ������������
    /// </summary>
    [SerializeField, Range(1f, 150f)]
    private float _turnSensitivity = 1.0f;
    /// <summary>
    /// ������������ ���� �������� �����
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
    /// �������� ������
    /// </summary>
    public float GetTorgue => _torque;
    /// <summary>
    /// ���� ����������
    /// </summary>
    public float GetBrakeAsc => _brakeAcseleration;
    /// <summary>
    /// ������������ ������ ����� 
    /// </summary>
    public float GetMaxAngle => _maxSteerAngle;
    /// <summary>
    /// ������ �������� I-O
    /// </summary>
    public bool GetBrakeStatus => _useBrake;
    /// <summary>
    /// ����� ��������
    /// </summary>
    public float GetInputMove => _moveInput;
    /// <summary>
    /// ����� ��������
    /// </summary>
    public float GetInputTurne => _steerInput;

    /// <summary>
    /// ���������������� ��������
    /// </summary>
    public float GetTurnSensitivity => _turnSensitivity;
    /// <summary>
    /// �������� ���� ������� ����� 
    /// </summary>
    public float GetAngle { get; set; }
    /// <summary>
    /// �������� ���� magnitude
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
    /// ��������� �������
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
    /// ������ ������ , ������������ ���� �������� �����. 
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
            Debug.Log("������ �������� ��������, ������� �����������");
            _carMaterial.mainTexture = _settings.GetTexture;
        }


    }



    /// <summary>
    /// ������������ ������ 
    /// </summary>
    /// <param name="value"></param>
    public void UseBrake(bool value)
    {
        _useBrake = value;
    }
    /// <summary>
    /// �������� ������ ����������
    /// </summary>
    /// <param name="move"></param>
    public void SetMoveInput(float move)
    {
        _moveInput = move;


    }
    /// <summary>
    /// �������� ����� �������� 
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







