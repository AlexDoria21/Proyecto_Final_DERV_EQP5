using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CreepyDollDisappear : MonoBehaviour
{
    public AudioSource creepySound; // Sonido creepy asignado al muñeco
    public GameObject dollObject;  // El muñeco que desaparecerá
    public GameObject dialogueUI;  // El panel del sistema de diálogo (Canvas)
    public Text dialogueText;      // El texto dentro del panel
    public string dialogueMessage = "¡Qué carajos, ese muñeco de nuevo!";

    private bool hasTriggered = false; // Controla si el evento ya fue activado

    void Start()
    {
        dialogueUI.SetActive(false); // Asegúrate de que el diálogo esté oculto al inicio
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true; // Marca el evento como activado
            StartCoroutine(HandleCreepyDollEvent());
        }
    }

    IEnumerator HandleCreepyDollEvent()
    {
        creepySound.Play(); // Reproduce el sonido creepy
        ShowDialogue();

        yield return new WaitForSeconds(creepySound.clip.length); // Espera a que termine el sonido

        HideDialogue();
        HideDoll(); // Luego, desaparece el muñeco
    }

    void ShowDialogue()
    {
        dialogueUI.SetActive(true); // Activa el panel de diálogo
        dialogueText.text = dialogueMessage; // Muestra el mensaje
    }

    void HideDialogue()
    {
        dialogueUI.SetActive(false); // Oculta el panel de diálogo
    }

    void HideDoll()
    {
        dollObject.SetActive(false); // Desactiva el muñeco
    }
}
