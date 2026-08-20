using UnityEngine;

public class PipeController : MonoBehaviour, IScrollable
{
    
    const float maxSpeed = 1.5f;

    float speed = maxSpeed;

    private void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }

    public void StopScrolling()
    {
        speed = 0.0f;
    }

    public void StartScrolling()
    {
        speed = maxSpeed;
    }

    private void OnDestroy()
    {
        GameController.Instance.DeQPipe();
    }
}
