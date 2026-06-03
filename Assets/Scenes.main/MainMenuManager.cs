using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
    [Header("Nome da cena principal")]
    public string nomeCenaPrincipal = "CenaPrincipal";

    [Header("Música de fundo do menu")]
    public AudioSource musicaMenu;

    [Header("Som ao clicar em Iniciar")]
    public AudioSource audioBotao;
    public AudioClip somIniciar;

    [Header("Configuração")]
    public bool esperarSomAntesDeCarregar = true;

    private bool jaClicouIniciar = false;

    private void Start()
    {
        if (musicaMenu != null)
        {
            musicaMenu.loop = true;
            musicaMenu.Play();
        }
        else
        {
            Debug.LogWarning("MusicaMenu não foi atribuída no Inspector.");
        }
    }

    public void IniciarExperiencia()
    {
        if (jaClicouIniciar)
            return;

        jaClicouIniciar = true;

        StartCoroutine(TocarSomECarregarCena());
    }

    private IEnumerator TocarSomECarregarCena()
    {
        if (audioBotao != null && somIniciar != null)
        {
            audioBotao.PlayOneShot(somIniciar);

            if (esperarSomAntesDeCarregar)
            {
                yield return new WaitForSeconds(somIniciar.length);
            }
        }
        else
        {
            Debug.LogWarning("AudioBotao ou SomIniciar não foram atribuídos no Inspector.");
        }

        SceneManager.LoadScene(nomeCenaPrincipal);
    }

    public void Sair()
    {
        Debug.Log("Saindo da aplicação...");

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}