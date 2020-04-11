using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public int lives = 3;
    public Text livesText;
    public GameObject[] InventoryItems;

    private static PlayerManager instance;

    public static PlayerManager _Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerManager>();
            }
            return instance;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Knife"))
        {
            Destroy(collider.gameObject);
            GameManager._Instance.ingredientsCount--;

            {
                GameManager._Instance.ingredientsCount--;
                AddToInventory(collider.gameObject);
                Destroy(collider.gameObject);
            }
        }
    }
        private bool IsInventoryFull(GameObject[] Inventory)
        {
            foreach (GameObject item in InventoryItems)
            {
                if (item.GetComponent<SpriteRenderer>().sprite == null) return false;
            }

            return true;
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
