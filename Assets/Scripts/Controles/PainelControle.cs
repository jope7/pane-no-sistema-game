using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PainelControle : MonoBehaviour
{

    public TextMeshProUGUI barraTexto;
    public TextMeshProUGUI nomePersonagem;

    public Personagem jogador;
    private string nome;
    // apenas para fazer a substituição do nome player
    // pelo nome que o jogador escolher no começo do jogo

    private CenaHistoria cenaAtual;
    private int sentencaIndex = -1;
    private Estado estado = Estado.CONCLUIDA;
    private Animator animator;
    private bool estaEscondido = false;

    public Dictionary<Personagem, SpriteControle> sprites;
    public GameObject spritesPrefab;

    private Coroutine escrevendoCoroutine;
    private float fatorVelocidade = 1f;

    private enum Estado
    {
        FALANDO, CORRENDO_TEXTO, CONCLUIDA
    }

    private void Start()
    {
        nome = jogador.nome;
        sprites = new Dictionary<Personagem, SpriteControle>();
        animator = GetComponent<Animator>();
    }

    public int GetSentencaIndex()
    {
        return sentencaIndex;
    }

    public void SetSentencaIndex(int sentencaIndex)
    {
        this.sentencaIndex = sentencaIndex;
    }

    public void Esconder()
    {
        if (!estaEscondido)
        {
            animator.SetTrigger("Esconder");
            estaEscondido = true;
        }
    }

    public void Mostrar()
    {
        animator.SetTrigger("Mostrar");
        estaEscondido = false;
    }

    public void LimparTexto()
    {
        nomePersonagem.text = "";
        barraTexto.text = "";
    }

    public void IniciarCena(CenaHistoria cena, int sentenceIndex = -1)
    {
        cenaAtual = cena;
        sentencaIndex = -1;
        IniciarProxSentenca();
    }

    public void IniciarProxSentenca(bool ehAnimado = true)
    {
        sentencaIndex++;
        IniciarSentenca(ehAnimado);
    }

    public void Voltar()
    {
        sentencaIndex--;
        PararEscrever();
        EsconderSprites();
        IniciarSentenca(false);
    }


    public bool FoiCompleto()
    {
        return estado == Estado.CONCLUIDA || estado == Estado.CORRENDO_TEXTO;
    }

    public bool EhUltimaSentenca()
    {
        return sentencaIndex + 1 == cenaAtual.sentencas.Count;
    }

    public bool EhUltimaCena()
    {
        return cenaAtual.proxCena == null;
    }

    public bool EhPrimeiraSentenca()
    {
        return sentencaIndex == 0;
    }

    public void Correndo()
    {
        estado = Estado.CORRENDO_TEXTO;
        fatorVelocidade = 0.25f;
    }

    public void PararEscrever()
    {
        estado = Estado.CONCLUIDA;
        StopCoroutine(escrevendoCoroutine);
    }

    public void EsconderSprites()
    {
        while (spritesPrefab.transform.childCount > 0)
        {
            DestroyImmediate(spritesPrefab.transform.GetChild(0).gameObject);
        }

        sprites.Clear();

    }

    private void IniciarSentenca(bool ehAnimado = true)
    {
        fatorVelocidade = 1f;
        escrevendoCoroutine = StartCoroutine(EscrevendoTexto(ReplaceName(cenaAtual.sentencas[sentencaIndex].texto)));
        nomePersonagem.text = cenaAtual.sentencas[sentencaIndex].personagem.nome;
        nomePersonagem.color = cenaAtual.sentencas[sentencaIndex].personagem.corNome;
        EhNarrador();
        AgirPersonagens(ehAnimado);
    }

    private IEnumerator EscrevendoTexto(string texto)
    {
        barraTexto.text = "";
        estado = Estado.FALANDO;
        int palavraIndex = 0;

        while (estado != Estado.CONCLUIDA)
        {
            barraTexto.text += texto[palavraIndex];
            yield return new WaitForSeconds(fatorVelocidade * 0.04f);
            if (++palavraIndex == texto.Length)
            {
                estado = Estado.CONCLUIDA;
                break;
            }
        }

    }

    private void AgirPersonagens(bool ehAnimado = true)
    {
        List<CenaHistoria.Sentenca.Acao> acoes = cenaAtual.sentencas[sentencaIndex].acoes;

        for(int i = 0; i < acoes.Count; i++)
        {
            AgirPersonagem(acoes[i], ehAnimado);
        }

    }

    private void AgirPersonagem(CenaHistoria.Sentenca.Acao acoes, bool ehAnimado = true)
    {
        SpriteControle controle;

        if (!sprites.ContainsKey(acoes.personagem))
        {
            controle = Instantiate(acoes.personagem.prefab.gameObject, spritesPrefab.transform)
                .GetComponent<SpriteControle>();
            sprites.Add(acoes.personagem, controle);
        }
        else
        {
            controle = sprites[acoes.personagem];
        }

        switch (acoes.tipoAcao)
        {
            case CenaHistoria.Sentenca.Acao.Tipo.APARECENDO:
                controle.Iniciar(acoes.personagem.sprites[acoes.spriteIndex]);
                controle.Mostrar(acoes.posicao, ehAnimado);
                return;
            case CenaHistoria.Sentenca.Acao.Tipo.DESAPARECENDO:
                controle.Esconder(ehAnimado);
                break;
        }

        controle.TrocarSprite(acoes.personagem.sprites[acoes.spriteIndex], ehAnimado);
        
    }

    private void EhNarrador()
    {
        if (nomePersonagem.text == "Narrador")
        {
            nomePersonagem.fontStyle = FontStyles.Italic;
            barraTexto.fontStyle = FontStyles.Italic;
        }
        else
        {
            nomePersonagem.fontStyle = FontStyles.Bold;
            barraTexto.fontStyle = FontStyles.Normal;
        }
    }

    private string ReplaceName(string text)
    {
        if (text.Contains("Player") || text.Contains("player"))
        {
            // Debug.LogError("Senteça possui \"Player\"");
            return text.Replace("Player", nome);
        } else
        {
            // Debug.LogError("Senteça não possui \"Player\"");
            return text;
        }
    }

}
