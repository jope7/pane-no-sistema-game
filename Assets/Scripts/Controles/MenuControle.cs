using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class MenuControle : MonoBehaviour
{

    public string cenaCarregada;
    public string cenaPassada;

    // caminho do arquivo de configuracao
    public string caminhoArqConfig = "config.txt";

    public TextMeshProUGUI musicaPorcentagem;
    public AudioMixer musicaMixer;
    public TextMeshProUGUI sonsPorcentagem;
    public AudioMixer sonsMixer;

    public Button carregarBtn;
    
    private Animator animator;
    private int _window = 0;

    private void Start()
    {
        if (!File.Exists(caminhoArqConfig))
        {
            GerenteDeSalvamento.LimparJogoSalvo();
            File.Create(caminhoArqConfig).Dispose();
        }

        animator = GetComponent<Animator>();
        carregarBtn.interactable = GerenteDeSalvamento.EstaSalvado();
        SceneManager.UnloadSceneAsync(cenaPassada);
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && _window == 1)
        {
            animator.SetTrigger("EsconderOpcoes");
            _window = 0;
        }
    }

    public void NovoJogo()
    {
        GerenteDeSalvamento.LimparJogoSalvo();
        Carregar();
    }

    public void Carregar()
    {
        SceneManager.LoadScene(cenaCarregada, LoadSceneMode.Additive);
    }

    public void MostrarOpcoes()
    {
        animator.SetTrigger("MostrarOpcoes");
        _window = 1;
    }

    public void Sair()
    {
        Application.Quit();
    }

    public void NaMudancaMusica(float valor)
    {
        musicaPorcentagem.SetText(valor + "%");
        musicaMixer.SetFloat("volume", -50 + valor / 2);
    }

    public void NaMudancaSons(float valor)
    {
        sonsPorcentagem.SetText(valor + "%");
        sonsMixer.SetFloat("volume", -50 + valor / 2);
    }

}
