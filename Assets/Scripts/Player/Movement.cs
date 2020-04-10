using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2d;
    private bool isNearCookingSpot;
    private bool isNearTrash;
    public int speed = 10;

    private void Start()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void Update()
    {
        Action();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("CookingSpot"))
            isNearCookingSpot = true;
        else if (collider.CompareTag("Trash")) isNearTrash = true;
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("CookingSpot") || collider.CompareTag("Trash"))
        {
            isNearTrash = false;
            isNearCookingSpot = false;
        }
    }

    private void Action()
    {
        if (isNearTrash)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Debug.Log("Çöpe Atılıyor...");
        }
        else if (isNearCookingSpot)
        {
            if (Input.GetKey(KeyCode.Space))
                Debug.Log("Pişiriliyor...");
        }
    }

    private void MovePlayer()
    {
        var move = Input.GetAxis("Horizontal");
        _rigidbody2d.velocity = new Vector2(move * speed, _rigidbody2d.velocity.y);
    }
}