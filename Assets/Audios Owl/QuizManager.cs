using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class QuizQuestion
{
    public string questionText;

    public string optionA;
    public string optionB;
    public string optionC;
    public string optionD;

    // 0 = A, 1 = B, 2 = C, 3 = D
    public int correctAnswerIndex;
}

public class QuizManager : MonoBehaviour
{
    [Header("Perguntas do Quiz")]
    public List<QuizQuestion> questions = new List<QuizQuestion>();

    [Header("Elementos da Interface")]
    public GameObject quizPanel;
    public GameObject resultPanel;

    public TMP_Text questionText;
    public TMP_Text progressText;
    public TMP_Text resultText;

    public Button optionAButton;
    public Button optionBButton;
    public Button optionCButton;
    public Button optionDButton;

    public TMP_Text optionAText;
    public TMP_Text optionBText;
    public TMP_Text optionCText;
    public TMP_Text optionDText;

    private int currentQuestionIndex = 0;
    private int score = 0;
    private bool quizStarted = false;

    private void Start()
    {
        quizPanel.SetActive(false);
        resultPanel.SetActive(false);

        optionAButton.onClick.AddListener(() => AnswerQuestion(0));
        optionBButton.onClick.AddListener(() => AnswerQuestion(1));
        optionCButton.onClick.AddListener(() => AnswerQuestion(2));
        optionDButton.onClick.AddListener(() => AnswerQuestion(3));
    }

    public void StartQuiz()
    {
        currentQuestionIndex = 0;
        score = 0;
        quizStarted = true;

        quizPanel.SetActive(true);
        resultPanel.SetActive(false);

        ShowQuestion();
    }

    private void ShowQuestion()
    {
        if (currentQuestionIndex >= questions.Count)
        {
            ShowResult();
            return;
        }

        QuizQuestion currentQuestion = questions[currentQuestionIndex];

        questionText.text = currentQuestion.questionText;

        optionAText.text = currentQuestion.optionA;
        optionBText.text = currentQuestion.optionB;
        optionCText.text = currentQuestion.optionC;
        optionDText.text = currentQuestion.optionD;

        progressText.text = "Pergunta " + (currentQuestionIndex + 1) + " de " + questions.Count;
    }

    private void AnswerQuestion(int selectedAnswerIndex)
    {
        if (!quizStarted)
            return;

        QuizQuestion currentQuestion = questions[currentQuestionIndex];

        if (selectedAnswerIndex == currentQuestion.correctAnswerIndex)
        {
            score++;
        }

        currentQuestionIndex++;
        ShowQuestion();
    }

    private void ShowResult()
    {
        quizPanel.SetActive(false);
        resultPanel.SetActive(true);

        quizStarted = false;

        resultText.text = "Você acertou " + score + " de " + questions.Count + " perguntas.";
    }

    public void CloseQuiz()
    {
        quizPanel.SetActive(false);
        resultPanel.SetActive(false);
        quizStarted = false;
    }
}