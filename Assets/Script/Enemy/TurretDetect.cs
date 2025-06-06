using UnityEngine;

public class TurretDetect : MonoBehaviour
{
    Turret turret;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        turret = transform.parent.GetComponent<Turret>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            turret.isDetected = true;
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            turret.isDetected = false;
        }
    }
}
