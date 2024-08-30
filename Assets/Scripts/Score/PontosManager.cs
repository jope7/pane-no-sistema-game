using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PontosManager : MonoBehaviour
{
    
    public int score;
    public int escBoas;
    public int escRuins;

    public Image gauger;
    public float posicaoGaugerX;

    public void AtualizarScore(int score, int escBoas, int escRuins)
    {
        GuardarScore(score, escBoas, escRuins);
        DontDestroyOnLoad(gameObject);
    }

    private void GuardarScore(int score, int escBoas, int escRuins)
    {
        this.score = score * 100;
        this.escBoas = escBoas;
        this.escRuins = escRuins;
        // guardando a posição X do gauger no jogo antes de ir para a tela final
        posicaoGaugerX = gauger.rectTransform.anchoredPosition.x;
    }
}
