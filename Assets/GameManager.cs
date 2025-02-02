using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver;
    [SerializeField] 
    private bool _isGameSolved;
    [SerializeField]
    private int _countDownTime = 10;
    [SerializeField]
    private int _timerRemaining;
    [SerializeField]
    private GameObject [] gameplayElement;
    private UIManager _UImanager;
    [SerializeField]
    private Wall wallTilemap;
    // Start is called before the first frame update
    void Start()
    {
        _UImanager = GameObject.Find("Canvas").GetComponent<UIManager>();
        foreach (GameObject element in gameplayElement)
        {
            element.SetActive(false);
        }
        StartCoroutine(StartAfterDelay());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartAfterDelay()
    {
        for (int i = _countDownTime; i > 0; i--)
        {
            _UImanager.StartCountdownTime(i);
            yield return new WaitForSeconds(1);
        }
        yield return new WaitForSeconds(1);
        _UImanager.EndCountdown();
        foreach (GameObject element in gameplayElement)
        {
            element.SetActive(true);
        }
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        while(_timerRemaining >= 0 && _isGameOver == false && _isGameSolved == false)
        {
            _UImanager.StartTimer(_timerRemaining);
            yield return new WaitForSeconds(1);
            _timerRemaining--;
        }

        _UImanager.UpdateGameOver();
    }

    public void GameOver()
    {
        _isGameOver = true;
        _isGameSolved = false;
    }

    public void GameSolved()
    {
        _isGameSolved = true;
        _isGameOver = false;
        wallTilemap.ShowWall(120);
    }

    public void Restart()
    {
        if (_isGameOver == true || _isGameSolved == true)
        {
            Debug.Log("Reloading scene...");
            SceneManager.LoadScene(1);
        }
    }

    public void ExittoMainMenu()
    {
        if (_isGameOver == true || _isGameSolved == true)
        {
            Debug.Log("Exit to main menu scene...");
            SceneManager.LoadScene(0);
        }
    }
}
