using System.ComponentModel.Design;
using UnityEngine;

public class enemyBehavior : MonoBehaviour
{

    public GameObject player;
    public float speed;
    private float distance;
    public int distanceMax;

    public int hp;
    public int hpMax;
    public int dmg;

    public bool playerHit;

    public Collider2D hitbox;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    public void movingTowardPlayer()
    {
        
        if(playerHit == false)
        {
            Debug.Log(playerHit);
            distance = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = player.transform.position - transform.position;
        }
        

        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);


        /*
        if(distance < distanceMax)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime); ;
        }
        */

    }

    

    public void OnTriggerEnter2D(Collider2D other) { 
    
    if(other.tag == "Player")
        {
            other.transform.GetComponent<playerManager>().hp = other.transform.GetComponent<playerManager>().hp - dmg;
            Debug.Log("player has been hit!");
            playerHit = true;
        }
    }

    


    void Update()
    {
        
    }
}
