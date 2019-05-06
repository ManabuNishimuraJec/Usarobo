using UnityEngine;

public class skyenemy : MonoBehaviour
{
    [SerializeField]
    private Vector3 speed;
    [SerializeField]
    private GameObject taget;

    [SerializeField]
    private float maxtime;
    private float time;

    private Vector3 pos;


    void Start()
    {

    }

    void Update()
    {
        time += Time.deltaTime;

        pos = taget.transform.position;
        if (time > maxtime)
        {
            transform.position += speed;
            time = 0.0f;
        }
    }
}
