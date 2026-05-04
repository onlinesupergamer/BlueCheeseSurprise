using UnityEngine;

public class CarCamera : MonoBehaviour
{
    public Transform m_Car;

    public float HeightOffset;
    public float DistanceOffset;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 TargetPosition = m_Car.transform.position + (m_Car.transform.up * HeightOffset + m_Car.transform.forward * -DistanceOffset);
        transform.position = Vector3.Lerp(transform.position, TargetPosition, Time.deltaTime * 20.0f);
        
        transform.rotation = Quaternion.Slerp(transform.rotation, m_Car.transform.rotation, Time.deltaTime * 6.0f);
    }
}
