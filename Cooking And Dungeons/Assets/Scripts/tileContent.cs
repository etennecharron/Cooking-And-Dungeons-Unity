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
    public int nbOfItemsToSend = 1;
    void Start()
    {
        Button btn = tile.GetComponent<Button>();
        
        btn.onClick.AddListener(onClick);
    }
    void onClick()
    {
        // ADDS ITEM INTO THE COOKING UI
        if(scriptItem.itemName != "" && tile.GetComponentInParent<Transform>().parent.name == "gridInventory")
        {
            cookingParent = GameObject.Find("ButtonCooking");
            List<GameObject> tilesArr = cookingParent.GetComponent<cooking>().tilesArr;
            bool emptyFound = false;
            bool itemStored = false;
            for (int i = 0; i < tilesArr.Count; i++)
            {
                

                if (tilesArr[i].GetComponent<itemIdentity>().itemName == tile.GetComponent<itemIdentity>().itemName && itemStored == false)
                {
                    Debug.Log("partie 1");
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
                    }
                    itemStored = true;
                    Debug.Log(itemStored);
                }
                else if (tilesArr[i].GetComponent<itemIdentity>().itemName == "" && emptyFound == false && itemStored == false)
                {
                    Debug.Log(itemStored);
                    Debug.Log("partie 2");
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

                    //remove info on the last item
                    if (scriptItem.nb <= 0)
                    {
                        imageItem.GetComponent<UnityEngine.UI.Image>().sprite = null;
                        nbItem.text = "";
                        scriptItem.itemName = null;
                        scriptItem.description = null;
                        scriptItem.maxInventory = 0;
                        scriptItem.img = null;
                    }
                     
                    itemStored = true;
                }
            }
        }
       
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
