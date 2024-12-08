using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CreepyDollTrigger : MonoBehaviour
{
    public AudioSource creepySound; // Asigna el AudioSource del muñeco
    public GameObject dialogueUI;  // El panel del sistema de diálogo (Canvas)
    public Text dialogueText;      // El texto dentro del panel
    public string dialogueMessage = "¿Por qué este muñeco está mirando fijamente?";

    private bool hasTriggered = false;   // Controla si el trigger ya se activó
    private bool isPlayerInRange = false; // Para detectar si el jugador está cerca

    void Start()
    {
        dialogueUI.SetActive(false); // Asegúrate de que el diálogo esté oculto al inicio
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            StopCreepySound(); // Detén el sonido y oculta el diálogo
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true; // Marca el trigger como activado
            isPlayerInRange = true;
            PlayCreepySound(); // Reproduce el sonido y muestra el diálogo
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    void PlayCreepySound()
    {
        creepySound.Play();

        // Activa el diálogo y muestra el mensaje
        dialogueUI.SetActive(true);
        dialogueText.text = dialogueMessage;
    }

    void StopCreepySound()
    {
        creepySound.Stop();

        // Oculta el diálogo
        dialogueUI.SetActive(false);
    }
}
