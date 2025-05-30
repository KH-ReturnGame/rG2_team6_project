using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class BankManager : MonoBehaviour
{
    public static BankManager instance;
    public int playerGold = 0;
    public int playerMaxGold = 999999;
    int playerAttackStep = 1;
    int playerAttackMaxStep = 22;
    [SerializeField]
    private ButtonManager buttonManager;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        PlayerPrefs.SetInt("PlayerAttackRate", 1);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (GameObject.Find("UI_Manager") != null)
        {
            buttonManager = GameObject.Find("UI_Manager").GetComponent<ButtonManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void PlusGold(int gold)
    {
        if (instance == null) return;

        instance.playerGold += gold;
        if (instance.playerGold > instance.playerMaxGold)
        {
            instance.playerGold = instance.playerMaxGold;
        }
        Debug.Log("Player Gold: " + instance.playerGold);
    }

    public static void ReinforcePlayer()
    {
        if (instance == null) return;
        if (instance.playerAttackStep >= instance.playerAttackMaxStep) return;
        if (instance.playerGold < 90 * (int)Mathf.Pow(1.5f, instance.playerAttackStep)) return;

        instance.playerGold -= 90 * (int)Mathf.Pow(1.5f, instance.playerAttackStep);
        instance.playerAttackStep++;
        PlayerPrefs.SetInt("PlayerAttackRate", instance.playerAttackStep * 2);

        if (GameObject.Find("UI_Manager") != null)
        {
            instance.buttonManager.GoldUpdate();
        }
    }
}
