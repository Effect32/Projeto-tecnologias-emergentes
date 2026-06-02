using UnityEngine;

public class FloatingIcon : MonoBehaviour
{
    [SerializeField] private float amplitude = 0.1f;
    [SerializeField] private float speed = 2f;

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.localPosition;
    }

    private void Update()
    {
        transform.localPosition = startPos + new Vector3(0, Mathf.Sin(Time.time * speed) * amplitude, 0);
    }
}