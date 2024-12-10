using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class tileContent : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    public GameObject tile;
    public Image imageItem;
    public TextMeshProUGUI nbItem;
    public itemIdentity scriptItem;
    public GameObject cookingParent;
    public List<GameObject> cookingArr;
    public int nbOfItemsToSend = 1;
    public bool itemInTile;
    void Start()
    {
        Button btn = tile.GetComponent<Button>();
        btn.onClick.AddListener(onClick);

        cookingParent = GameObject.Find("ButtonCooking");
        cookingArr = cookingParent.GetComponent<cooking>().tilesArr;
    }
    void onClick()
    {

        moveItem(cookingArr);

    }
    // Update is called once per frame
    void Update()
    {
        
    }



    public void moveItem(List<GameObject> arrDestination)
    {
        if (itemInTile == true)
        {
            List<GameObject> tilesArr = arrDestination; //cookingParent.GetComponent<cooking>().tilesArr; 
            bool emptyFound = false; // good 
            bool itemStored = false; // good
            for (int i = 0; i < tilesArr.Count ; i++) // good
            {
                if (tilesArr[i].GetComponent<itemIdentity>().itemName == tile.GetComponent<itemIdentity>().itemName && itemStored == false)//good
                {
                    //removes a quantity of item from the inventory
                    scriptItem.nb = scriptItem.nb - nbOfItemsToSend; //good
                    tile.transform.GetChild(1).gameObject.transform.GetComponent<TextMeshProUGUI>().text = scriptItem.nb.ToString();//good
                    //adds a quantity of item to the cooking
                    tilesArr[i].GetComponent<itemIdentity>().nb++; //good
                    tilesArr[i].transform.GetChild(1).gameObject.transform.GetComponent<TextMeshProUGUI>().text = tilesArr[i].GetComponent<itemIdentity>().nb.ToString(); // good

                    if (scriptItem.nb <= 0)// good
                    {
                        imageItem.GetComponent<UnityEngine.UI.Image>().sprite = null;//good
                        nbItem.text = "";//good
                        scriptItem.itemName = null;//good
                        scriptItem.description = null;//good
                        scriptItem.maxInventory = 0;//good
                        scriptItem.img = null;//good
                        itemInTile = false;
                    }
                    itemStored = true;//good
                }
                else if (tilesArr[i].GetComponent<itemIdentity>().itemName == "" && emptyFound == false && itemStored == false)//good
                {
                    // new item in cooking tilesArr
                    emptyFound = true;//good
                    // tile in cooking receiving the item's data
                    itemIdentity itemInNewTile = tilesArr[i].GetComponent<itemIdentity>(); //good
                    itemInNewTile.itemName = scriptItem.itemName; //good
                    itemInNewTile.description = scriptItem.description; //good
                    itemInNewTile.maxInventory = scriptItem.maxInventory; //good
                    itemInNewTile.img = scriptItem.img;//good

                    //quantity to send
                    itemInNewTile.nb = nbOfItemsToSend; //good
                    scriptItem.nb = scriptItem.nb - nbOfItemsToSend; //good
                    tile.transform.GetChild(1).gameObject.transform.GetComponent<TextMeshProUGUI>().text = scriptItem.nb.ToString(); //good

                    tilesArr[i].transform.GetChild(0).gameObject.transform.GetComponent<UnityEngine.UI.Image>().sprite = itemInNewTile.img; //good
                    tilesArr[i].transform.GetChild(1).gameObject.transform.GetComponent<TextMeshProUGUI>().text = itemInNewTile.nb.ToString(); //good

                    tilesArr[i].GetComponent<tileContent>().itemInTile = true;
                    //remove info on the last item
                    if (scriptItem.nb <= 0) //good
                    {
                        imageItem.GetComponent<UnityEngine.UI.Image>().sprite = null; //good
                        nbItem.text = ""; //good
                        scriptItem.itemName = null; //good
                        scriptItem.description = null; //good
                        scriptItem.maxInventory = 0; //good
                        scriptItem.img = null; //good
                        itemInTile = false;
                    }

                    itemStored = true; //good
                }
            }
        }
    }


}


