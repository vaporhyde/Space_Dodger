using System; // Importa il namespace System per l'utilizzo di classi e metodi di base
using UnityEngine; // Importa il namespace UnityEngine per l'utilizzo delle funzionalità di Unity
using UnityEngine.InputSystem; // Importa il namespace UnityEngine.InputSystem per l'utilizzo del nuovo sistema di input di Unity
using UnityEngine.SceneManagement; // Importa il namespace UnityEngine.SceneManagement per la gestione delle scene
using UnityEngine.UIElements; // Importa il namespace UnityEngine.UIElements per l'utilizzo dell'interfaccia utente basata su UI Toolkit

public class PlayerController : MonoBehaviour // Definisce la classe PlayerController che eredita da MonoBehaviour, consentendo di essere attaccata a un GameObject in Unity
{
    public float thrustForce = 1f; // Forza di spinta del giocatore
    public float maxSpeed = 5f; // Velocità massima del giocatore
    Rigidbody2D _rb; // Riferimento al componente Rigidbody2D del giocatore
    public GameObject boosterFlame; // Riferimento all'oggetto della fiamma del booster
    public UIDocument uiDocument; // Riferimento al componente UIDocument per l'interfaccia utente
    public GameObject explosionEffect; // Riferimento all'effetto di esplosione
    public GameObject borderParent; // Riferimento al genitore dei bordi del gioco
    private Button restartButton; // Dichiarazione del pulsante di riavvio
    private Button quitbutton; // Dichiarazione del pulsante di uscita
    public bool IsBoosting { get; private set; } // Proprietà per verificare se il giocatore sta utilizzando il booster

    void Start() // Inizializzazione del gioco
    {
        _rb = GetComponent<Rigidbody2D>(); // Ottieni il componente Rigidbody2D del giocatore
        restartButton = uiDocument.rootVisualElement.Q<Button>("RestartButton"); // Ottieni il componente Button per il riavvio
        restartButton.style.display = DisplayStyle.None; // Nascondi il pulsante di riavvio all'inizio del gioco
        restartButton.clicked += ReloadScene; // Aggiungi un listener per il click del pulsante di riavvio
        quitbutton = uiDocument.rootVisualElement.Q<Button>("QuitButton"); // Ottieni il componente Button per l'uscita
        quitbutton.clicked += CloseApplication; // Aggiungi un listener per il click del pulsante di uscita
    }

    void Update() // Aggiornamento del gioco ad ogni frame
    {
        BoosterFlame(); // Aggiorna la fiamma del booster in base all'input del mouse
        MovePlayer(); // Muovi il giocatore in base all'input del mouse
    }

    private void CloseApplication() // Chiude l'applicazione quando il pulsante di uscita viene premuto
    {
#if UNITY_EDITOR // Se il gioco è in esecuzione nell'editor di Unity
        UnityEditor.EditorApplication.isPlaying = false; // Ferma l'esecuzione del gioco nell'editor
#else // Altrimenti, se il gioco è in esecuzione come build standalone
            Application.Quit(); // Chiude il gioco
#endif // ... è la fine della direttiva condizionale
    }

    void BoosterFlame() // Aggiorna la visibilità della fiamma del booster in base all'input del mouse
    {
        IsBoosting = Mouse.current.leftButton.isPressed;
        if (Mouse.current.leftButton.isPressed) // Se il pulsante sinistro del mouse è premuto
        {
            boosterFlame.SetActive(true); // Mostra la fiamma del booster

        }
        else // Altrimenti
        {
            boosterFlame.SetActive(false); // Nascondi la fiamma del booster
        }
    }

    void MovePlayer() // Muovi il giocatore in base all'input del mouse
    {
        if (Mouse.current.leftButton.isPressed) // Se il pulsante sinistro del mouse è premuto
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value); // Ottieni la posizione del mouse in coordinate del mondo
            Vector2 direction = (mousePos - transform.position).normalized; // Calcola la direzione del movimento del giocatore verso il mouse

            transform.up = direction; // Ruota il giocatore verso la direzione del mouse
            _rb.AddForce(direction * thrustForce); // Applica una forza al giocatore nella direzione del mouse
            if (_rb.linearVelocity.magnitude > maxSpeed) // Se la velocità del giocatore supera la velocità massima
            {
                _rb.linearVelocity = _rb.linearVelocity.normalized * maxSpeed; // Limita la velocità del giocatore alla velocità massima
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) // Gestisce la collisione del giocatore con altri oggetti
    {
        Instantiate(explosionEffect, transform.position, transform.rotation); // Istanzia l'effetto di esplosione nella posizione del giocatore
        Destroy(gameObject); // Distruggi il giocatore
        restartButton.style.display = DisplayStyle.Flex; // Mostra il pulsante di riavvio
        borderParent.SetActive(false); // Nascondi i bordi del gioco
    }

        void ReloadScene() // Ricarica la scena corrente quando il pulsante di riavvio viene premuto
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Ricarica la scena corrente
    }
}
