using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;
    public GameObject[] inventoryItems;
    private bool isNearCookingSpot;
    private bool isNearTrash;
    public int lives = 3;
    public AudioSource painAudio;
    public AudioSource dumpAudio;
    public AudioSource collectAudio;
    public AudioSource cookAudio;

    public static PlayerManager _Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<PlayerManager>();
            return instance;
        }
    }

    private void Update()
    {
        Action();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Knife"))
        {
            Destroy(collider.gameObject);
            GameManager.ingredientsCount--;

            UIManager._Instance.HealtControl(lives);
            lives--;
            
            painAudio.Play();
        }
        else if (collider.CompareTag("Ingredient"))
        {
            if (!IsInventoryFull(inventoryItems))
            {
                collectAudio.Play();
                AddToInventory(collider.gameObject);
                Destroy(collider.gameObject);
                GameManager.ingredientsCount--;
            }
        }

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
            if (Input.GetKeyDown(KeyCode.Space)) RemoveItemsFromInventory();
        }
        else if (isNearCookingSpot)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (IsInventoryCorrect(inventoryItems))
                {
                    cookAudio.Play();
                    UIManager._Instance.score += 10;
                    foreach (GameObject inventoryItem in inventoryItems)
                    {
                        inventoryItem.GetComponent<SpriteRenderer>().sprite = null;
                    }


                    UIManager._Instance.NewRecipe();
                }
            }
        }
    }


    private void RemoveItemsFromInventory()
    {
        dumpAudio.Play();
        var rnd = RandomRecipe.GetInstance();

        foreach (var item in inventoryItems)
            if (item.GetComponent<SpriteRenderer>().sprite != null)
            {
                var itemSpriteName = item.GetComponent<SpriteRenderer>().sprite.name;
                if (!IsIngredientsContains(rnd.randomRecipe, itemSpriteName))
                {
                    item.GetComponent<SpriteRenderer>().sprite = null;
                }
                else
                {
                    var number = NumberOfIngredient(rnd.randomRecipe, itemSpriteName);
                    var temp = 0;
                    foreach (var item2 in inventoryItems)
                        if (item2.GetComponent<SpriteRenderer>().sprite != null)
                            if (item2.GetComponent<SpriteRenderer>().sprite.name == itemSpriteName)
                            {
                                temp++;
                                if (temp > number) item2.GetComponent<SpriteRenderer>().sprite = null;
                            }
                }
            }
    }

    private int NumberOfIngredient(Recipe recipe, string name)
    {
        var number = 0;
        foreach (var item in recipe.Ingredients)
            if (item.Name == name)
                number += item.Number;

        return number;
    }

    private bool IsIngredientsContains(Recipe recipe, string name)
    {
        foreach (var item in recipe.Ingredients)
            if (item.Name == name)
                return true;

        return false;
    }

    private bool IsInventoryFull(GameObject[] inventory)
    {
        foreach (var item in inventory)
            if (item.GetComponent<SpriteRenderer>().sprite == null)
                return false;

        return true;
    }

    private bool IsInventoryCorrect(GameObject[] inventory)
    {
        if (!IsInventoryFull(inventory)) return false;

        var rnd = RandomRecipe.GetInstance();
        Dictionary<string, int> inventoryIngreds = new Dictionary<string, int>();

        foreach (var item in inventory)
        {
            if (!IsIngredientsContains(rnd.randomRecipe, item.GetComponent<SpriteRenderer>().sprite.name)) return false;
            
            if (inventoryIngreds.ContainsKey(item.GetComponent<SpriteRenderer>().sprite.name))
            {
                inventoryIngreds[item.GetComponent<SpriteRenderer>().sprite.name] += 1;
            }
            else
            {
                inventoryIngreds.Add(item.GetComponent<SpriteRenderer>().sprite.name, 1);
            }
        }

        foreach (var rndItem in rnd.randomRecipe.Ingredients)
        {
            if (inventoryIngreds[rndItem.Name] != rndItem.Number) return false;
        }

        return true;
    }
    
    private void AddToInventory(GameObject ingredient)
    {
        foreach (var item in inventoryItems)
            if (item.GetComponent<SpriteRenderer>().sprite == null)
            {
                item.GetComponent<SpriteRenderer>().sprite
                    = ingredient.GetComponent<SpriteRenderer>().sprite;
                break;
            }
    }
}