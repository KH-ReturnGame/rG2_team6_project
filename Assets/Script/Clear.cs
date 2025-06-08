using UnityEngine;
using UnityEngine.SceneManagement;

public class Clear : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider2D;
    public GameObject[] enemies;
    public string nextGo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckEnemy();
    }

    void CheckEnemy()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                return;
            }
        }
        spriteRenderer.enabled = true;
        boxCollider2D.enabled = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            NextSceneGo(nextGo);
        }
    }

    void NextSceneGo(string i)
    {
        SceneManager.LoadScene(i);
    }

}
