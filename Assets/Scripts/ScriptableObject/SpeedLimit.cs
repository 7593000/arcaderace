using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName ="_SpeedLimit", menuName ="Configuration/Data Speed Limit", order =1)]
public class SpeedLimit : ScriptableObject
{
    [SerializeField]
    private DataSpeedLimit[] _data;

    public DataSpeedLimit GetSpeed(float angle)
    {
        int closestIndex = 0;
        float closestDifference = Mathf.Abs(_data[0].angle - angle);

        for (int i = 1; i < _data.Length; i++)
        {
            float difference = Mathf.Abs(_data[i].angle - angle);
            if (difference < closestDifference)
            {
                closestIndex = i;
                closestDifference = difference;
            }
        }
        return _data[closestIndex];
    }
    [Serializable]
    public struct DataSpeedLimit
    {
        public float angle;
        public float speed;
    }

   

}
