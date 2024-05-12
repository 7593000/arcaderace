using UnityEngine;



public class Movement
{
    private bool _isMoving = false;

    private CarComponent _car;

    public Movement(CarComponent car)
    {
        _car = car;

    }


    public void Move()
    {
         

        if (_isMoving == false)
            return;

        float torque = _car.GetInputMove * _car.GetTorgue;

        foreach (CarComponent.Wheel wheel in _car.GetListWheel)
        {
            if (wheel.axed == Axes.Front)
                wheel.wheelCollider.motorTorque = torque;

        }
    }

    public void Steer(float angleTemp)
    {

        foreach (CarComponent.Wheel wheel in _car.GetListWheel)
        {
            float angle = Mathf.Clamp(angleTemp, -_car.GetMaxAngle, _car.GetMaxAngle);

            if (wheel.axed == Axes.Front && _isMoving)
            {
                wheel.wheelCollider.steerAngle = angle;

            }
        }
    }



    public void Breke()
    {
        if (_car.GetBrakeStatus || !_isMoving)
        {

            foreach (var wheel in _car.GetListWheel)
            {
                wheel.wheelCollider.brakeTorque = 300 * _car.GetBrakeAsc * Time.fixedDeltaTime;
                wheel.wheelCollider.motorTorque = 0f;
            }
        }
        else
        {

            foreach (var wheel in _car.GetListWheel)
            {
                wheel.wheelCollider.brakeTorque = 0;
            }
        }
    }


    public void AnimationWheels()
    {
        foreach (CarComponent.Wheel wheel in _car.GetListWheel)
        {

            wheel.wheelCollider.GetWorldPose(out Vector3 pos, out Quaternion rot);
            wheel.wheelMode.transform.position = pos;
            wheel.wheelMode.transform.rotation = rot;
        }
    }

    public void OnStarRace(bool status)
    {
        _isMoving = status;

    }
    public void UpdateInstal()
    {

        Move();
        Breke();
        AnimationWheels();


    }
}
