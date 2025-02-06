using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScorringManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text comboText;
    [SerializeField] TMP_Text scoreBonusText;
    [SerializeField] AudioSource SFXCling;
    public Animator animator;
    public int CurrentScore = 0;
    public int progress = 0;
    [SerializeField] Slider sliderscombo;
    public GameObject slidersBarcombo;
    public GameObject GOscoreBonusText;

    public float comboResetTime = 3f;
    public float comboTimer;
    private int comboCount = 0;
    private int basePoints = 100;
    private int[] comboMultipliers = { 0, 1, 3, 5, 7, 10 };

    void Start()
    {
        sliderscombo.maxValue = comboResetTime;
        sliderscombo.value = 0;
        UpdateText();
        UpdateSlider();
        UpdateComboText();
    }

    void Update()
    {
        if (comboCount > 0)
        {
            comboTimer -= Time.deltaTime;
            UpdateSlider();

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
        int targetScore = CurrentScore + scoreToAdd;
        StartCoroutine(AnimateScoreIncrease(targetScore, 0.5f));

        UpdateText();
        UpdateSlider();

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
        if (sliderscombo != null)
        {
            sliderscombo.value = comboTimer;
            slidersBarcombo.SetActive(true);
        }
        else
        {slidersBarcombo.SetActive(false);}
    }

    private void UpdateComboText()
    {
        if (comboText != null)
        {
            if (comboCount > 0)
                comboText.text = "x" + comboCount + "!";
            else
                comboText.text = "";
        }

    }

    public void AddComboPoints()
    {
        comboTimer = comboResetTime;
        comboCount++;
        int multiplierIndex = Mathf.Min(comboCount, comboMultipliers.Length - 1);
        int pointsGained = basePoints * comboMultipliers[multiplierIndex];
        CurrentScore += pointsGained;

        if(scoreBonusText != null)
        {
            if(comboCount > 0)
            {
                scoreBonusText.text = "+" + pointsGained + "$";
                GOscoreBonusText.SetActive(true);
            }   
            else{GOscoreBonusText.SetActive(false);}
        }
        else{scoreBonusText.text = ""; GOscoreBonusText.SetActive(false);}

        UpdateSlider();
        UpdateComboText();
    }

    void ResetCombo()
    {
        comboCount = 0;
        UpdateComboText();
        slidersBarcombo.SetActive(false);
        GOscoreBonusText.SetActive(false);
    }

    IEnumerator AnimateScoreIncrease(int targetScore, float duration)
    {
        int startScore = CurrentScore;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            CurrentScore = (int)Mathf.Lerp(startScore, targetScore, elapsed / duration);
            UpdateText();
            yield return null;
        }

        CurrentScore = targetScore;
        UpdateText();
    }
}
