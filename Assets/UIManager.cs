using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _countDownText;
    [SerializeField] 
    private Text _timerText;
    [SerializeField]
    private Text _healthText;
    [SerializeField] 
    private Text _pointText;
    [SerializeField]
    private Text _gameOver;
    [SerializeField]
    private Text _gameSolved;
    [SerializeField]
    private Button _exitButton;
    [SerializeField]
    private Button _restartButton;
    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("Game_Manager is NULL");
        }

        _gameOver.gameObject.SetActive(false);
        _gameSolved.gameObject.SetActive(false);
        _exitButton.gameObject.SetActive(false);
        _restartButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCountdownTime(int countDownTime)
    {
        _countDownText.gameObject.SetActive(true);
        _countDownText.text = countDownTime.ToString();
    }

    public void EndCountdown()
    {
        _countDownText.gameObject.SetActive(false);
    }

    public void StartTimer(int timerTime)
    {
        _timerText.text = timerTime.ToString();
    }

    public void updateHealth(int health)
    {
        _healthText.text = "Health: " + health.ToString();
    }

    public void UpdatePoint(int score)
    {
        _pointText.text = "latern: " + score.ToString() + "/5";
    }

    public void UpdateGameOver()
    {
        _gameOver.gameObject.SetActive(true);
        _gameManager.GameOver();
        ButtonSpawn();
    }

    public void UpdateGameSolved()
    {
        _gameSolved.gameObject.SetActive(true);
        _gameManager.GameSolved();
        Destroy(_gameOver);
        ButtonSpawn();
    }

    public void ButtonSpawn()
    {
        _exitButton.gameObject.SetActive(true);
        _restartButton.gameObject.SetActive(true);
    }
}
