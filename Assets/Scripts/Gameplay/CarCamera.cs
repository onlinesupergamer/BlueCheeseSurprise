using UnityEngine;

public class CarCamera : MonoBehaviour
{
    public Transform CarTransform;
    public float HeightOffset;
    public float DistanceOffset;

    void Start()
    {
        
    }

    
    void FixedUpdate()
    {
        Vector3 TargetPosition = CarTransform.position + (CarTransform.up * HeightOffset + CarTransform.forward * -DistanceOffset);
        transform.position = Vector3.Lerp(transform.position, TargetPosition, Time.deltaTime * 10.0f);
        

        transform.rotation = Quaternion.Slerp(transform.rotation, CarTransform.rotation, Time.deltaTime * 6.0f);
    }
}
