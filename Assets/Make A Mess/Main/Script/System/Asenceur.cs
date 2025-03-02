using System.Collections;
using UnityEngine;

public class Ascenseur : MonoBehaviour
{
    public Transform etageBas;
    public Transform etageHaut;
    public float vitesse = 2f;
    private bool enMouvement = false;
    private bool auPremierEtage = false;

    public void AppuyerBouton()
    {
        if (!enMouvement)
        {
            StartCoroutine(MouvementAscenseur());
        }
    }

    IEnumerator MouvementAscenseur()
    {
        enMouvement = true;
        Vector3 depart = transform.position;
        Vector3 destination = auPremierEtage ? etageBas.position : etageHaut.position;

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * vitesse;
            transform.position = Vector3.Lerp(depart, destination, t);
            yield return null;
        }

        auPremierEtage = !auPremierEtage;
        enMouvement = false;
    }
}
