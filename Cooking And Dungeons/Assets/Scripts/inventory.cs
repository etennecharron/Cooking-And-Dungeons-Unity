using JetBrains.Annotations;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class inventory : MonoBehaviour
{


    // Inventory template
    [System.Serializable]
    public class Item
    {
        public string name;
        public string description;
        public int nb;
    }
    private int maxInventorySize = 80;

    public List<Item> inventoryPlayer = new();
    public GameObject inventoryPrefab;
    public GameObject inventoryContainer;
    public GameObject inventoryStart;
    public List<GameObject> inventorySquaresArr;
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


    public void createInventory()
    {
        int nbSquareX = 8;
        int size = (int)inventoryStart.GetComponent<Collider2D>().bounds.size.x;
        int gap = 12;
        int positionX = 0;
        int positionY = 0;
        int startX = (int)inventoryStart.transform.position.x;
        int startY = (int)inventoryStart.transform.position.y;
        int startZ = (int)inventoryStart.transform.position.y;
        for (int i = 0; i < maxInventorySize; i++)
        {
           
            if (positionX != nbSquareX)
            {
               if(positionX == 0 && positionY != 0)
                {
                    positionX++;
                }
                
              Instantiate(inventoryPrefab, new Vector3(startX + size * positionX + gap * positionX, startY - size *positionY - gap * positionY, startZ), Quaternion.identity).transform.SetParent(inventoryContainer.transform);
               
                positionX++;
            }
            else
            {
                positionY++;
                positionX = 0;
                Instantiate(inventoryPrefab, new Vector3(startX, startY - size * positionY - gap * positionY, startZ), Quaternion.identity).transform.SetParent(inventoryContainer.transform);
                

            }
            if (i == maxInventorySize-1)
            {
                Destroy(inventoryStart.gameObject);
                for(int x = 0; x < maxInventorySize;x++)
                inventorySquaresArr.Add(inventoryContainer.transform.GetChild(x).gameObject);
            }
            
            }
    }




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        createInventory();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 
}
