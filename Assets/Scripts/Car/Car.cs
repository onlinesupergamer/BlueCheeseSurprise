using UnityEngine;

public class Car : MonoBehaviour
{
    public Rigidbody CarRb;

    PlayerInputActions m_PlayerInputActions;

    [Header("Engine Settings")]

    public float EngineTorque;
    public float MaxSpeed;
    public AnimationCurve EngineCurve = new AnimationCurve();

    

    [Header("Wheels")]

    public float MaxWheelAngle;
    public WheelComponent[] Wheels = new WheelComponent[4];
    public AnimationCurve SteeringCurve = new AnimationCurve();



    void OnEnable()
    {
        m_PlayerInputActions.Enable();        
    }

    void OnDisable()
    {
        m_PlayerInputActions.Disable();
    }

    void Awake()
    {
        m_PlayerInputActions = new PlayerInputActions();
    }

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
        float PlayerInput = m_PlayerInputActions.Drive.Accelerate.ReadValue<float>();
        float SpeedRatio = GetCurrentSpeed() / MaxSpeed;
        float DriveForce = EngineCurve.Evaluate(Mathf.Abs(SpeedRatio)) * EngineTorque * PlayerInput;

        for(int i = 0; i < Wheels.Length; i++)
        {
            if(Wheels[i].bIsGrounded)
            {
                CarRb.AddForceAtPosition(Wheels[i].transform.forward * DriveForce * Time.deltaTime, Wheels[i].Hit.point);
            }
        }
    }

    void Steer()
    {
        float PlayerInput = m_PlayerInputActions.Drive.Steer.ReadValue<float>();
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
