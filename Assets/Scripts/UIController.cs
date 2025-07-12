using System;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [Header("UI Canvases")]
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject hudCanvas;
    [SerializeField] private GameObject restartMenuCanvas;

    [Header("HUD Elements")]
    [SerializeField] private TextMeshProUGUI coinsCounterText;

    public Action OnStartClick { get; set; }

    public void UpdateCoinsCounter(int coins)
    {
        coinsCounterText.text = $"Coins: {coins}";
    }

    public void OnStartClicked()
    {
        OnStartClick?.Invoke();
    }

    public void StartGame()
    {
        UpdateCoinsCounter(0);
        ShowOnly(hudCanvas);
    }
    
    public void OnExitClicked()
    {
        Application.Quit();
    }

    public void OnGameOver()
    {
        ShowOnly(restartMenuCanvas);
    }

    private void Awake()
    {
        ShowOnly(mainMenuCanvas);
    }
    
    private void ShowOnly(GameObject activeCanvas)
    {
        mainMenuCanvas.SetActive(activeCanvas == mainMenuCanvas);
        hudCanvas.SetActive(activeCanvas == hudCanvas);
        restartMenuCanvas.SetActive(activeCanvas == restartMenuCanvas);
    }
}