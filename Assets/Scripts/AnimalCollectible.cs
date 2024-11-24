using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AnimalCollectible : MonoBehaviour
{
    public string animalName;       // Nombre del animal (Cerdo, Mono, etc.)
    [TextArea] public string fact;  // Dato educativo sobre el animal
    public GameObject dialogueUI;   // Panel del diálogo (Canvas)
    public Text dialogueText;       // Texto del diálogo

    private bool isPlayerInRange = false; // Para detectar si el jugador está cerca
    private static int collectedAnimals = 0; // Contador de animales recolectados
    public int totalAnimals = 5; // Total de animales a recolectar

    void Start()
    {
        dialogueUI.SetActive(false); // Ocultar el diálogo al inicio
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            CollectAnimal(); // Recolecta el animal al presionar "E"
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true; // Detecta que el jugador está cerca
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false; // El jugador salió del área
        }
    }

    void CollectAnimal()
    {
        // Muestra el dato educativo
        dialogueUI.SetActive(true);
        dialogueText.text = $"{animalName}: {fact}";

        // Desaparece el muñeco
        gameObject.SetActive(false);

        // Opcional: Oculta el diálogo después de unos segundos
        StartCoroutine(HideDialogueAfterSeconds(3f));

        if(++collectedAnimals >= totalAnimals){
            // LoadScene(); // Carga la siguiente escena
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    System.Collections.IEnumerator HideDialogueAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        dialogueUI.SetActive(false);
    }
}

// void LoadScene()
// {
//     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
// }
