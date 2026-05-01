using UnityEngine;

public class Car : MonoBehaviour
{
    
    public Rigidbody CarRb;
    public float EngineTorque = 100.0f;




    void FixedUpdate()
    {
        Drive();
    }


    void Drive()
    {
        float PlayerInput = Input.GetAxisRaw("Accelerate");
        
        Vector3 DriveForce = (CarRb.transform.forward * EngineTorque) * PlayerInput;
        CarRb.AddForce(DriveForce, ForceMode.Acceleration);

    }
}
