using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    EnemyMovement enemyMovement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyMovement = transform.parent.GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemyMovement.isDetected = true;
        }
    }
}
