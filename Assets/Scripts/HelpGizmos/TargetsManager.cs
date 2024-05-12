using System;
using System.Collections.Generic;
using UnityEngine;

public class TargetsManager : MonoBehaviour
{
    [SerializeField]
    private BrakePoint _prefBrake;

    [SerializeField]
    private SpeedLimit _speedLimit;

    [SerializeField]
    private List<TargetPoint> _target = new();
    [SerializeField]
    private List<BrakePoint> _brakePoints = new();
    private const float c_convertMeterInSecFromKmInH = 3.6f;

#if UNITY_EDITOR
    public string _nameGameObjectPoints = "TargetPoint";

#endif



    public IReadOnlyList<TargetPoint> GetTargets => _target;

    private void OnValidate()
    {




        for (int i = 0; i < _target.Count; i++)
        {
            var nextIndex = i + 1;
            if (nextIndex < _target.Count)
            {
                _target[i].transform.LookAt(_target[nextIndex].transform);

            }

        }



    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < _target.Count; i++)
        {
            var nextIndex = i + 1;
            if (nextIndex < _target.Count)
            {
                var nextTarget = _target[nextIndex];
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(_target[i].transform.position, nextTarget.transform.position);
            }
        }
    }
    private void Start()
    {
#if UNITY_EDITOR
        if (_target.Count <= 0) Debug.Log("Не добавлены ключевые точки для пути");
#endif
    }



    [ContextMenu("Rename points")]
    private void RenameFiles()
    {
        int index = 0;
        foreach (var target in _target)
        {
            target.gameObject.name = $"{_nameGameObjectPoints}-{index}";

            index++;

        }
    }
    [ContextMenu("Remove all brakes Points")]
    private void RemoveALlPoints()
    {
        if (_brakePoints.Count > 0)
        {
            foreach (var brake in _brakePoints)
            {
                DestroyImmediate(brake.gameObject);
               
            }
        }
        _brakePoints.Clear();

    }

    [ContextMenu("Install the brakes")]
    private void InstallBrakes()
    {
        CheckingAngle();


    }

    /// <summary>
    /// Получить угол между точками
    /// </summary>

    private void CheckingAngle()
    {
        _brakePoints.Clear();

        for (int i = 0; i < _target.Count - 2; i++)
        {
            Vector3 VectorAB = (_target[i].transform.position - _target[i + 1].transform.position).normalized;
            Vector3 VectorBC = (_target[i + 1].transform.position - _target[i + 2].transform.position).normalized;

            float dotProduct = Vector3.Dot(VectorAB, VectorBC);
            float angle = Mathf.Acos(dotProduct) * Mathf.Rad2Deg;
#if UNITY_EDITOR 
            _target[i + 1].AnglePoint = angle;
#endif
            if (_target[i + 1] == null) return;
            GetLengthDistance(_target[i], _target[i + 1], _speedLimit.GetSpeed(angle).speed);
        }

    }
    /// <summary>
    /// Определить длину отрезка у становкой ограничителя скорости 
    /// </summary>
    private void GetLengthDistance(TargetPoint a, TargetPoint b, float speedLimit)
    {

        Vector3 quarterPoint = Vector3.Lerp(a.transform.position, b.transform.position, 0.75f);


        BrakePoint brakePoint = Instantiate(_prefBrake, quarterPoint, Quaternion.identity, transform);

        brakePoint.SpeedLimit = (float)Math.Round(speedLimit / c_convertMeterInSecFromKmInH);

        brakePoint.name = $"Sdeed Limit = {speedLimit}";
        _brakePoints.Add(brakePoint);
    }



}











