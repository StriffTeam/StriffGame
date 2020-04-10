using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] ingredients;
    GameObject[] fallingIngredients = new GameObject[4];

    [SerializeField]
    Transform startZoneTransform;

    public int ingredientsCount;
    float dropTime;

    private static GameManager instance;

    public static GameManager _Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

    void Update()
    {
        GenerateRandomIngredients();
    }

    void GenerateRandomIngredients()
    {
        if (ingredientsCount <= 5)
        {
            for (int i = 0; i < fallingIngredients.Length; i++)
            {

                if (fallingIngredients[i] == null && Time.time > dropTime)
                {
                    Vector2 position = new Vector2(Random.Range(-7.0f, 7.0f), startZoneTransform.position.y);
                    fallingIngredients[i] = Instantiate(ingredients[Random.Range(0, 4)], position, Quaternion.identity);
                    ingredientsCount++;
                    dropTime = Time.time + 0.5f;
                }

            }
        }   
    
    }
}
