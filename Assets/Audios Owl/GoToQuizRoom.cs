using UnityEngine;

public class GoToQuizRoom : MonoBehaviour
{
    [Header("Referências")]
    public Transform xrRig;
    public Transform quizSpawnPoint;
    public QuizManager quizManager;

    [Header("Controle de movimento")]
    public MonoBehaviour[] movementScriptsToDisable;

    public void EnterQuizRoom()
    {
        // Move o jogador para a sala do quiz
        xrRig.position = quizSpawnPoint.position;
        xrRig.rotation = quizSpawnPoint.rotation;

        // Desativa scripts de movimento, se você quiser impedir que o usuário ande
        foreach (MonoBehaviour script in movementScriptsToDisable)
        {
            if (script != null)
                script.enabled = false;
        }

        // Inicia o quiz
        quizManager.StartQuiz();
    }
}