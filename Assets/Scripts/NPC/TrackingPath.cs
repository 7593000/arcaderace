using UnityEngine;
/// <summary>
/// Отслеживание пути и передачи данных
/// </summary>
public class TrackingPath : MonoBehaviour
{

    private TargetsManager _targetsManager;

    private Rigidbody _rb;
    [SerializeField]
    private NPCComponent _bot;
    [SerializeField, Range(0f, 15f)]
    private float _distance = 10f;

    [SerializeField]
    private TargetPoint _targetPoint;
    [SerializeField]
    private TargetPoint _nextPoint;
    [SerializeField]
    private bool _isMove = false;
    [SerializeField]
    public float _sqrTolerance = 0.01f; // Квадрат погрешности
    private int _indexPoint = 0;

    //todo => temp потом, получение данных у NPC + скорость опережения. 
    [SerializeField]
    private float _MaxSpeed = 120;
    [SerializeField]
    private float _speed = 10;
    [SerializeField]
    private float _acceleration = 2f; // Ускорение
    [SerializeField]
    private float _deceleration = 4f; // Замедление
    public Transform GetPositionPoint => _targetPoint.transform;
    public float GetVelocity => _rb.velocity.magnitude;



    private void OnValidate()
    {
        _rb ??= GetComponent<Rigidbody>();

        _targetsManager ??= FindFirstObjectByType<TargetsManager>();
        Vector3 markerPosition = _bot.transform.position + transform.forward * _distance;
        transform.position = markerPosition;
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent<BrakePoint>(out var brakePoint))
        {
            float speedLimit = brakePoint.SpeedLimit;
            float speedTracker = _bot.GetVelocity;


            if (speedTracker > speedLimit)
            {


                _bot.UseBrake(speedLimit);
            }


        }
    }



    private void Start()
    {
        SettingDirection();

        transform.position = _targetsManager.GetTargets[0].transform.position;
        transform.LookAt(_targetPoint.transform);


    }
    private void FixedUpdate()
    {

        if (_isMove) MovementTarget();
    }

    /// <summary>
    /// Назначение начальной и конечной точки движения
    /// </summary>
    private void SettingDirection()
    {

        if (_indexPoint >= _targetsManager.GetTargets.Count)
        {
            _isMove = false;

            return;
        }

        _targetPoint = _targetsManager.GetTargets[_indexPoint];

        GetPositionTarget(transform);

        _indexPoint++;
        if (_targetsManager.GetTargets[_indexPoint] == null)
        {
            _indexPoint = 0;
        }
        _nextPoint = _targetsManager.GetTargets[_indexPoint];

    }



    private bool CheckDistance()
    {

        Vector3 relativePosition = _bot.transform.position - transform.position;
        float sqrDistance = relativePosition.sqrMagnitude; // Получаем квадрат расстояния между объектами
        float sqrThreshold = _distance * _distance; // Получаем квадрат допустимого расстояния

        return sqrDistance < sqrThreshold;



    }

    /// <summary>
    /// Проверка растояния до цели 
    /// </summary>
    /// <returns></returns>
    private bool IsAtTarget()
    {

        float sqrDistance = (transform.position - _targetPoint.transform.position).sqrMagnitude;

        return sqrDistance <= _sqrTolerance;

    }

    private void MovementTarget()
    {

        if (IsAtTarget())
        {
            SettingDirection();
        }



        if (CheckDistance())
        {


            if (_bot.GetVelocity > GetVelocity)
            {
                _speed += _bot.GetVelocity + _acceleration * Time.fixedDeltaTime;
                _speed = Mathf.Clamp(_speed, 0, _MaxSpeed);
            }
            else
            {
                _speed += _speed + _acceleration * Time.fixedDeltaTime;
                _speed = Mathf.Clamp(_speed, 0, _MaxSpeed);

            }
        }
        else
        {


            _speed -= _deceleration * Time.fixedDeltaTime;
            _speed = Mathf.Clamp(_speed, 0, _MaxSpeed);

        }


        transform.position = Vector3.MoveTowards(transform.position, _targetPoint.transform.position, _speed * Time.fixedDeltaTime);


    }





    /// <summary>
    /// Получить данные о позиции маячка 
    /// </summary>
    public void GetPositionTarget(Transform unit)
    {

        unit.transform.LookAt(_targetPoint.transform);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = CheckDistance() ? Color.red : Color.blue;

        Gizmos.DrawLine(_bot.transform.position, transform.position);
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(transform.position, 0.3f);

    }
#endif
}
