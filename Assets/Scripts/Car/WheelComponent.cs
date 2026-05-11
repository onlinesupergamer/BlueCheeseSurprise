using Unity.VisualScripting;
using UnityEngine;

public class WheelComponent : MonoBehaviour
{

    public Rigidbody CarRb;
    public float MaxDistance;
    public float RestDistance;
    public float DamperValue;
    public float Stiffness;

    float CurrentLength;
    float PreviousLength;

    public bool bIsGrounded;
    public bool bIsSteering;

    public RaycastHit Hit;
    Vector3 TotalForce;




    void Start()
    {
        
    }

    
    void FixedUpdate()
    {
        UpdateSuspension();
        Friction();
    }

    void UpdateSuspension()
    {
        Vector3 Start = transform.position;
        Vector3 End = -transform.up;

        if(Physics.Raycast(Start, End, out Hit, MaxDistance))
        {
            CurrentLength = Hit.distance;
            float Velocity = (PreviousLength - CurrentLength) / Time.deltaTime;
            TotalForce.y = Stiffness * (RestDistance - CurrentLength) + DamperValue * Velocity;
            CarRb.AddForceAtPosition(TotalForce.y * Hit.normal, Hit.point);
            PreviousLength = CurrentLength;

            bIsGrounded = true;

        }

        else
        {
            bIsGrounded = false;
        }

        Debug.DrawRay(Start, End * MaxDistance, Color.red, 0.0f);
    }

    void Friction()
    {
        if(bIsGrounded)
        {
            Vector3 SteeringDir = transform.right;
            Vector3 TireVelocity = CarRb.GetPointVelocity(transform.position);
            float SteeringVelocity = Vector3.Dot(SteeringDir, TireVelocity);
            float XTraction = 0.5f;
            float Gravity = 9.81f;
            Vector3 XForce = -SteeringDir * SteeringVelocity * XTraction * ((CarRb.mass * Gravity) / 4.0f);
            Vector3 ForcePosition = transform.position;

            CarRb.AddForceAtPosition(XForce, ForcePosition);
        }

    }
}
