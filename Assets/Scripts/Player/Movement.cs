using Unity.Mathematics;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2d;
    private bool isFacingLeft;
    private bool isFacingRight = true;
    public int speed = 10;
    [SerializeField]
    Sprite[] sprites;

    [SerializeField]
    Animator anim;

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
        if (move == -1)
        {
            this.GetComponent<SpriteRenderer>().sprite = sprites[1];
            anim.SetInteger("move", -1);
        }
        else if (move == 1)
        {
            this.GetComponent<SpriteRenderer>().sprite = sprites[0];
            anim.SetInteger("move", 1);
        }
        else
        {
            anim.SetInteger("move", 0);
        }
        if (_rigidbody2d.velocity.x < 0 && !isFacingLeft)
        {
            isFacingLeft = true;
            isFacingRight = false;
            transform.Rotate(0, 180, 0);
        }
        else if (_rigidbody2d.velocity.x > 0 && !isFacingRight)
        {
            isFacingRight = true;
            isFacingLeft = false;
            transform.rotation = quaternion.Euler(0, 0, 0);
        }
    }
}