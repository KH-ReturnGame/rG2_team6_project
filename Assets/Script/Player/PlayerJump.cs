using UnityEngine;
using System.Collections;

public class PlayerJump : MonoBehaviour
{
    PlayerMovemnt playerMovement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovement = transform.parent.GetComponent<PlayerMovemnt>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("platform"))
        {
            playerMovement.isOnGround = true;
            Debug.Log("Tlqkf");
        }
    }
    
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("platform"))
        {
            playerMovement.isOnGround = false;
        }
    }
}
