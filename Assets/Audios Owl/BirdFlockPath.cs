using UnityEngine;

public class BirdFlockPath : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float speed = 5f;
    public bool loop = true;

    void Start()
    {
        if (startPoint != null)
        {
            transform.position = startPoint.position;
        }
    }

    void Update()
    {
        if (endPoint == null) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            endPoint.position,
            speed * Time.deltaTime
        );

        Vector3 direction = endPoint.position - transform.position;

        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }

        if (Vector3.Distance(transform.position, endPoint.position) < 0.2f)
        {
            if (loop && startPoint != null)
            {
                transform.position = startPoint.position;
            }
        }
    }
}