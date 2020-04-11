using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    Image[] Img_Healts;
    [SerializeField]
    GameObject gameMenu;

    public Text recipeHeader;
    public Text recipeIngredients;
    
    Recipe recipe;

    private static UIManager instance;

    public static UIManager _Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }
    }
    void Start()
    {
        StringBuilder sb = new StringBuilder();
        RandomRecipe randomRecipe = RandomRecipe.GetInstance();
        recipe = randomRecipe.GetRandomRecipe();
        recipeHeader.text = recipe.Name;

        foreach (var ingredient in recipe.Ingredients)
        {
            sb.Append(ingredient.Number + " x " + ingredient.Name + "\n");
        }

        recipeIngredients.text = sb.ToString();
    }

   
    void Update()
    {
        
    }

    public void HealtControl(int healt)
    {
        Img_Healts[healt - 1].fillAmount = 0;
        if (healt == 1)
        {
            OpenGameMenu();

        }
    }
   void OpenGameMenu()
    {
        gameMenu.SetActive(true);
        Time.timeScale = 0;
    }
}
