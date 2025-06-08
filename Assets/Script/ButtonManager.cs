using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public TextMeshProUGUI goldText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerPrefs.SetInt("PlayerHP", 10);
        GoldUpdate();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        SceneManager.LoadScene("Stage 1");
    }

    public void RetryGame()
    {
        SceneManager.LoadScene("Start");
    }

    public void EndGame()
    {
        Application.Quit();
    }
    
    public void GoldUpdate()
    {
        if (goldText != null)
        {
            goldText.text = BankManager.instance.playerGold.ToString();
        }
    }
}
