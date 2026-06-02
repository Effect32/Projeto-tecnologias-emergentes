using UnityEngine;
using TMPro;

public class TotemObjectiveManager : MonoBehaviour
{
    [Header("Configuração do Objetivo")]
    [SerializeField] private int totalTotens = 4;

    [Header("HUD")]
    [SerializeField] private TMP_Text objectiveText;

    [Header("Totem liberado ao completar")]
    [SerializeField] private GameObject finalTotemPlaceholder;

    [Header("Som de conclusão")]
    [SerializeField] private AudioSource completionAudioSource;

    private int activatedTotems = 0;
    private bool objectiveCompleted = false;

    private void Start()
    {
        if (finalTotemPlaceholder != null)
            finalTotemPlaceholder.SetActive(false);

        UpdateHUD();
    }

    public void RegisterTotemActivated()
    {
        if (objectiveCompleted)
            return;

        activatedTotems++;

        if (activatedTotems > totalTotens)
            activatedTotems = totalTotens;

        UpdateHUD();

        if (activatedTotems >= totalTotens)
        {
            CompleteObjective();
        }
    }

    private void UpdateHUD()
    {
        if (objectiveText != null)
        {
            objectiveText.text =
                "<b>OBJETIVO ATUAL</b>\n" +
                "Interaja com os Totens\n" +
                "Progresso: " + activatedTotems + "/" + totalTotens;
        }
    }

    private void CompleteObjective()
    {
        objectiveCompleted = true;

        if (objectiveText != null)
        {
            objectiveText.text =
                "<b>OBJETIVO COMPLETO</b>\n" +
                "Todos os Totens foram visitados\n" +
                "Novo ponto liberado";
        }

        if (finalTotemPlaceholder != null)
        {
            finalTotemPlaceholder.SetActive(true);
        }

        if (completionAudioSource != null)
        {
            completionAudioSource.Play();
        }

        Debug.Log("Objetivo completo! Totem final liberado.");
    }
}