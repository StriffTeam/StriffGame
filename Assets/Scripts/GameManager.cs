using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private float dropTime;
    private readonly GameObject[] fallingIngredients = new GameObject[4];

    [SerializeField] private GameObject[] ingredients;

    public int ingredientsCount;

    [SerializeField] private Transform startZoneTransform;

    public static GameManager _Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<GameManager>();
            return instance;
        }
    }

    private void Update()
    {
        GenerateRandomIngredients();
    }

    private void GenerateRandomIngredients()
    {
        if (ingredientsCount <= 5)
            for (var i = 0; i < fallingIngredients.Length; i++)
                if (fallingIngredients[i] == null && Time.time > dropTime)
                {
                    var position = new Vector2(Random.Range(-7.0f, 7.0f), startZoneTransform.position.y);
                    fallingIngredients[i] = Instantiate(ingredients[Random.Range(0, 4)], position, Quaternion.identity);
                    ingredientsCount++;
                    dropTime = Time.time + 0.5f;
                }
    }
}