using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCombo : MonoBehaviour
{
    public float comboResetTime = 3f; // Temps max pour enchaîner un combo
    private float comboTimer; // Timer du combo
    private int comboCount = 0; // Niveau du combo
    private int basePoints = 100; // Points de base par objet
    private int[] comboMultipliers = { 1, 3, 5, 7, 10 }; // Multiplicateurs de points (1 x 100, 3 x 100, ect)

    private int currentScore = 0;

    void Update()
    {
        // Réduction du timer du combo
        if (comboCount > 0)
        {
            comboTimer -= Time.deltaTime;
            if (comboTimer <= 0)
            {
                ResetCombo();
            }
        }
    }

    public void AddComboPoints() //ça va remplacer ton activatemultipliers()
    {
        // Réinitialise le timer à chaque destruction
        comboTimer = comboResetTime;

        // Incrémente le combo (sans dépasser la taille du tableau)
        comboCount = Mathf.Clamp(comboCount + 1, 0, comboMultipliers.Length - 1);

        // Calcule les points gagnés avec le multiplicateur du palier
        int pointsGained = basePoints * comboMultipliers[comboCount];
        currentScore += pointsGained;

        Debug.Log($"Combo {comboCount} ! +{pointsGained} points (Score: {currentScore})");
    }

    void ResetCombo()
    {
        Debug.Log("Combo perdu !");
        comboCount = 0;
    }

    //Et la ligne de code à mettre pour appeler le combo c'est ça : FindObjectOfType<Le nom du script>().AddComboPoints();
}

