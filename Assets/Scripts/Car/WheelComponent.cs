using Unity.VisualScripting;
using UnityEngine;

public class WheelComponent : MonoBehaviour
{
    public Rigidbody CarRb;
    public bool bIsSteering;

    public float MaxWheelDistance;
    public float StiffnessValue;
    public float RestLength;
    public float DampingValue;
    public RaycastHit Hit;

    float CurrentLength;
    float PreviousLength;
    float Force;

    public bool bIsGrounded = false;

    void FixedUpdate()
    {
        UpdateSuspension();
        Friction();
    }

    void UpdateSuspension()
    {
        Vector3 Start = transform.position;
        Vector3 End = -transform.up;
        

        if(Physics.Raycast(Start, End, out Hit, MaxWheelDistance))
        {
            
            CurrentLength = Hit.distance;
            float Velocity = (PreviousLength - CurrentLength) / Time.deltaTime;
            Force = StiffnessValue * (RestLength - CurrentLength) + DampingValue * Velocity;

            CarRb.AddForceAtPosition(transform.up * Force, transform.position);

            PreviousLength = CurrentLength;

            bIsGrounded = true;
        }
        
        else
        {
            bIsGrounded = false;
        }

        Debug.DrawRay(Start, End * MaxWheelDistance, Color.red, 0.0f);
    }

    void Friction()
    {
        if(bIsGrounded)
        {
            Vector3 SteeringDir = transform.right;
            Vector3 TireVelocity = CarRb.GetPointVelocity(transform.position);
            float SteeringVel = Vector3.Dot(SteeringDir, TireVelocity);
            float XTraction = 1.0f;
            float Gravity = 9.81f;
            Vector3 XForce = -SteeringDir * SteeringVel * XTraction * ((CarRb.mass * Gravity) / 4.0f);
            Vector3 ForcePosition = transform.position;

            CarRb.AddForceAtPosition(XForce, ForcePosition);
        }
    }

    
}
