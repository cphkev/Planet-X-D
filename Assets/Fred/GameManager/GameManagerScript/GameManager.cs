using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] public int coinsNeededToWin;
    private int currentCoins = 0;

    [SerializeField] public TextMeshProUGUI winText; // Change from Text to TextMeshProUGUI
    [SerializeField] public TextMeshProUGUI coinCounter; 

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        winText.gameObject.SetActive(false); // Hide win text at the start
        UpdateCoinText();
    }

    public void AddCoin()
    {
        currentCoins++;
        UpdateCoinText();

        if (currentCoins >= coinsNeededToWin)
        {
            WinGame();
        }
    }
    
    void WinGame()
    {
        winText.gameObject.SetActive(true); // Show "You Win!" text
    }


    void UpdateCoinText()
    {
        if (coinCounter != null)
        {
            coinCounter.text = "Coins: " + currentCoins;
        }
    }
}