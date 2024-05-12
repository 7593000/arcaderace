using UnityEngine;

public class CameraGarage : MonoBehaviour
{
    [SerializeField]
    private Transform _point;
    [SerializeField]
    private float _wheelSpeed = 10;

    [SerializeField]
    private float _minDistance = 5f; // Минимальное расстояние до точки
    [SerializeField]
    private float _maxDistance = 17f; // Максимальное расстояние до точки


    float _scrollWhell = 0;
    private void ScrollCamera()
    {
        Vector3 directionTarget = _point.position - transform.position;

        directionTarget.Normalize();

        Vector3 newPosition = transform.position + directionTarget * _scrollWhell * _wheelSpeed;


        float distanceToTarget = Vector3.Distance(newPosition, _point.position);
        if (distanceToTarget >= _minDistance && distanceToTarget <= _maxDistance)
        {
            transform.position = newPosition;
        }

    }



    // Update is called once per frame
    void Update()
    {
        _scrollWhell = Input.GetAxis("Mouse ScrollWheel");

        if (_scrollWhell != 0.0f)
        {
            ScrollCamera();
        }


    }
}
