
using System.Collections;
using UnityEngine;

public class NPCMoveComponent : MonoBehaviour
{

    private CarComponent _carComponent;

    [SerializeField, Range(0f, 50f)]
    private float _booster = 20;
    [SerializeField]
    private float _maxBooster = 40;
    [SerializeField]
    private TrackingPath _tracker;
    [SerializeField]
    private float _sqrTolerance = 10f;
    private Coroutine _corBrake;
    [SerializeField]
    float _speed = 0;

    private bool CheckDistance()
    {
        Vector3 relativePosition = transform.position - _tracker.transform.position;
        float sqrDistance = relativePosition.sqrMagnitude; // Получаем квадрат расстояния между объектами
        float sqrThreshold = _sqrTolerance * _sqrTolerance; // Получаем квадрат допустимого расстояния

        return sqrDistance <= sqrThreshold;

    }

    private float AngleCalculation()
    {
        var directionAB = _tracker.transform.position - transform.position;

        float angle = Vector3.SignedAngle(transform.forward, directionAB, Vector3.up);

        return Mathf.RoundToInt(angle);
    }

    private void Turn()
    {
        float angle = AngleCalculation();
        _carComponent.SetSteerInput(angle);

    }

    private void Move()
    {

        if (CheckDistance())
        {
            _speed = _carComponent.GetVelocity;

            _carComponent.UseBrake(true);
        }
        else
        {
            _speed += _booster * Time.fixedDeltaTime;
            _speed = Mathf.Clamp(_speed, 0, _maxBooster);
            _carComponent.UseBrake(false);
            _carComponent.SetMoveInput(_speed);
        }

    }

    public void BrakeCoroutine(float speedLimit)
    {
        _corBrake ??= StartCoroutine(BrakeSpeed(speedLimit));
    }

    private IEnumerator BrakeSpeed(float speedLimit)
    {


        while (_carComponent.GetVelocity > speedLimit)
        {
            _carComponent.UseBrake(true);
            yield return null;
        }

        _corBrake = null; // Очищаем ссылку на корутину после ее завершения
    }


    private void Start()
    {
        _carComponent.SetMoveInput(300);
    }
    private void FixedUpdate()
    {
        Move();
        Turn();
    }

    private void OnValidate()
    {

        _carComponent ??= GetComponentInChildren<CarComponent>();

    }
}

