using System.Collections.Generic;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;

public class recipeReader : MonoBehaviour
{

    [System.Serializable]
    public class Recipe
    {
        public string name;
        public List<string> ingredients;
        public int lvl;

    }

    public List<Recipe> RecipeList = new();


    void Start()
    {
    }
  
    
    void Update()
    {
        
    }
}
