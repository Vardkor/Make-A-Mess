using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoutonScript : MonoBehaviour
{
    public enum eItemtype {BoutonClim, BoutonDinosaure, BoutonTuto, BoutonBoris};
    public eItemtype itemType;

    public Light LightTutorial;
    private bool LightTutorialEnabled = true;
    

    public void Bouton()
    {
        switch(itemType)
        {
            case eItemtype.BoutonClim:
                BoutonClimEvent();
            break;

            case eItemtype.BoutonDinosaure:
                BoutonDinosaureEvent();
            break;

            case eItemtype.BoutonTuto:
                BoutonTutoEvent();
            break;
            
            case eItemtype.BoutonBoris:
                BoutonBorisEvent();
            break;
        }
    }

    void Update()
    {

    }

    void BoutonClimEvent()
    {
        Debug.Log("ClimeAugmentée");
    }

    void BoutonDinosaureEvent()
    {
        Debug.Log("DinosaureExplose");
    }

    void BoutonTutoEvent()
    {
        if(!LightTutorialEnabled)
        {
            LightTutorial.intensity = 1153.4f;
            LightTutorialEnabled = true;
        }
        else
        {
            LightTutorial.intensity = 0f;
            LightTutorialEnabled = false;
        }
    }

    void BoutonBorisEvent()
    {
        Debug.Log("C'est soirée DISCO");
    }
}
