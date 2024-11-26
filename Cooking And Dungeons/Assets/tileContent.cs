using UnityEngine;
using UnityEngine.UI;

public class tileContent : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    public Button tile;

    void Start()
    {
        Button btn = tile.GetComponent<Button>();
        btn.onClick.AddListener(onClick);
    }
    void onClick()
    {
       
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
