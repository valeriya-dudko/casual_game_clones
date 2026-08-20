using UnityEngine;

public class GroundScroller : MonoBehaviour, IScrollable
{
    float speed = 0f;
    const float maxSpeed = 0.78f;
    Material material;
    Vector2 offset;

    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        offset.x += speed * Time.deltaTime;
        material.mainTextureOffset = offset;
    }

    public void StopScrolling()
    {
        speed = 0.0f;
    }

    public void StartScrolling()
    {
        speed = maxSpeed;
    }
}
