using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2d;
    public int speed = 10;

    private void Start()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        var move = Input.GetAxis("Horizontal");
        _rigidbody2d.velocity = new Vector2(move * speed, _rigidbody2d.velocity.y);
    }
}