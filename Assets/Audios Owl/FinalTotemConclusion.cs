using UnityEngine;

public class FinalTotemConclusion : MonoBehaviour
{
    [Header("Painel final")]
    public GameObject finalPanel;

    [Header("Áudio final")]
    public AudioSource finalAudio;

    [Header("Configuração")]
    public bool onlyActivateOnce = true;

    private bool alreadyActivated = false;

    public void ShowConclusion()
    {
        Debug.Log("TOTEM FINAL INTERAGIDO");

        if (onlyActivateOnce && alreadyActivated)
            return;

        alreadyActivated = true;

        if (finalPanel != null)
        {
            finalPanel.SetActive(true);
            Debug.Log("PAINEL FINAL ATIVADO");
        }
        else
        {
            Debug.LogWarning("Final Panel está vazio no Inspector");
        }

        if (finalAudio != null)
        {
            finalAudio.Play();
            Debug.Log("ÁUDIO FINAL TOCANDO");
        }
        else
        {
            Debug.LogWarning("Final Audio está vazio no Inspector");
        }
    }
}