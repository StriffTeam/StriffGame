using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;
    public GameObject[] inventoryItems;
    private bool isNearCookingSpot;
    private bool isNearTrash;
    public int lives = 3;
    public Text livesText;

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
        }
        else if (collider.CompareTag("Ingredient"))
        {
            if (!IsInventoryFull(inventoryItems))
            {
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
            if (Input.GetKey(KeyCode.Space))
            {
                //Yemek Pişirilecek   
            }
        }
    }


    private void RemoveItemsFromInventory()
    {
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