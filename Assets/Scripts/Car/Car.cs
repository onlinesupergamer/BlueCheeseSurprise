using UnityEngine;

public class Car : MonoBehaviour
{
    
    public Rigidbody CarRb;

    [Header("Wheel Settings")]
    public WheelComponent[] Wheels = new WheelComponent[4];
    public float MaxSteeringAngle;


    [Header("Engine Settings")]
    public AnimationCurve EngineCurve;
    public AnimationCurve SteeringCurve;
    public float EngineTorque = 100.0f;
    public float MaxSpeed;



    void FixedUpdate()
    {
        Drive();
        Steer();
    }


    void Drive()
    {
        float PlayerInput = Input.GetAxis("Accelerate");
        float SpeedRatio = GetCurrentSpeed() / MaxSpeed;
        float TotalTorque = EngineCurve.Evaluate(SpeedRatio) * EngineTorque;

        for(int i = 0; i < Wheels.Length; i++)
        {
            if(Wheels[i].bIsGrounded)
            {
                Vector3 Force = Wheels[i].transform.forward;
                Vector3 ProjectedForce = Vector3.ProjectOnPlane(Force, Wheels[i].Hit.normal) * (TotalTorque * PlayerInput);
                CarRb.AddForceAtPosition(ProjectedForce, Wheels[i].Hit.point);
            }
        }

    }

    void Steer()
    {
        float PlayerInput = Input.GetAxisRaw("Steer");
        float SpeedRatio = GetCurrentSpeed() / MaxSpeed;
        float TotalSteerAngle = SteeringCurve.Evaluate(SpeedRatio) * MaxSteeringAngle;
        

        for(int i = 0; i < Wheels.Length; i++)
        {
            if(Wheels[i].bIsSteering)
            {
                Vector3 NewRot = new Vector3(0, TotalSteerAngle * PlayerInput, 0);
                Wheels[i].transform.localRotation = Quaternion.Euler(NewRot);
            }
        }
    }
    

    public float GetCurrentSpeed()
    {
        return Vector3.Dot(CarRb.linearVelocity, CarRb.transform.forward);
    }
}
