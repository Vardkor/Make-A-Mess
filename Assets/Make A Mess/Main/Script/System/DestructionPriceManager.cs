using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DestructionPriceManagers : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> priceTexts;
    private Queue<int> lastPrices = new Queue<int>();

    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            lastPrices.Enqueue(0);
        }
        UpdateUI();
    }

    public void AddNewPrice(int price)
    {
        if (lastPrices.Count >= 4)
        {
            lastPrices.Dequeue();
        }

        lastPrices.Enqueue(price);
        UpdateUI();
    }

    private void UpdateUI()
    {
        int index = 0;
        foreach (int price in lastPrices)
        {
            priceTexts[index].text = price + "$";
            index++;
        }
    }
}

