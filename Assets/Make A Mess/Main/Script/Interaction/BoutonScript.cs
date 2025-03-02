using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoutonScript : MonoBehaviour
{
    public enum eItemtype {BoutonClim, BoutonDinosaure, BoutonTuto, BoutonBoris, BoutonAsenceur};
    public eItemtype itemType;

    public Ascenseur ascenceurscript;
    public Light[] LightTutorials;
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
            case eItemtype.BoutonAsenceur:
                Asenceur();
                break;
        }
    }

    void Update()
    {
        
    }

    void BoutonClimEvent()
    {
        Debug.Log("Clime Augmentée");
    }

    void BoutonDinosaureEvent()
    {
        Debug.Log("Dinosaure Explose");
    }

    public void BoutonTutoEvent()
    {
        LightTutorialEnabled = !LightTutorialEnabled;

        foreach (Light light in LightTutorials)
        {
            if (light != null)
            {
                light.intensity = LightTutorialEnabled ? 5000f : 0f;
            }
        }
    }

    void BoutonBorisEvent()
    {
        Debug.Log("C'est soirée DISCO");
    }

    void Asenceur()
    {
        ascenceurscript.AppuyerBouton();
    }

    void OnTriggerEnter(Collider other)
    {
        Asenceur();
    }
}
