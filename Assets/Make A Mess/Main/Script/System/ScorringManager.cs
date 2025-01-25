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

    void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(int scoreToAdd)
    {
        Debug.Log($"Ajout du score : {scoreToAdd}. Score actuel : {CurrentScore}");
        CurrentScore += scoreToAdd;
        UpdateScoreText();
        SFXCling.pitch = Random.Range(0.9f,1.1f); 
        SFXCling.Play();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = CurrentScore + "$";
        }
    }
}