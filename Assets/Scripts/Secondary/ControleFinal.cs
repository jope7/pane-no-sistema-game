using TMPro; 
using UnityEngine;
using UnityEngine.UI;

public class ControleFinal : MonoBehaviour
{
    // escolhas boas, escolhas ruins e score
    private int eb, er, s;

    [Header("TMPro escolhas boas, escolhas ruins e score")]
    public TextMeshProUGUI escBonsTxt;
    public TextMeshProUGUI escRuinsTxt;
    public TextMeshProUGUI scoreTxt;

    [Header("TMPro texto falando das escolhas feitas")]
    public TextMeshProUGUI escolhasTexto;

    [Header("TMPro texto de lição")]
    public TextMeshProUGUI licao;

    [Header("Imagem do Gauger")]
    public Image gauger;

    private void Start()
    {
        PontosManager pontos = (PontosManager)FindAnyObjectByType(typeof(PontosManager));
        AtualizarScore(pontos);
        Vector2 posicao = new Vector2(pontos.posicaoGaugerX, gauger.rectTransform.anchoredPosition.y);
        gauger.rectTransform.anchoredPosition = posicao;
        Atualizar();
        
        string texto = "Você teve EB escolhas boas e ER escolhas ruins.";
        escolhasTexto.text = AtualizarTexto(texto);
        licao.text = LicaoFinal();
    }

    public void AtualizarScore(PontosManager pontos)
    {
        eb = pontos.escBoas;
        er = pontos.escRuins;
        s = pontos.score;
    }

    public void Atualizar()
    {
        escBonsTxt.text = eb.ToString();
        escRuinsTxt.text = er.ToString();
        scoreTxt.text = s.ToString();
    }

    public string AtualizarTexto(string str)
    {
        str = str.Replace("EB", eb.ToString());
        str = str.Replace("ER", er.ToString());
        return str;
    }

    public string LicaoFinal()
    {
        if (eb >= 3)
        {
            return "Você pode não ter feito as melhores escolhas, mas teve o melhor final possivel!";
        } 
        else
        {
            return licao.text;
        }
    }

    public void Sair()
    {
        Application.Quit();
    }

}
