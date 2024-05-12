using UnityEditor;
using UnityEngine;
//todo->вынести из проекта
public class TargetPoint : MonoBehaviour
{
    [SerializeField, Range(0.0f, 2.0f)] 
   private float _radius = 0.4f;

    public Transform GetTransform() { return transform; }

#if UNITY_EDITOR
    public float AnglePoint;

#endif
    void OnDrawGizmos()
    {
        Handles.Label(transform.position + Vector3.up * 5f, AnglePoint.ToString());
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, _radius);
    }
}
