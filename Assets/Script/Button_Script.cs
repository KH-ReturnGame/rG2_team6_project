using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_Script: MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void game_start()
    {
        Debug.Log("Start_to_game");
        SceneManager.LoadScene("Stage1_Scenes");
    }

    public void game_end()
    {
        Debug.Log("game_end");
        Application.Quit();
    }
}
