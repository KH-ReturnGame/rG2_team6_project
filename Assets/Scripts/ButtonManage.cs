using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonManage : MonoBehaviour
{
    public void GameStart()
    {
        Debug.Log("Game Start");
        SceneManager.LoadScene("Stage1");

    }
    public void GameExit()
    {
        Debug.Log("Game Exite");
        Application.Quit();
    }
    void Update()
    {
        
    }
}
