using TMPro;
using UnityEngine;

public class Pontos : MonoBehaviour
{

    public int pontos;
    public int escolhasBoas;
    public int escolhasRuins;

    public TextMeshProUGUI score;
    public TextMeshProUGUI escRuinsTxt;
    public TextMeshProUGUI escBoasTxt;

    void Start()
    {
        pontos = 0;
        escolhasBoas = 0; 
        escolhasRuins = 0;
    }

    void Update()
    {
        score.text = pontos.ToString();
        escBoasTxt.text = escolhasBoas.ToString();
        escRuinsTxt.text = escolhasRuins.ToString();
    }

    public void AddPontos(int p)
    {
        pontos += p;

        if (p > 0)
        {
            escolhasBoas++;
        } else if (p < 0)
        {
            escolhasRuins++;
        } 
    }

    public int getPontos()
    {
        return pontos;
    }

    public int getEscolhasBoas()
    {
        return escolhasBoas;
    }

    public int getEscolhasRuins()
    {
        return escolhasRuins;
    }

}
