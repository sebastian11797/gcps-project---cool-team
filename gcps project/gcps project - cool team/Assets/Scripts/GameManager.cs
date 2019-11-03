using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour {

    private const int CoinAmount = 5;

	public static GameManager Instance { set; get; }

    private bool isGameStarted = false;
    private MovePlayer move;

    private bool isDead = false;

    //UI

    public Text scoreText, coinText, modifierText;
    private float score, coinScore, modifierScore;
    private int lastScore;

    //Death menu
    public Animator deathMenuAnim;
    public Text finalScoreText, finalCoinText;

    private void Awake()
    {
        Instance = this;
        modifierScore = 1;
        move = GameObject.FindGameObjectWithTag("Player").GetComponent<MovePlayer>();
        modifierText.text = "x " + modifierScore.ToString("0");
        scoreText.text = score.ToString("0");
        coinText.text = coinScore.ToString("0");
    }

    private void Update()
    {
        if (isDead)
        {
            return;
        }

        if (MobileInput.Instance.Tap && !isGameStarted)
        {
            isGameStarted = true;
            //move.StartRunning();
        }

        if (isGameStarted && !isDead)
        {
            //start increasing score
            score += (Time.deltaTime * modifierScore);

            if (lastScore != (int)score)
            {
                lastScore = (int)score;
                scoreText.text = score.ToString("0");
            }
        }
    }

    public void UpdateModifier(float modifierAmount)
    {
        modifierScore = 1f + modifierAmount;
        modifierText.text = "x " + modifierScore.ToString("0");
    }

    public void GetCoin()
    {
        coinScore++;
        coinText.text = coinScore.ToString("0");
        score += CoinAmount;
        scoreText.text = scoreText.text = score.ToString("0");

    }

    public void OnDeath()
    {
        isDead = true;
        
        finalScoreText.text = "Score: " + score.ToString("0");
        finalCoinText.text = "Coins: " + coinScore.ToString("0");
        deathMenuAnim.SetTrigger("Dead");
    }

    public void OnPlayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scene1");
    }


}
