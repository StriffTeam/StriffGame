using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float dropTime;
    private readonly GameObject[] fallingIngredients = new GameObject[4];

    [SerializeField] private GameObject[] ingredients;

    public static int ingredientsCount;

    [SerializeField] private Transform startZoneTransform;

    private void Update()
    {
        GenerateRandomIngredients();
    }

    private void GenerateRandomIngredients()
    {
        if (ingredientsCount <= 20)
            for (var i = 0; i < fallingIngredients.Length; i++)
                if (fallingIngredients[i] == null && Time.time > dropTime)
                {
                    var position = new Vector2(Random.Range(-7.0f, 7.0f), startZoneTransform.position.y);
                    fallingIngredients[i] = Instantiate(ingredients[Random.Range(0, ingredients.Length)], position, Quaternion.identity);
                    ingredientsCount++;
                    dropTime = Time.time + 0.1f;
                }
    }
}