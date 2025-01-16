using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golf : MonoBehaviour
{

    public void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<Collider>().CompareTag("ballegolf"))
        {
            Debug.Log("In the trou");
        }
    }
}
