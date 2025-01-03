using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Tuto_Text : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Text;
    public bool Box1;
    public bool Box2;
    public bool Box3;
    public bool Box4;
    public bool Box5;
    public bool BoxText;


    public bool ControlActif = true;
    public bool StartTimerBloc;
    public bool StartTimerEnd;
    [SerializeField] float remainingTimeBloc;
    [SerializeField] float remainingTimeEnd;

    void Start()
    {
        Text.text = "Bienvenue dans le musée. Pour commencer, utilise les touches Z, Q, S, D pour te déplacer. Appuie sur ESPACE pour sauter. Essaye de te promener un peu.";
        ControlActif = true;
    }

    void Update()
    {
        if(StartTimerBloc)
        {
            remainingTimeBloc -= Time.deltaTime;
            int seconds = Mathf.FloorToInt(remainingTimeBloc % 60);

            if(seconds == 0f)
            {
                ControlActif = true;
            }
        }
        if(StartTimerEnd)
        {
            remainingTimeEnd -= Time.deltaTime;
            int seconds = Mathf.FloorToInt(remainingTimeEnd % 60);

            if(seconds == 0f)
            {
                UpdateTextContent("");
            }
        }
    }



    public void OnTriggerEnter(Collider other)
    {
        if(Box1 && BoxText)
        {
            if(other.GetComponent<Collider>().CompareTag("Player"))
            {
                UpdateTextContent("Bien joué ! Maintenant, approche-toi d'un objet et appuie sur E pour l'attraper. Garde-le en main, tu vas en avoir besoin !");
                Box1 = false;
            }
        }
        else if(Box2 && !Box1 && BoxText)
        {
            if(other.GetComponent<Collider>().CompareTag("Player"))
            {
                UpdateTextContent("Super ! Maintenant, essaye de lancer cet objet en appuyant sur F. La vitre juste devant toi est une cible parfaite pour tester tes talents de destruction !");
                Box2 = false;
            }
        }
        else if(Box2 && !Box1 && BoxText)
        {
            if(other.GetComponent<Collider>().CompareTag("Player"))
            {
                UpdateTextContent("Super ! Maintenant, essaye de lancer cet objet en appuyant sur F. La vitre juste devant toi est une cible parfaite pour tester tes talents de destruction !");
                Box2 = false;
            }
        }
        else if(Box3 && !Box2 && BoxText)
        {
            if(other.GetComponent <Collider>().CompareTag("Player"))
            {
                UpdateTextContent("Continue comme ca ! Prend l'objet que tu as utilisé pour casser la vitre et lance l'objet sur le bouton rouge pour allumer la lumière a ta gauche !");
                Box3 = false;
            }
        }
        else if(Box4 && !Box3 && BoxText)
        {
            if(other.GetComponent <Collider>().CompareTag("Player"))
            {
                UpdateTextContent("Derniere étape ! Prend le Pied De Biche, et fait Clic gauche sur la porte, tu trouveras d'autre choses a utiliser pour casser / mettre le feu au musée");
                Box4 = false;
            }
        }
        else if(Box5 && !Box4 && BoxText)
        {
            if(other.GetComponent <Collider>().CompareTag("Player"))
            {
                UpdateTextContent("Ne bouge pas ! Tu va rentré dans le musée, tu va avoir 10min pour casser le plus de choses, ce qui se passe dans le musée reste dans le musée et fait attention a ne pas ceder a la panique et ne pas sortir avant les 10min !");
                Box5 = false;
                StartTimerBloc = true;
                ControlActif = false;
            }
        }
    }



    //Update Text\\

    public void UpdateTextContent(string newText)
    {
        Text.text = newText;
    }


}
