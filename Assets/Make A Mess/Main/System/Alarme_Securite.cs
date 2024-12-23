 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarme_Securite : MonoBehaviour
{
    [SerializeField] public Vitre vitre;
    public bool AlarmeActiv = false;

    public void Start()
    {
        AlarmeActiv = true;
    }
    void Update()
    {
        if(vitre.vitrebreak)
        {
            if(AlarmeActiv)
            {
                Debug.Log("Alarme Active");
            }      
        }
        else if(!AlarmeActiv)
        {
            Debug.Log("Alarme Desactiver");
        }
    }


    public void ActiveAlarme()
    {
        AlarmeActiv = false;
    }

    public void PlaySound()
    {
        
    }

}
