using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float dropTime;
    private float dropTimeKnife;
    private readonly GameObject[] fallingIngredients = new GameObject[4];

    [SerializeField] private GameObject[] ingredients;
    [SerializeField] private GameObject Knife;

    public static int ingredientsCount;
    public static int knifeCount;

    [SerializeField] private Transform startZoneTransform;

    private void Update()
    {
        GenerateRandomIngredients();
        GenerateKnife();
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

    private void GenerateKnife()
    {
        if (knifeCount <= 3)
        {
            if (Time.time > dropTimeKnife)
            {
                var position = new Vector2(Random.Range(-7.0f, 7.0f), startZoneTransform.position.y);
                Instantiate(Knife, position, Quaternion.identity);
                knifeCount++;
                dropTimeKnife = Time.time + 0.8f;
            }
               
            
        }
    }
}