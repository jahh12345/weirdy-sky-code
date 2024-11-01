using UnityEngine;

public class WitchController : MonoBehaviour
{
    public float moveSpeed = 5f; 
    public BoxCollider2D boundaryCollider; 
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("HorizontalWitch");
        float moveY = Input.GetAxis("VerticalWitch");

        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;
        Vector2 newPosition = rb.position + moveDirection * moveSpeed * Time.deltaTime;

        if (IsWithinBoundary(newPosition))
        {
            rb.MovePosition(newPosition);
        }
    }

    bool IsWithinBoundary(Vector2 position)
    {
        Bounds boundaryBounds = boundaryCollider.bounds;
        return boundaryBounds.Contains(position);
    }
}