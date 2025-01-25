using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BombePeinture : MonoBehaviour
{

    [SerializeField] private DecalProjector decalProjector; 
    [SerializeField] private Transform decalPosition;
    //[SerializeField] public Scorring score;

    public void Peindre()
    {
        DecalProjector newDecal = Instantiate(decalProjector, decalPosition.position, decalPosition.rotation);
        newDecal.size = new Vector3(0.2f, 0.2f, 0.2f); 
        //ScorePeinture();
    }

    public void ScorePeinture()
    {
        //score.CurrentScore += score.ScorePeinture;
    }
}
