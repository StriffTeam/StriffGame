﻿using System.Collections.Generic;

public class Recipe
{
    public Recipe(string name, List<Ingredient> ingredients)
    {
        Name = name;
        Ingredients = ingredients;
    }

    public string Name { get; }
    public List<Ingredient> Ingredients { get; }
}