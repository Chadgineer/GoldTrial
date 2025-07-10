using UnityEngine;

public class Simple2DVelocity : MonoBehaviour
{
    private enum Direction { Left = -1, Right = 1 }

    [SerializeField] private Direction direction = Direction.Right;
    [SerializeField] private float speed = 5f;
    [SerializeField] private Rigidbody2D rb;

    private void Reset()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (rb == null) return;
        rb.linearVelocity = new Vector2((int)direction * speed, rb.linearVelocity.y);
    }
}