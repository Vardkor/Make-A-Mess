using System.Collections.Generic;
using UnityEngine;

public class FirePoolManager : MonoBehaviour
{
    public static FirePoolManager Instance; // Singleton pour un accès global
    public GameObject firePrefab; // Prefab du feu à instancier
    public int poolSize = 10; // Nombre d'objets dans le pool

    private Queue<GameObject> firePool = new Queue<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject fireInstance = Instantiate(firePrefab);
            fireInstance.SetActive(false);
            firePool.Enqueue(fireInstance);
        }
    }

    public GameObject GetFire()
    {
        if (firePool.Count > 0)
        {
            GameObject fireInstance = firePool.Dequeue();
            fireInstance.SetActive(true);
            return fireInstance;
        }
        else
        {
            Debug.LogWarning("Plus de feux dans le pool ! Pense à augmenter la taille.");
            return null;
        }
    }

    public void ReturnFire(GameObject fireInstance)
    {
        fireInstance.SetActive(false);
        firePool.Enqueue(fireInstance);
    }
}
