using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControle : MonoBehaviour
{
    public CenaJogo cenaAtual;
    public PainelControle painel;
    public SpriteInterruptor backgroundControle;
    public EscolhasControle escolhasControle;
    public AudioControle audioControle;
    public Pontos pontosControle;
    public EmpatiaControle empatiaControle;

    public PontosManager pontosManager;

    public DetentorDeDados dados;

    public Personagem jogador;

    public string menuTela;
    public string finalTela;

    public CenaJogo finalBom;
    public CenaJogo finalRuim;
    private bool finalizado;

    public Button pause;

    private Estado estado = Estado.PARADO;

    private List<CenaHistoria> historia =  new List<CenaHistoria>();

    private enum Estado
    {
        PARADO, ANIMANDO, ESCOLHENDO
    }

    void Start()
    {

        finalizado = false;

        if (GerenteDeSalvamento.EstaSalvado())
        {
            SalvarDados dados = GerenteDeSalvamento.CarregarJogo();
            dados.cenasPassadas.ForEach(cena =>
            {
                historia.Add(this.dados.cenas[cena] as CenaHistoria);
            });
            cenaAtual = historia[historia.Count - 1];
            historia.RemoveAt(historia.Count - 1);
            painel.SetSentencaIndex(dados.sentenca - 1);

            jogador.nome = dados.nomePersonagem;
        }

        if (cenaAtual is CenaHistoria)
        {
            CenaHistoria cenaHistoria = cenaAtual as CenaHistoria;            
            historia.Add(cenaHistoria);
            painel.IniciarCena(cenaHistoria, painel.GetSentencaIndex());
            backgroundControle.SetImagem(cenaHistoria.background);
            TocarAudio(cenaHistoria, cenaHistoria.sentencas[painel.GetSentencaIndex()]);
        }
    }

    void Update()
    {
        
        if (estado == Estado.PARADO) {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return))
            {
                if (painel.FoiCompleto())
                {
                    painel.PararEscrever();

                    if (painel.EhUltimaCena() && finalizado == false && painel.EhUltimaSentenca())
                    {
                        finalizado = true;
                        if (pontosControle.getEscolhasBoas() >= 3)
                        {
                            IniciarCena(finalBom);
                        } else
                        {
                            IniciarCena(finalRuim);
                        }
                        
                    } else if (painel.EhUltimaCena() && finalizado && painel.EhUltimaSentenca())
                    {
                        Debug.Log("Direcionando para o final do jogo!");
                        pontosManager.AtualizarScore(pontosControle.pontos, pontosControle.escolhasBoas, pontosControle.escolhasRuins);
                        SceneManager.LoadScene(finalTela);

                    } else if (painel.EhUltimaSentenca())
                    {
                        IniciarCena((cenaAtual as CenaHistoria).proxCena);
                    }
                    else
                    {
                        painel.IniciarProxSentenca();
                        TocarAudio((cenaAtual as CenaHistoria), (cenaAtual as CenaHistoria).sentencas[painel.GetSentencaIndex()]);
                    }
                }
                else
                {
                    painel.Correndo();
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (painel.EhPrimeiraSentenca())
                {
                    if (historia.Count > 1)
                    {
                        painel.PararEscrever();
                        painel.EsconderSprites();
                        historia.RemoveAt(historia.Count - 1);
                        CenaHistoria cena = historia[historia.Count - 1];
                        historia.RemoveAt(historia.Count - 1);
                        IniciarCena(cena, cena.sentencas.Count - 2, false);
                    }
                }
                else
                {
                    painel.Voltar();
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {

                 if (finalizado == false)
                 {
                    List<int> indiciesHistoria = new List<int>();
                    historia.ForEach(cena =>
                    {
                        indiciesHistoria.Add(this.dados.cenas.IndexOf(cena));
                    });

                    SalvarDados dados = new SalvarDados
                    {
                        sentenca = painel.GetSentencaIndex(),
                        cenasPassadas = indiciesHistoria,
                        nomePersonagem = jogador.nome
                    };

                    GerenteDeSalvamento.SalvarJogo(dados);
                    SceneManager.LoadScene(menuTela);
                 } else
                 {
                    Debug.Log("não tem como pausar no final do jogo!");
                 }
            }

        }

    }

    public void IniciarCena(CenaJogo cena, int sentencaIndex = -1, bool EhAnimado = true)
    {
        StartCoroutine(TrocarCena(cena, sentencaIndex, EhAnimado));
    }

    private IEnumerator TrocarCena(CenaJogo cena, int sentencaIndex = -1, bool EhAnimado = true)
    {

        pause.interactable = false;

        estado = Estado.ANIMANDO;
        cenaAtual = cena;

        if (EhAnimado)
        {
            painel.Esconder();
            yield return new WaitForSeconds(1f);
        }

        if (cena is CenaHistoria)
        {
            CenaHistoria cenaHistoria = cena as CenaHistoria;
            historia.Add(cenaHistoria);
            TocarAudio(cenaHistoria, cenaHistoria.sentencas[sentencaIndex + 1]);

            if (EhAnimado)
            {
                backgroundControle.TrocarImagem(cenaHistoria.background);
                yield return new WaitForSeconds(1f);
                painel.LimparTexto();
                painel.Mostrar();
                yield return new WaitForSeconds(1f);
            }
            else
            {
                backgroundControle.SetImagem(cenaHistoria.background);
                painel.LimparTexto();
            }

            painel.IniciarCena(cenaHistoria);
            pause.interactable = true;
            estado = Estado.PARADO;
        }
        else if (cena is CenaEscolhas)  
        {
            estado = Estado.ESCOLHENDO;
            escolhasControle.ConfigurarEscolha(cena as CenaEscolhas);
        }

        
    }

    private void TocarAudio(CenaHistoria cenaHistoria, CenaHistoria.Sentenca sentenca) {       
                                                    /* (CenaHistoria.Sentenca senteca) 
                                                     * em caso de uso do "music" dentro de "Sentenca"*/
        audioControle.TocarAudio(cenaHistoria.music, sentenca.fx);
    }

    public void Restartar()     // remover pos expotec
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Pausar()
    {
        
        if (finalizado == false)
        {
            List<int> indiciesHistoria = new List<int>();
            historia.ForEach(cena =>
            {
                indiciesHistoria.Add(this.dados.cenas.IndexOf(cena));
            });

            SalvarDados dados = new SalvarDados
            {
                sentenca = painel.GetSentencaIndex(),
                cenasPassadas = indiciesHistoria
            };

            GerenteDeSalvamento.SalvarJogo(dados);
            SceneManager.LoadScene(menuTela);
        } else
        {
            Debug.Log("Não tem como salvar no final do jogo!");
        }

    }

}
