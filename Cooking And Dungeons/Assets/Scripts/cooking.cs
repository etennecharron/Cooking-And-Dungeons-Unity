
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using static recipeReader;

public class cooking : MonoBehaviour
{


    //filled using the script inventory
    public System.Collections.Generic.List<GameObject> tilesArr;
    public recipeReader recipeReaderScript;
    public List<GameObject> tilesFilled;


    public void cook()
    {
        bool cookedGood = false;

        tilesFilled.Clear();

        foreach(GameObject tileFilled in tilesArr)
        {
            if (tileFilled.GetComponent<tileContent>().itemInTile == true)
            {
                tilesFilled.Add(tileFilled);
            }
        }

        void removeItems()
        {
            foreach (GameObject tiles in tilesFilled)
            {
                if (tiles.GetComponent<itemIdentity>().nb == 1)
                {
                    tiles.transform.GetChild(0).gameObject.transform.GetComponent<UnityEngine.UI.Image>().sprite = null;
                    tiles.transform.GetChild(1).gameObject.transform.GetComponent<TextMeshProUGUI>().text = null;

                    tiles.GetComponent<itemIdentity>().itemName = null;
                    tiles.GetComponent<itemIdentity>().description = null;
                    tiles.GetComponent<itemIdentity>().maxInventory = 0;
                    tiles.GetComponent<itemIdentity>().img = null;
                    tiles.GetComponent<itemIdentity>().nb = 0;

                    tiles.GetComponent<tileContent>().itemInTile = false;
                }
                else
                {
                    tiles.GetComponent<itemIdentity>().nb = tiles.GetComponent<itemIdentity>().nb - 1;

                    tiles.transform.GetChild(1).gameObject.transform.GetComponent<TextMeshProUGUI>().text = tiles.GetComponent<itemIdentity>().nb.ToString();
                }

            }
        }
        List<Recipe> recipeList = recipeReaderScript.RecipeList;

        foreach (Recipe recipe in recipeList)
        {
            int goodIngredient = 0;
            foreach(string ingredientNeeded in recipe.ingredients)
            {  
                if (recipe.ingredients.Count == tilesFilled.Count)
                {
                    foreach (GameObject ingredientInTiles in tilesArr)
                    {
                        if (ingredientInTiles.GetComponent<itemIdentity>().itemName == ingredientNeeded)
                        {
                            goodIngredient++;
                        }

                    }

                }          
            }
            if (goodIngredient == recipe.ingredients.Count)
            {
                Debug.Log(recipe.name + " cooked");
                cookedGood = true;
                removeItems();


            }
        }

        if (cookedGood == false)
        {
            Debug.Log("trash");
            removeItems();
        }

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
 