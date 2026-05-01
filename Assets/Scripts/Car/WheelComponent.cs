using Unity.VisualScripting;
using UnityEngine;

public class WheelComponent : MonoBehaviour
{
    public Rigidbody CarRb;

    public float MaxWheelDistance;
    public float StiffnessValue;
    public float RestLength;
    public float DampingValue;
    public RaycastHit Hit;

    float CurrentLength;
    float PreviousLength;
    float Force;


    void FixedUpdate()
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


        }

        Debug.DrawRay(Start, End * MaxWheelDistance, Color.red, 0.0f);
    }
}
