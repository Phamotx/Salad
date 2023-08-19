using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    [SerializeField]
    private int score;
    [SerializeField]
    private PlateScript ps;
    [SerializeField]
    private float playerTime;
    [SerializeField]
    private Text ScoreText;
    [SerializeField]
    private Text TimeText;


    public int Score { get => score; set => score = value; }
    public PlateScript Ps { get => ps; set => ps = value; }
    public float PlayerTime { get => playerTime; set => playerTime = value; }
    private void Update()
    {
        playerTime -= Time.deltaTime;
        TimeText.text = Mathf.Round(PlayerTime).ToString();
        ScoreText.text = Score.ToString();
    }
}
