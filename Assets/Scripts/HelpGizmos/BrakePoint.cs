using UnityEditor;
using UnityEngine;
//todo->������� �� �������
public class BrakePoint : MonoBehaviour
{
    [SerializeField, Range(0.0f, 2.0f)] 
    private float _radius = 0.8f;

    /// <summary>
    /// �������� ������������ �������� ��� �������� ( ������ � !!::magnitude:: )
    /// </summary>
    public float SpeedLimit;

#if UNITY_EDITOR

    void OnDrawGizmos()
    {
       
        Gizmos.color = Color.red;
        Handles.Label(transform.position + Vector3.up *5f, (SpeedLimit * 3.6f).ToString());
        Gizmos.DrawSphere(transform.position, _radius);
    }
#endif
}
