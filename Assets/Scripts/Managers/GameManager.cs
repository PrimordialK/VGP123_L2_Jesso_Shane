using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


[DefaultExecutionOrder(-10)]
public class GameManager : MonoBehaviour
{
    public AudioMixerGroup masterMixerGroup;
    public AudioMixerGroup musicMixerGroup;
    public AudioMixerGroup sfxMixerGroup;


    public delegate void PlayerSpawnDelegate(PlayerController playerInstance);
    public event PlayerSpawnDelegate OnPlayerControllerCreated;

    #region Player Controller Information
    public PlayerController playerPrefab;
    private PlayerController _playerInstance;
    public PlayerController playerInstance => _playerInstance;
    private Vector3 currentCheckpoint;
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
                //gameover goes here
                Debug.Log("Game Over! You have no lives left.");
                GameOver();
                _lives = 0;
            }
            else if (value < _lives)
            {
                //play hurt sound
                Debug.Log("Lost a life ");
                Respawn();

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
            Debug.Log($"Lives: {_lives}");
            OnLivesChanged?.Invoke(_lives);
        }
    }

    public int maxLives = 9;
    #endregion

    #region Singleton Pattern
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }

        Destroy(gameObject);
    }
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void GameOver()
    {
        SceneManager.LoadScene(2);
    }

    void Respawn()
    {
        if (_playerInstance != null)
        {
        Destroy(_playerInstance.gameObject);
        }
        _playerInstance = Instantiate(playerPrefab, currentCheckpoint, Quaternion.identity);
        if (OnPlayerControllerCreated != null) OnPlayerControllerCreated.Invoke(_playerInstance);
    }

    public void StartLevel(Vector3 startPositon)
    {
        currentCheckpoint = startPositon;
        _playerInstance = Instantiate(playerPrefab, currentCheckpoint, Quaternion.identity);
        OnPlayerControllerCreated?.Invoke(_playerInstance);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    int buildIndex = SceneManager.GetActiveScene().buildIndex;
        //    if (buildIndex == 0)
        //    {
        //        SceneManager.LoadScene(1);
        //    }
        //    else
        //    {
        //        SceneManager.LoadScene(0);
        //    }
        //}
    }
    public void StartGame() => SceneManager.LoadScene(1);
}