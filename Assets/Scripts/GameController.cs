using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private UIController uiController;
    [SerializeField] private GameObject levelPrefab;
    [SerializeField] private Transform levelSpawnPoint;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Transform playerSpawnPoint;

    private GameObject _currentLevel;
    private int _coins;

    private void OnEnable()
    {
        uiController.OnStartClick += StartGame;
        
        playerController.OnCoinCollected += OnCoinCollected;
        playerController.OnDeath   += OnPlayerDeath;
    }

    private void OnDisable()
    {
        uiController.OnStartClick -= StartGame;
        
        playerController.OnCoinCollected -= OnCoinCollected;
        playerController.OnDeath   -= OnPlayerDeath;
    }

    private void StartGame()
    {
        if (_currentLevel != null)
            Destroy(_currentLevel);
        
        _currentLevel = Instantiate(levelPrefab, levelSpawnPoint);
        
        _coins = 0;
        uiController.StartGame();
        playerController.StartGame();
        playerController.transform.position = playerSpawnPoint.position;
    }
    
    private void OnCoinCollected()
    {
        _coins++;
        uiController.UpdateCoinsCounter(_coins);
    }
    
    private void OnPlayerDeath()
    {
        uiController.OnGameOver();
    }
}
