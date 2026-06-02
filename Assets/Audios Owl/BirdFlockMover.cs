using UnityEngine;

public class BirdFlockMover : MonoBehaviour
{
    public Vector3 direction = new Vector3(1f, 0f, 0f);
    public float speed = 5f;

    void Update()
    {
        transform.position += direction.normalized * speed * Time.deltaTime;
    }
}