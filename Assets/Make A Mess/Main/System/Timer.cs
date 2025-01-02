using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] float remainingTime;
    [SerializeField] private TextMeshProUGUI Text;
    [SerializeField] public AudioSource Notif;
    [SerializeField] public AudioSource soundTimer;
    [SerializeField] Tuto_Text tutoText;
    public bool StartTimer;
    
    public void Start()
    {
        Text.text = "";
    }
    
    void Update()
    {
        if(StartTimer)
        {
            remainingTime -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            int centiseconds = Mathf.FloorToInt((remainingTime - Mathf.Floor(remainingTime)) * 100);
            timer.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, centiseconds);
    
            if(minutes == 9 && seconds == 30)
            {
                UpdateTextContent("C’est toi ?! T’as vraiment osé revenir dans MON musée après avoir été viré comme une merde ? Tu crois que tu vas réparer ton ego en détruisant ce que je possède ? Tu étais inutile alors, et tu l’es toujours. Prends tes affaires et dégage avant que je ne te traîne devant les tribunaux.");
                Notif.Play();
            }
            if(minutes == 9 && seconds == 0)
            {
                UpdateTextContent("");
            }
            if(minutes == 8 && seconds == 0)
            {
                UpdateTextContent("Revenir ici… Après ce que j’ai fait pour toi ! Je t’ai donné une chance, et tu l’as gaspillée, alors je t’ai jeté. Comme je jette tous les parasites inutiles. Et maintenant, tu te crois capable de me défier ? Ça ne fera qu’ajouter une autre défaite à ta liste.");
                Notif.Play();
            }
            if(minutes == 7 && seconds == 30)
            {
                UpdateTextContent("");
            }
            if(minutes == 6 && seconds == 0)
            {
                UpdateTextContent("Tu continues encore ? Franchement, je suis impressionné par ta bêtise. Mais ce n’est pas une surprise. Rappelle-toi pourquoi je t’ai viré : tu es un incapable. Ce musée est au-dessus de toi, tout comme je le suis. Continue, et tu verras ce que ça coûte de s’attaquer à quelqu’un comme moi.");
                Notif.Play();
            }
            if(minutes == 5 && seconds == 30)
            {
                UpdateTextContent("");
            }
            if(minutes == 3 && seconds == 0)
            {
                UpdateTextContent("Bien. Tu voulais de l’attention ? Tu l’as. J’ai appelé les flics, et ils seront là dans 3 minutes. Rappelle-toi de cette sensation : être complètement dépassé. Ça t’était déjà familier quand je t’ai viré, non ? Mais cette fois, ce ne sera pas un carton de tes affaires qu’on emportera, ce sera toi, menotté.");
                Notif.Play();
            }
            if(minutes == 2 && seconds == 30)
            {
                UpdateTextContent(""); 
            }
            if(minutes == 1 && seconds == 0)
            {
                UpdateTextContent("Il te reste 1 minute. Une minute pour éviter d’ajouter 'criminel raté' à ton CV déjà pathétique. Mais non, tu vas rester, n’est-ce pas ? Parce que tu es trop stupide pour comprendre quand tu as perdu. Rappelle-toi juste : tout ce que tu fais ici aujourd’hui, c’est confirmer que j’ai eu raison de te virer.");
                Notif.Play();
            }
        }
    }

    public void StartingTimer()
    {
        StartTimer = true;
        soundTimer.Play();
        tutoText.StartTimerEnd = true;
    }

    public void UpdateTextContent(string newText)
    {
        Text.text = newText;
    }
}