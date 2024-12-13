
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static recipeReader;

public class cooking : MonoBehaviour
{


    //filled using the script inventory
    public System.Collections.Generic.List<GameObject> tilesArr;
    public recipeReader recipeReaderScript;
    public void cook()
    {

        List<Recipe> recipeList = recipeReaderScript.RecipeList;

        foreach (Recipe recipe in recipeList)
        {
            Debug.Log(recipe.name);
            foreach(string ingredientNeeded in recipe.ingredients)
            {
                int goodIngredient = 0;
                foreach(GameObject ingredientInTiles in tilesArr)
                {
                    if(ingredientInTiles.GetComponent<itemIdentity>().itemName == ingredientNeeded)
                    {
                        goodIngredient++;
                    }
                }
                if(goodIngredient == recipe.ingredients.Count)
                {
                    Debug.Log(recipe.name + "cuisiner");
                }
            }
        }

        Debug.Log(recipeList.Count);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //cook();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
