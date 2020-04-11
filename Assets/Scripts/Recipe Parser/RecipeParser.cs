using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RecipeParser
{
    private const string DIRECTORY_PATH = "./Assets/Recipes";
    private readonly DirectoryInfo dInfo;
    private readonly FileInfo[] files;

    public RecipeParser()
    {
        dInfo = new DirectoryInfo(DIRECTORY_PATH);
        files = dInfo.GetFiles("*.txt");
    }

    public Recipe GetRandomRecipe()
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
        }

        return new Recipe(name, ingredients);
    }
}