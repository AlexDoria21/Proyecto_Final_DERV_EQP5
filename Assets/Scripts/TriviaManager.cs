using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TriviaManager : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string questionText;          // Texto de la pregunta
        public string[] answers;            // Respuestas posibles
        public int correctAnswerIndex;      // Índice de la respuesta correcta
    }

    public Text questionText;               // Referencia al texto de la pregunta
    public Button[] answerButtons;          // Arreglo de botones para las respuestas
    public Question[] questions;            // Lista de preguntas

    private int currentQuestionIndex = 0;   // Índice de la pregunta actual
    private int score = 0;                  // Puntuación

    void Start()
    {
        DisplayQuestion(); // Muestra la primera pregunta
    }

    void DisplayQuestion()
    {
        if (currentQuestionIndex < questions.Length)
        {
            Question currentQuestion = questions[currentQuestionIndex];
            questionText.text = currentQuestion.questionText;

            for (int i = 0; i < answerButtons.Length; i++)
            {
                answerButtons[i].GetComponentInChildren<Text>().text = currentQuestion.answers[i];
                int answerIndex = i; // Necesario para capturar el índice en lambdas
                answerButtons[i].onClick.RemoveAllListeners();
                answerButtons[i].onClick.AddListener(() => CheckAnswer(answerIndex));
            }
        }
        else
        {
            EndTrivia(); // Si no hay más preguntas, termina la trivia
        }
    }

    void CheckAnswer(int selectedIndex)
    {
        if (selectedIndex == questions[currentQuestionIndex].correctAnswerIndex)
        {
            score++;
            Debug.Log("Respuesta correcta!");
        }
        else
        {
            Debug.Log("Respuesta incorrecta. Reiniciando preguntas...");
            RestartTrivia();
            return;
        }

        currentQuestionIndex++;
        DisplayQuestion();
    }

    void EndTrivia()
    {
        if (score == questions.Length)
        {
            Debug.Log("¡Ganaste la trivia! ¡Felicidades!");
            SceneManager.LoadScene("End_game");
        }
        else
        {
            Debug.Log("Trivia terminada, pero no acertaste todas las preguntas.");
            RestartTrivia();
        }
    }

    void RestartTrivia()
    {
        currentQuestionIndex = 0;
        score = 0;
        DisplayQuestion();
    }
}
