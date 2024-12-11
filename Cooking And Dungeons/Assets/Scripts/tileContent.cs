using System.Collections.Generic;
using TMPro;
using UnityEditor;
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
    public GameObject inventoryParent;
    public List<GameObject> inventoryArr;
    
    public int nbOfItemsToSend = 1;
    public bool itemInTile;
    void Start()
    {
        Button btn = tile.GetComponent<Button>();
        btn.onClick.AddListener(onClick);

        cookingParent = GameObject.Find("ButtonCooking");
        cookingArr = cookingParent.GetComponent<cooking>().tilesArr;

        inventoryParent = GameObject.Find("player");
        inventoryArr = inventoryParent.GetComponent<inventory>().tilesInventoryArr;
       
    }
    void onClick()
    {
        switch (tile.transform.parent.name){
            case "gridInventory":
                moveItem(cookingArr);
                break;
            case "gridCooking":
                moveItem(inventoryArr);
                break;
            default:
                Debug.Log("not working");
                Debug.Log(tile.transform.parent.name);
                break;
        }
        
      
    }
    // Update is called once per frame
    void Update()
    {
        
    }



    public void moveItem(List<GameObject> arrDestination)
    {
        if (itemInTile == true)
        {
            List<GameObject> tilesArr = arrDestination;
            bool emptyFound = false;
            bool itemStored = false; 
            for (int i = 0; i < tilesArr.Count ; i++) 
            {
                if (tilesArr[i].GetComponent<itemIdentity>().itemName == tile.GetComponent<itemIdentity>().itemName && itemStored == false)
                {
                    //removes a quantity of item from the inventory
                    scriptItem.nb = scriptItem.nb - nbOfItemsToSend; 
                    tile.transform.GetChild(1).gameObject.transform.GetComponent<TextMeshProUGUI>().text = scriptItem.nb.ToString();
                    //adds a quantity of item to the cooking
                    tilesArr[i].GetComponent<itemIdentity>().nb++; 
                    tilesArr[i].transform.GetChild(1).gameObject.transform.GetComponent<TextMeshProUGUI>().text = tilesArr[i].GetComponent<itemIdentity>().nb.ToString(); 

                    if (scriptItem.nb <= 0)
                    {
                        imageItem.GetComponent<UnityEngine.UI.Image>().sprite = null;
                        nbItem.text = "";
                        scriptItem.itemName = null;
                        scriptItem.description = null;
                        scriptItem.maxInventory = 0;
                        scriptItem.img = null;
                        itemInTile = false;
                    }
                    itemStored = true;
                }
                else if (emptyFound == false && itemStored == false && tilesArr[i].GetComponent<tileContent>().itemInTile == false)//good
                {
                    // new item in cooking tilesArr
                    emptyFound = true;
                    // tile in cooking receiving the item's data
                    itemIdentity itemInNewTile = tilesArr[i].GetComponent<itemIdentity>();
                    itemInNewTile.itemName = scriptItem.itemName; 
                    itemInNewTile.description = scriptItem.description; 
                    itemInNewTile.maxInventory = scriptItem.maxInventory; 
                    itemInNewTile.img = scriptItem.img;

                    //quantity to send
                    itemInNewTile.nb = nbOfItemsToSend; 
                    scriptItem.nb = scriptItem.nb - nbOfItemsToSend; 
                    tile.transform.GetChild(1).gameObject.transform.GetComponent<TextMeshProUGUI>().text = scriptItem.nb.ToString(); 

                    tilesArr[i].transform.GetChild(0).gameObject.transform.GetComponent<UnityEngine.UI.Image>().sprite = itemInNewTile.img; 
                    tilesArr[i].transform.GetChild(1).gameObject.transform.GetComponent<TextMeshProUGUI>().text = itemInNewTile.nb.ToString(); 

                    tilesArr[i].GetComponent<tileContent>().itemInTile = true;
                    //remove info on the last item
                    if (scriptItem.nb <= 0) 
                    {
                        imageItem.GetComponent<UnityEngine.UI.Image>().sprite = null;
                        nbItem.text = ""; 
                        scriptItem.itemName = null; 
                        scriptItem.description = null; 
                        scriptItem.maxInventory = 0; 
                        scriptItem.img = null; 
                        itemInTile = false;
                    }

                    itemStored = true; 
                }
            }
        }
    }


}


