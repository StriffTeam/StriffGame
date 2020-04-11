using System.Collections.Generic;
using System.IO;
using UnityEngine;

//Singleton Class
public class RandomRecipe
{
    private const string DIRECTORY_PATH = "./Assets/Recipes";

    private static RandomRecipe _instance;
    private readonly DirectoryInfo dInfo;
    private readonly FileInfo[] files;
  

    public int numberOfIngredients { get; set; }
    public Recipe randomRecipe { get; set; }
    
    private RandomRecipe()
    {
        dInfo = new DirectoryInfo(DIRECTORY_PATH);
        files = dInfo.GetFiles("*.txt");
    }

    public static RandomRecipe GetInstance()
    {
        if (_instance == null) _instance = new RandomRecipe();

        return _instance;
    }

    public void GenerateRandomRecipe()
    {
        var random = Random.Range(0, files.Length);
        string name;
        var lines = File.ReadAllLines($"{DIRECTORY_PATH}/{files[random].Name}");
        name = lines[0];

        var ingredients = new List<Ingredient>();

        for (var i = 1; i < lines.Length; i++)
        {

            var ingredient = lines[i].Split(' ');
            ingredients.Add(new Ingredient(ingredient[1], int.Parse(ingredient[0])));
            numberOfIngredients += int.Parse(ingredient[0]);
        }
        
        randomRecipe = new Recipe(name, ingredients);
    }
}