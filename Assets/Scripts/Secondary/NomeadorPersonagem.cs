using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class NomeadorPersonagem : MonoBehaviour
{
    public TMP_InputField DigitarNome;
    public TextMeshProUGUI erro;
    public Personagem jogador;
    public string proxCena;

    private void Start()
    {
        erro.gameObject.SetActive(false);
        erro.text = "Erro, campo está vazio ou passando de 20 caracteres!";
    }

    public void Continuar()
    {
        if (!string.IsNullOrEmpty(DigitarNome.text) && DigitarNome.text.Length < 20)
        {
            jogador.nome = NomearPersonagem(DigitarNome.text);
            Prosseguir();
        }
        else
        {
            MostrarErro();
        }
    }

    void Prosseguir()
    {
        SceneManager.LoadScene(proxCena, LoadSceneMode.Additive);
    }

    public string NomearPersonagem(string nome)
    {
        return (char.ToUpper(nome[0]) + nome[1..]);
    }

    public void MostrarErro()
    {
        StartCoroutine(ErroMostrado());
    }

    private IEnumerator ErroMostrado()
    {
        // Exibe o texto
        erro.gameObject.SetActive(true);
        
        // Aguarda por alguns segundos
        yield return new WaitForSeconds(3f);
        
        // Oculta o texto
        erro.gameObject.SetActive(false);
    }

}
