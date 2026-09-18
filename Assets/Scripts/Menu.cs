using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Menu : MonoBehaviour
{
    Rigidbody2D _rb; // Riferimento al componente Rigidbody2D del giocatore
    public UIDocument uiDocument; // Riferimento al componente UIDocument per l'interfaccia utente
    private Button startButton; // Dichiarazione del pulsante di riavvio
    private Button quitbutton; // Dichiarazione del pulsante di uscita
    void Start() // Inizializzazione del gioco
    {
        _rb = GetComponent<Rigidbody2D>(); // Ottieni il componente Rigidbody2D del giocatore
        startButton = uiDocument.rootVisualElement.Q<Button>("StartButton");  
        startButton.clicked += LoadScene;
        quitbutton = uiDocument.rootVisualElement.Q<Button>("QuitButton"); // Ottieni il componente Button per l'uscita
        quitbutton.clicked += CloseApplication; // Aggiungi un listener per il click del pulsante di uscita
    }

    void Update() // Aggiornamento del gioco ad ogni frame
    {

    }

    private void CloseApplication() // Chiude l'applicazione quando il pulsante di uscita viene premuto
    {
#if UNITY_EDITOR // Se il gioco è in esecuzione nell'editor di Unity
        UnityEditor.EditorApplication.isPlaying = false; // Ferma l'esecuzione del gioco nell'editor
#else // Altrimenti, se il gioco è in esecuzione come build standalone
            Application.Quit(); // Chiude il gioco
#endif // ... è la fine della direttiva condizionale
    }

    private void LoadScene() // Carica la scena corrente quando il pulsante di avvio viene premuto
    {
        SceneManager.LoadScene("Game"); 
    }
}
