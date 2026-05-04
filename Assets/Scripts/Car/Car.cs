using UnityEngine;

public class Car : MonoBehaviour
{
    
    public Rigidbody CarRb;
    public float GravityValue;

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
        Gravity();
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
    
    void Gravity()
    {
        Vector3 GravityForce = new Vector3(0, 0, 0);
        if(Wheels[0].bIsGrounded || Wheels[1].bIsGrounded ||Wheels[2].bIsGrounded ||Wheels[3].bIsGrounded)
        {
            GravityForce = -transform.up * GravityValue;
        }

        else
        {
            GravityForce = -Vector3.up * GravityValue;
            
        }

        CarRb.AddForce(GravityForce * Time.deltaTime);
    }

    public float GetCurrentSpeed()
    {
        return Vector3.Dot(CarRb.linearVelocity, CarRb.transform.forward);
    }
}
