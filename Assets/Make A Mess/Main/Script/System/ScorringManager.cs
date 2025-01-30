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

    private float multiplierTimer = 5f;
    private float multiplierDuration = 5f;

    void Start()
    {
        UpdateText();
    }

    public void AddScore(int scoreToAdd)
    {
        UpdateScore(scoreToAdd);
        ActiveMultipliers();
    }

    private void UpdateScore(int scoreToAdd)
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

    public void ActiveMultipliers()
    {
        
    }  
}