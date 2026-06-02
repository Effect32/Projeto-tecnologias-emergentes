using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class TotemAudioPlayer : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private XRSimpleInteractable interactable;
    [SerializeField] private TotemObjectiveManager objectiveManager;

    [Header("Configurações")]
    [SerializeField] private bool restartIfAlreadyPlaying = false;

    private bool hasBeenActivated = false;

    private void Awake()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        if (interactable == null)
            interactable = GetComponent<XRSimpleInteractable>();
    }

    private void OnEnable()
    {
        if (interactable != null)
            interactable.selectEntered.AddListener(OnTotemSelected);
    }

    private void OnDisable()
    {
        if (interactable != null)
            interactable.selectEntered.RemoveListener(OnTotemSelected);
    }

    private void OnTotemSelected(SelectEnterEventArgs args)
    {
        PlayAudio();

        if (!hasBeenActivated)
        {
            hasBeenActivated = true;

            if (objectiveManager != null)
            {
                objectiveManager.RegisterTotemActivated();
            }
        }
    }

    private void PlayAudio()
    {
        if (audioSource == null)
            return;

        if (audioSource.isPlaying)
        {
            if (restartIfAlreadyPlaying)
            {
                audioSource.Stop();
                audioSource.Play();
            }

            return;
        }

        audioSource.Play();
    }
}