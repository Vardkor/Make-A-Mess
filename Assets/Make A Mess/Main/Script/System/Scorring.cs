using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scorring : MonoBehaviour
{
    [SerializeField] TMP_Text Score;
    public Animator animator;
    public int CurrentScore = 0;
    public int ScoreVitre = 100;
    public int ScorePeinture = 1;

    void Start()
    {
        Score.text = "Score : 0$";
    }


    void Update()
    {
        Score.text = CurrentScore.ToString() + "$";
    }

    public void UpdateScore()
    {
        CurrentScore += ScoreVitre;
        animator.SetTrigger("Change");
    }
}
