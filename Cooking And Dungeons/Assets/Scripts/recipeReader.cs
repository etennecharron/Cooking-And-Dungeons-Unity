using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;

public class recipeReader : MonoBehaviour
{
    public TextAsset recipesJSON;



    // Must be the same as the JSON
    [System.Serializable]
    public class Recipe
    {
        public string name;
        public string[] ingredients;
        public int lvl;
    }


    //Must be the same as the JSON
    [System.Serializable]
    public class RecipeList
    {
        public Recipe[] recipes;
    }

    public RecipeList myRecipeList = new RecipeList();
  

    void Start()
    {
        myRecipeList = JsonUtility.FromJson<RecipeList>(recipesJSON.text);
    }
  
    
    void Update()
    {
        
    }
}
