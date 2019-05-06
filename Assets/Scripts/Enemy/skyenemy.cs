using UnityEngine;

public class skyenemy : MonoBehaviour
{
    [SerializeField]
    private Vector3 speed;

    void Start()
    {
        speed.x = 0.0f;
        speed.y = 0.0f;
        speed.z = 0.0f;
    }

    void Update()
    {
        
    }
}
