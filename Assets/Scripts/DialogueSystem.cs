using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;


public class DialogueSystem : MonoBehaviour
{
    [System.Serializable]
    public class Dialogue
    {
        public string[] sentences; // Las líneas del diálogo
    }

    public Dialogue dialogue; // Asignar diálogos en el Inspector
    public TextMeshProUGUI dialogueText; // Referencia al texto del UI
    public GameObject dialoguePanel; // Panel que contiene el diálogo
    public float typingSpeed = 0.05f; // Velocidad de escritura

    private int currentSentenceIndex = 0;
    private bool isTyping = false;

    void Start()
    {
        dialoguePanel.SetActive(false); // Oculta el panel al inicio
    }

    public void StartDialogue()
    {
        dialoguePanel.SetActive(true);
        currentSentenceIndex = 0;
        StartCoroutine(TypeSentence(dialogue.sentences[currentSentenceIndex]));
    }

    public void NextSentence()
    {
        if (isTyping) return;

        currentSentenceIndex++;

        if (currentSentenceIndex < dialogue.sentences.Length)
        {
            StartCoroutine(TypeSentence(dialogue.sentences[currentSentenceIndex]));
        }
        else
        {
            EndDialogue();
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);
    }
}
