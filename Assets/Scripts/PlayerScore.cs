using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.UIElements;
using Label = UnityEngine.UIElements.Label;

public class PlayerScore : MonoBehaviour
{
    private float elapsedTime = 0f; // Tempo trascorso dall'inizio del gioco
    public float score = 0f; // Punteggio del giocatore
    public float scoreMultiplier = 1f; // Moltiplicatore per il punteggio
    public UIDocument UIDocument; // Riferimento al componente UIDocument per l'interfaccia utente
    private Label scoreText; // Riferimento al componente Label per il punteggio


    void Start()
    {
        scoreText = UIDocument.rootVisualElement.Q<Label>("ScoreLabel"); // Ottieni il componente Label per il punteggio>
    }

    void Update()
    {
        UpdateScore(); // Aggiorna il punteggio del giocatore in base al tempo trascorso
    }

    void UpdateScore() // Aggiorna il punteggio del giocatore in base al tempo trascorso
    {
        elapsedTime += Time.deltaTime; // Incrementa il tempo trascorso
        score = Mathf.FloorToInt(elapsedTime * scoreMultiplier); // Calcola il punteggio in base al tempo trascorso e al moltiplicatore
        scoreText.text = "Score: " + score; // Aggiorna il testo del punteggio nell'interfaccia utente
    }
}
