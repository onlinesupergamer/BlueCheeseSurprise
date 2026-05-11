using UnityEngine;

public class Car : MonoBehaviour
{
    public Rigidbody CarRb;

    [Header("Engine Settings")]

    public float EngineTorque;
    public float MaxSpeed;
    public AnimationCurve EngineCurve = new AnimationCurve();


    [Header("Wheels")]

    public float MaxWheelAngle;
    public WheelComponent[] Wheels = new WheelComponent[4];
    public AnimationCurve SteeringCurve = new AnimationCurve();


    
    void Start()
    {
        
    }

    void Update()
    {
        //Debug.DrawRay(transform.position, transform.forward * 2, Color.orange, 0.0f);

    }


    void FixedUpdate()
    {
        Accelerate();
        Steer();
    }

    void Accelerate()
    {
        float PlayerInput = Input.GetAxis("Accelerate");
        float SpeedRatio =  GetCurrentSpeed() / MaxSpeed;
        float DriveForce = EngineCurve.Evaluate(Mathf.Abs(SpeedRatio)) * EngineTorque * PlayerInput;

        for(int i = 0; i < Wheels.Length; i++)
        {
            if(Wheels[i].bIsGrounded)
            {
                CarRb.AddForceAtPosition(Wheels[i].transform.forward * DriveForce, Wheels[i].Hit.point);
            }
        }
    }

    void Steer()
    {
        float PlayerInput = Input.GetAxis("Steer");
        float SpeedRatio = GetCurrentSpeed() / MaxSpeed;
        float SteeringAngle = SteeringCurve.Evaluate(SpeedRatio) * MaxWheelAngle;
        for(int i = 0; i < Wheels.Length; i++)
        {
            if(Wheels[i].bIsSteering)
            {
                float WheelAngle = SteeringAngle * PlayerInput;
                Vector3 NewAngle = new Vector3(0, WheelAngle, 0);

                Wheels[i].transform.localRotation = Quaternion.Euler(NewAngle);
            }
        }
    }

    public float GetCurrentSpeed()
    {
        return Vector3.Dot(transform.forward, CarRb.linearVelocity);
    }
}
