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
    public itemIdentity scripItem;
    public GameObject cookingParent;
    void Start()
    {
        Button btn = tile.GetComponent<Button>();
        
        btn.onClick.AddListener(onClick);
    }
    void onClick()
    {

        // ADDS ITEM INTO THE COOKING UI
        if(scripItem.itemName != "" && tile.GetComponentInParent<Transform>().parent.name == "gridInventory")
        {
            cookingParent = GameObject.Find("ButtonCooking");
            List<GameObject> tilesArr = cookingParent.GetComponent<cooking>().tilesArr;
            bool emptyFound = false;
            for (int i = 0; i < tilesArr.Count; i++)
            {
                if(tilesArr[i].GetComponent<itemIdentity>().itemName == "" && emptyFound == false)
                {

                    // new item in cooking tilesArr
                    emptyFound = true;
                    itemIdentity itemInNewTile = tilesArr[i].GetComponent<itemIdentity>();
                    itemInNewTile.itemName = scripItem.itemName;
                    itemInNewTile.description = scripItem.description;
                    itemInNewTile.maxInventory = scripItem.maxInventory;
                    itemInNewTile.img = scripItem.img;
                    tilesArr[i].transform.GetChild(0).gameObject.transform.GetComponent<UnityEngine.UI.Image>().sprite = itemInNewTile.img;
                    //tilesArr[i].transform.GetChild(1).gameObject.transform.GetComponent<TextMeshProUGUI>().text =
                    //remove info on the last item
                    imageItem.GetComponent<UnityEngine.UI.Image>().sprite = null;
                    nbItem.text = "";
                    scripItem.itemName = null;
                    scripItem.description = null;
                    scripItem.maxInventory = 0;
                    scripItem.img = null;
                }
                

            }
        }
       
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
