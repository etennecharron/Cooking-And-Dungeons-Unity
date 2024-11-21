using JetBrains.Annotations;
using Microsoft.Unity.VisualStudio.Editor;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class inventory : MonoBehaviour
{

    public GameObject grid;
    public GameObject tilesPrefab;
    public List<GameObject> tilesArr;

    // Inventory template
    [System.Serializable]
    public class Item
    {
        public string name;
        public string description;
        public int nb;
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
                }
            }
            if(index == inventoryPlayer.Count)
            {
                Item newItem = new Item();
                newItem.name = nameOfCollision;
                newItem.description = other.GetComponent<itemIdentity>().description;
                newItem.nb = 1;
                inventoryPlayer.Add(newItem);
            }
            
            Destroy(other.gameObject);

        }
    }

    public void inventoryCreator(int max, GameObject gridTemplate, GameObject tileTemplate, List<GameObject> arr)
    {

        int indexMax = max;
       for(int i = 0; i < indexMax; i++)
        {
                Instantiate(tileTemplate, new Vector3(0,0,0) ,Quaternion.identity).transform.SetParent(gridTemplate.transform);
            if(i == indexMax - 1)
            {
                for (int x = 0;x < indexMax; x++)
                {
                    arr.Add(grid.transform.GetChild(x).gameObject);
                }
            }
        }
       
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //createInventory();
        inventoryCreator(maxInventorySize, grid, tilesPrefab, tilesArr);
    }

    // Update is called once per frame
    void Update()
    {
    }
 
}
