using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;
    public GameObject[] InventoryItems;
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

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Knife"))
        {
            Destroy(collider.gameObject);
            GameManager._Instance.ingredientsCount--;

            UIManager._Instance.HealtControl(lives);
            lives--;
        }
        else if (collider.CompareTag("Ingredient"))
        {
            if (!IsInventoryFull(InventoryItems))
            {
                AddToInventory(collider.gameObject);
                Destroy(collider.gameObject);
                GameManager._Instance.ingredientsCount--;
            }
        }
    }

    private bool IsInventoryFull(GameObject[] Inventory)
    {
        foreach (var item in InventoryItems)
            if (item.GetComponent<SpriteRenderer>().sprite == null)
                return false;

        return true;
    }

    private void AddToInventory(GameObject ingredient)
    {
        foreach (var item in InventoryItems)
            if (item.GetComponent<SpriteRenderer>().sprite == null)
            {
                item.GetComponent<SpriteRenderer>().sprite
                    = ingredient.GetComponent<SpriteRenderer>().sprite;
                break;
            }
    }
}