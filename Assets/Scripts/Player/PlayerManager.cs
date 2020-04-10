using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    private int lives = 3;
    public Text livesText;
    public GameObject[] InventoryItems;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Knife"))
        {
            Destroy(collider.gameObject);
            GameManager._Instance.ingredientsCount--;
            livesText.text = (--lives).ToString();
        }else if (collider.CompareTag("Ingredient") && IsInventoryFull(InventoryItems))
        {
            GameManager._Instance.ingredientsCount--;
            AddToInventory(collider.gameObject);
            Destroy(collider.gameObject);
        }
    }

    private bool IsInventoryFull(GameObject[] Inventory)
    {
        foreach (GameObject item in InventoryItems)
        {
            if (item.GetComponent<SpriteRenderer>().sprite == null) return true;
        }

        return false;
    }

    private void AddToInventory(GameObject ingredient)
    {
        foreach (GameObject item in InventoryItems)
        {
            if (item.GetComponent<SpriteRenderer>().sprite == null)
            {
                item.GetComponent<SpriteRenderer>().sprite 
                    = ingredient.GetComponent<SpriteRenderer>().sprite;
                break;
            }
        }
    }
}
