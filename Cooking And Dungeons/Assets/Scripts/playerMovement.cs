using UnityEngine;

public class playerMovement : MonoBehaviour
{

    [SerializeField] private float movementSpeed = 2f;

    private Rigidbody2D rb;

    private Vector2 movementDirection;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }


    private void FixedUpdate()
    {
        rb.linearVelocity = movementDirection * movementSpeed;
        
    }
}
