using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsScripts : MonoBehaviour
{
    Rigidbody2D rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        if (this.CompareTag("Knife"))
            rigid.velocity = new Vector2(0, -5);
        else
            rigid.velocity = new Vector2(0,Random.Range(-4,-2));
    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DestroyZone"))
        {
            Destroy(this.gameObject);
            GameManager._Instance.ingredientsCount--;
            //Puan Dusur
        }
        if (collision.CompareTag("Player"))
        {
            if (this.CompareTag("Knife"))
            {
               //Skordusur
            }
            else
            {
                
                //inventory ekle
            }
            GameManager._Instance.ingredientsCount--;
            Destroy(this.gameObject);

        }
       
    }
}
