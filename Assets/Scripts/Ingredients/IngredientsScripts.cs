using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class IngredientsScripts : MonoBehaviour
{
    private Rigidbody2D rigid;
    private int rotationSpeed;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rotationSpeed = Random.Range(1, 4);
        if (CompareTag("Knife"))
            rigid.velocity = new Vector2(0, -5);
        else
        {
            rigid.velocity = new Vector2(0, Random.Range(-4, -1));
            transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 180));
        }
    }

    private void FixedUpdate()
    {
        if(CompareTag("Ingredient")) transform.Rotate(0,0, rotationSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DestroyZone"))
        {
            Destroy(gameObject);
            
            GameManager.ingredientsCount--;
        }
    }
}