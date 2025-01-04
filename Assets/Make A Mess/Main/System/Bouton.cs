using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bouton : MonoBehaviour
{
    public Transform ButtonTop;
    public Transform ButtonLowerLimit;
    public Transform ButtonUperLimit;
    
    public float threshHold = 0.5f;
    private float UpperLowerDiff;
    public float Force = 10f;

    public bool IsPressed;
    private bool prevPressedState;

    //public AudioSource PressedSound;

    public UnityEvent OnPressed;
    public UnityEvent OnReleased;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreCollision(GetComponent<Collider>(),ButtonTop.GetComponent<Collider>());
        if(transform.eulerAngles != Vector3.zero)
        {
            Vector3 savedAngle = transform.eulerAngles;
            transform.eulerAngles = Vector3.zero;
            UpperLowerDiff = ButtonUperLimit.position.y - ButtonLowerLimit.position.y;
            transform.eulerAngles = savedAngle;
        }
        else
        {
            UpperLowerDiff = ButtonUperLimit.position.y - ButtonLowerLimit.position.y;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ButtonTop.transform.localPosition = new Vector3(0, ButtonTop.transform.localPosition.y, 0);
        ButtonTop.transform.localEulerAngles = new Vector3(0,0,0);

        if(ButtonTop.localPosition.y >= 0) //UperLimit
        {
            ButtonTop.transform.position = new Vector3(ButtonUperLimit.position.x, ButtonUperLimit.position.y, ButtonUperLimit.position.z);
        }
        else
        {
            ButtonTop.GetComponent<Rigidbody>().AddForce(ButtonTop.transform.up * Force * Time.fixedDeltaTime);
        }

        if(ButtonTop.localPosition.y <= ButtonLowerLimit.position.y) //LowerLimit
        {
            ButtonTop.transform.position = new Vector3(ButtonLowerLimit.position.x, ButtonLowerLimit.position.y, ButtonLowerLimit.position.z);
        }

        if(Vector3.Distance(ButtonTop.position, ButtonLowerLimit.position) <= UpperLowerDiff * threshHold)
        {
            IsPressed = true;
        }
        else
        {
            IsPressed = false;
        }

        if(IsPressed && prevPressedState != IsPressed)
        {
            Pressed();
        }

        if(!IsPressed && prevPressedState != IsPressed)
        {
            Released();
        }
    }

    public void Pressed()
    {
        prevPressedState = IsPressed;
        //PressedSound.ptich = 1;
        //PressedSound.Play();
        OnPressed.Invoke();
    }

    public void Released()
    {
        prevPressedState = IsPressed;
        OnReleased.Invoke();
    }
}
