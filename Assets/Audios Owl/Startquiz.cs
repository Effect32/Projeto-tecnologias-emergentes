using UnityEngine;

public class FinalTotemInteraction : MonoBehaviour
{
    public QuizManager quizManager;

    public void Interact()
    {
        quizManager.StartQuiz();
    }
}