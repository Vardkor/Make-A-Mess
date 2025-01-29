using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScorringManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] AudioSource SFXCling;
    public Animator animator;
    public int CurrentScore = 0;

    //Multilier\\

    private int multiplier = 1;
    private int multiplierBase = 1;
    private float multiplierResetTime = 3f;
    
    //Multilier Timer\\

    private float multiplierTimer = 0f;
    private float multiplierDuration = 5f;

    void Start()
    {
        UpdateScoreText();
    }

    void Update()
    {
        Debug.Log("Multipliers" + multiplier);

        if(multiplier > 1f)
        {
            multiplierTimer -= Time.deltaTime;
            if(multiplierTimer <= 0f)
            {
                multiplier = multiplierBase;
            }
        }
    }


    public void AddScore(int scoreToAdd)
    {
        //scoreToAdd *= multiplier;
        UpdateScore();
        ActiveMultipliers();
    }

    private void UpdateScore()
    {
        CurrentScore += scoreToAdd;
        UpdateText();
        SFXCling.pitch = Random.Range(0.9f,1.1f); 
        SFXCling.Play();
    }
    private void UpdateText()
    {
        if (scoreText != null)
        {
            scoreText.text = CurrentScore + "$";
        }
    }

    private void ActiveMultipliers()
    {

    }
}