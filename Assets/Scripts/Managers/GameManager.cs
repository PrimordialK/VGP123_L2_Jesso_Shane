using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public delegate PlayerController PlayerSpawnDelegate(PlayerController playerInstance);
    
        

    #region PlayerControllerInformation
    public PlayerController playerPrefab;
    private PlayerController _playerInstance;
    public PlayerController playerInstance => _playerInstance;
    private Vector3 currentCheckPoint;
    #endregion
    public event Action<int> OnLivesChanged;
    #region Stats
    private int _lives = 3;
    private int _score = 0;
    public int score
    {
        get => _score;
        set
        {
            if (value < 0)
                _score = 0;
            else
                _score = value;
        }
    }

    public int lives
    {
        get => _lives;
        set
        {
            if (value < 0)
            {
                Debug.Log("Game Over");
                _lives = 0;
                SceneManager.LoadScene(2);

            }
           
            else if (value < _lives)
            {
                Respawn();
                Debug.Log("You lost a Life");
                _lives = value;
               
            }
            else if (value > maxLives)
            {
                _lives = maxLives;
            }
            else
            {
                _lives = value;
            }
            if (_lives == 0)
            
        } 
    }

    public int maxLives = 9;
    #endregion

    #region Singleton Pattern
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private void Awake()
    {
        if (_instance == null) // corrected: == instead of =
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Respawn()
    {
        if (_playerInstance != null)
        {
            Destroy(_playerInstance.gameObject);
        }
        _playerInstance = Instantiate(playerPrefab, currentCheckPoint, Quaternion.identity);
    }

    public void StartLevel(Vector3 startPosition)
    {
        currentCheckPoint = startPosition;
        _playerInstance = Instantiate(playerPrefab, currentCheckPoint, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                SceneManager.LoadScene(1);
            }
           else if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }
        
    }
}

