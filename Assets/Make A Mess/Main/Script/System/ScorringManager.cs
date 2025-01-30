using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScorringManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] AudioSource SFXCling;
    public Animator animator;
    public int CurrentScore = 0;
    public int progress = 0;
    [SerializeField] Slider sliderscore;

    public float comboResetTime = 3f; // Temps max pour enchaîner un combo
    private float comboTimer; // Timer du combo
    private int comboCount = 0; // Niveau du combo
    private int basePoints = 100; // Points de base par objet
    private int[] comboMultipliers = { 1, 3, 5, 7, 10 }; // Multiplicateurs de points

    void Start()
    {
        UpdateText();
        UpdateSlider();
    }

    void Update()
    {
        if (comboCount > 0)
        {
            comboTimer -= Time.deltaTime;
            if (comboTimer <= 0)
            {
                ResetCombo();
            }
        }
    }

    public void AddScore(int scoreToAdd)
    {
        UpdateScore(scoreToAdd);
        AddComboPoints();
    }

    private void UpdateScore(int scoreToAdd)
    {
        CurrentScore += scoreToAdd;
        UpdateText();
        UpdateSlider(); // Met à jour le slider
        SFXCling.pitch = Random.Range(0.9f, 1.1f);
        SFXCling.Play();
    }

    private void UpdateText()
    {
        if (scoreText != null)
        {
            scoreText.text = CurrentScore + "$";
        }
    }

    private void UpdateSlider()
    {
        if (sliderscore != null)
        {
            sliderscore.value = CurrentScore; // MAJ la valeur du slider avec le score
        }
    }

    public void AddComboPoints()
    {
        comboTimer = comboResetTime;
        comboCount = Mathf.Clamp(comboCount + 1, 0, comboMultipliers.Length - 1);
        int pointsGained = basePoints * comboMultipliers[comboCount];
        CurrentScore += pointsGained;
        UpdateSlider(); // Met à jour le slider après ajout des points
    }

    void ResetCombo()
    {
        comboCount = 0;
    }
}
