using UnityEngine;
namespace Race
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField]
        private float _moveSmoothness;
        [SerializeField]
        private float _rotSmoothness;
        [SerializeField]
        private Vector3 _moveOffset;
        [SerializeField]
        private Vector3 _roOffSet;
        [SerializeField]
        private Transform _carTarger;


        private void FixedUpdate()
        {
            FollowTarget();
        }


        private void FollowTarget()
        {
            HandlerMovement();
            HandlerRotation();
        }

        private void HandlerMovement()
        {
            Vector3 targerPos = new();
            targerPos = _carTarger.TransformPoint(_moveOffset);

            transform.position = Vector3.Lerp(transform.position, targerPos, _moveSmoothness * Time.fixedDeltaTime);
        }

        private void HandlerRotation()
        {
            var direction = _carTarger.position - transform.position;

            Quaternion rotation = Quaternion.LookRotation(direction + _roOffSet, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _rotSmoothness * Time.fixedDeltaTime);
        }



    }
}
