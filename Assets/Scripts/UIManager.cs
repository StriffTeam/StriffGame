using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    [SerializeField] private GameObject gameMenu;

    [SerializeField] private Image[] Img_Healts;

    private Recipe recipe;

    public Text recipeHeader;
    public Text recipeIngredients;

    public static UIManager _Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<UIManager>();
            return instance;
        }
    }

    private void Start()
    {
        var sb = new StringBuilder();
        var randomRecipe = RandomRecipe.GetInstance();
        recipe = randomRecipe.GetRandomRecipe();
        recipeHeader.text = recipe.Name;

        foreach (var ingredient in recipe.Ingredients) sb.Append(ingredient.Number + " x " + ingredient.Name + "\n");

        recipeIngredients.text = sb.ToString();
    }

    public void HealtControl(int healt)
    {
        Img_Healts[healt - 1].fillAmount = 0;
        if (healt == 1) OpenGameMenu();
    }

    private void OpenGameMenu()
    {
        gameMenu.SetActive(true);
        Time.timeScale = 0;
    }
}