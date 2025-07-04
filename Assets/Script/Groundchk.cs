using UnityEngine;

public class Groundchk : MonoBehaviour
{
    public bool canjump;
    void OnCollisionEnter2D(Collision2D collision) //바닥과 충돌 감지
    {
        if (collision.gameObject.tag == "Ground")
        {
            canjump = true;
            Debug.Log("");
        }
        else
        {
            canjump = false;
            Debug.Log("");
        }

    } 
}

