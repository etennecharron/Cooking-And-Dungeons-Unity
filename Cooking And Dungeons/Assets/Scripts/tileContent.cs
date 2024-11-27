using UnityEngine;
using UnityEngine.UI;

public class tileContent : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    public GameObject tile;
    public itemIdentity scripItem;


    void Start()
    {
        Button btn = tile.GetComponent<Button>();
        
        btn.onClick.AddListener(onClick);
    }
    void onClick()
    {
        Debug.Log(tile.GetComponentInParent<Transform>().parent.name);
        if(scripItem.itemName == "" && tile.GetComponentInParent<Transform>().parent.name == "gridInventory")
        {
            Debug.Log("object dans l'inventaire");    
        }
       
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
