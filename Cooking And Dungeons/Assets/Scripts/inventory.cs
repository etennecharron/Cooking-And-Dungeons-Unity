using JetBrains.Annotations;
using Microsoft.Unity.VisualStudio.Editor;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class inventory : MonoBehaviour
{

    
    public GameObject tilesPrefab;

    public GameObject gridInventory;
    public List<GameObject> tilesInventoryArr;

    public GameObject gridCooking;
    public List<GameObject> tilesCookingArr;
    public int maxCookingTiles;
    public cooking cookingScript; 

    public UnityEngine.UI.Button cookingBtn;

    // Inventory template
    [System.Serializable]
    public class Item
    {
        public string name;
        public string description;
        public int maxInventory;
        public Sprite img;

        public int nb;
        public int positionTiles;
    }
    public int maxInventorySize;
    public List<Item> inventoryPlayer = new();

    // Adds item to inventory when collided with
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "item" && inventoryPlayer.Count < maxInventorySize)
        {
            string nameOfCollision = other.GetComponent<itemIdentity>().itemName;
            int index = 0;
            foreach (Item item in inventoryPlayer)
            {
                if(item.name != nameOfCollision)
                {
                    index++;
                }
                else
                {
                    item.nb++;
                    tilesInventoryArr[item.positionTiles].transform.GetChild(1).gameObject.transform.GetComponent<TextMeshProUGUI>().text = item.nb.ToString();
                }
            }
            if(index == inventoryPlayer.Count)
            {
                Item newItem = new Item();
                // STORE ITEM'S INFORMATIONS
                newItem.name = nameOfCollision;
                newItem.description = other.GetComponent<itemIdentity>().description;
                newItem.maxInventory = other.GetComponent<itemIdentity>().maxInventory;
                newItem.img = other.GetComponent<SpriteRenderer>().sprite;
                newItem.nb = 1; 
                inventoryPlayer.Add(newItem);

                bool itemStored = false;
                for(int i = 0; i < tilesInventoryArr.Count; i++)
                {
                       Sprite itemImage = tilesInventoryArr[i].transform.GetChild(0).gameObject.transform.GetComponent<UnityEngine.UI.Image>().sprite;
                       itemIdentity tileScript = tilesInventoryArr[i].GetComponent<itemIdentity>();
                    if ( itemImage == null && itemStored == false)
                    {
                        tileScript.itemName = newItem.name;
                        tileScript.description = newItem.description;
                        tileScript.maxInventory = newItem.maxInventory;
                        tileScript.img = newItem.img;

                        itemImage = newItem.img;
                        newItem.positionTiles = i;
                        tilesInventoryArr[i].transform.GetChild(0).gameObject.transform.GetComponent<UnityEngine.UI.Image>().sprite = newItem.img;
                        tilesInventoryArr[i].transform.GetChild(1).gameObject.transform.GetComponent<TextMeshProUGUI>().text =  newItem.nb.ToString();
                        itemStored = true;
                    }
                    
                }
            }
            
            Destroy(other.gameObject);

        }
    }

    public void gridCreator(int max, GameObject gridTemplate, GameObject tileTemplate, List<GameObject> arr)
    {

        int indexMax = max;
       for(int i = 0; i < indexMax; i++)
        {
                Instantiate(tileTemplate, new Vector3(0,0,0) ,Quaternion.identity).transform.SetParent(gridTemplate.transform);
            if(i == indexMax - 1)
            {
                for (int x = 0;x < indexMax; x++)
                {
                    arr.Add(gridTemplate.transform.GetChild(x).gameObject);
                }
            }
        }
       
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       gridCreator(maxInventorySize, gridInventory, tilesPrefab, tilesInventoryArr);
       gridCreator(maxCookingTiles,gridCooking, tilesPrefab, cookingScript.tilesArr);

        UnityEngine.UI.Button btn = cookingBtn.GetComponent<UnityEngine.UI.Button>();
        btn.onClick.AddListener(cookClick);
    }
    void cookClick()
    {

    }
    // Update is called once per frame
    void Update()
    {
    }
 
}
