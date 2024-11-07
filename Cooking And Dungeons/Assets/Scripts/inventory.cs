using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
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
    public List<Item> inventoryPlayer = new();
  


    // Adds item to inventory when collided with
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "item")
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 
}
