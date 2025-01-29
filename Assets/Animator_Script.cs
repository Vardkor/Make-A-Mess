using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_Script : MonoBehaviour
{
    public Animator AnimationAttackPDB;
    public bool startanim = false;
    public void AnimPDB()
    {
        if(startanim == true)
        {
            AnimationAttackPDB.SetTrigger("TriggerAttack");
            Debug.Log("Trigger");
        }
    }
}
