using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private PlayerScore player_1;
    private PlayerScore player_2;
    [SerializeField]
    private GameObject ScoreResult;
    [SerializeField]
    private Text ResultText;
    [SerializeField]
    private Button retry_btn;
    [SerializeField]
    private Button play_btn;
    [SerializeField]
    private GameObject GameStartPanel;
    private void Awake()
    {
        Time.timeScale = 0f;
        GameObject obj = GameObject.Find("Player_1");
        GameObject obj2 = GameObject.Find("Player_2");
        player_1 = obj.GetComponent<PlayerScore>();
        player_2 = obj2.GetComponent<PlayerScore>();
        retry_btn.onClick.AddListener(() => ResetGame());
        play_btn.onClick.AddListener(() => Play());
    }
    void Play()
    {
        GameStartPanel.SetActive(false);
        Time.timeScale = 1f;

    }
    private void Update()
    {
        if (player_1.PlayerTime <= 0 && player_2.PlayerTime <= 0)
        {
            if (player_1.Score > player_2.Score)
            {
                ResultText.text = "Player 1 Wins";
            }
            else if (player_1.Score < player_2.Score)
            {
                ResultText.text = "Player 2 Wins";
            }
            else
            {
                ResultText.text = "Draw";
            }
            ShowResult();
            //Stop Game
            Time.timeScale = 0f;
        }
    }

    void ShowResult()
    {
        ScoreResult.SetActive(true);
    }
    void ResetGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
