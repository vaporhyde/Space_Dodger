using UnityEngine;

public class Icon : MonoBehaviour
{
    public GameObject boosterFlameIcon; // Riferimento all'oggetto della fiamma del booster
    public PlayerController playerController; // Riferimento al componente PlayerController del giocatore

    // Update is called once per frame
    void Update() // Aggiorna la visibilità dell'icona della fiamma del booster in base allo stato del giocatore
    {
        if (boosterFlameIcon == null || playerController == null) //    
            return; // Se l'oggetto della fiamma del booster o il componente PlayerController non sono assegnati, esci dalla funzione

        boosterFlameIcon.SetActive(playerController.IsBoosting); // Imposta la visibilità dell'icona della fiamma del booster in base allo stato del giocatore
    }
}
