using UnityEngine; // Importa il namespace UnityEngine per l'utilizzo delle funzionalità di Unity

public class Obstacle : MonoBehaviour // Definisce la classe Obstacle che eredita da MonoBehaviour, consentendo di essere attaccata a un GameObject in Unity
{
    public float minSize = 0.5f; // Dimensione minima dell'ostacolo
    public float maxSize = 2.0f; // Dimensione massima dell'ostacolo
    public float minSpeed = 50f; // Velocità minima dell'ostacolo
    public float maxSpeed = 150f; // Velocità massima dell'ostacolo
    public float maxSpinSpeed = 10f; // Velocità massima di rotazione dell'ostacolo
    public GameObject bounceEffectPrefab; // Prefab dell'effetto di rimbalzo da istanziare alla collisione

    Rigidbody2D _rb; // Riferimento al componente Rigidbody2D dell'ostacolo

    void Start() // Inizializzazione dell'ostacolo
    {
        float randomSize = Random.Range(minSize, maxSize); // Genera una dimensione casuale per l'ostacolo
        transform.localScale = new Vector3(randomSize, randomSize, 1); // Imposta la scala dell'ostacolo in base alla dimensione casuale generata

        _rb = GetComponent<Rigidbody2D>(); // Ottieni il componente Rigidbody2D dell'ostacolo
        float randomSpeed = Random.Range(minSpeed, maxSpeed) / randomSize; // Genera una velocità casuale per l'ostacolo in base alla dimensione casuale generata

        Vector2 randomDirection = Random.insideUnitCircle; // Genera una direzione casuale per l'ostacolo
        _rb.AddForce(randomDirection * randomSpeed); // Applica una forza all'ostacolo nella direzione casuale generata con la velocità casuale generata

        float randomTorque = Random.Range(-maxSpinSpeed, maxSpinSpeed); // Genera una velocità di rotazione casuale per l'ostacolo
        _rb.AddTorque(randomTorque); // Applica una coppia all'ostacolo con la velocità di rotazione casuale generata
    }

    void Update() // Aggiornamento dell'ostacolo ad ogni frame
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 contactPoint = collision.GetContact(0).point; // Ottieni il punto di contatto della collisione
        GameObject bounceEffect = Instantiate(bounceEffectPrefab, contactPoint, Quaternion.identity); // Istanzia l'effetto di rimbalzo alla posizione del punto di contatto della collisione

        Destroy(bounceEffect, 1f); // Distruggi l'effetto di rimbalzo dopo 1 secondo
    }

}
