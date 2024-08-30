using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EscolhasControle : MonoBehaviour
{

    public EscolhasTagControle escolhasTag;
    public GameControle gameControle;
    private RectTransform rectTransform;
    private Animator animator;

    private float tagAltura = -1;


    void Start()
    {
        animator = GetComponent<Animator>();
        rectTransform = GetComponent<RectTransform>();
    }
    
    public void ConfigurarEscolha(CenaEscolhas cena)
    {
        DestruirTags();
        animator.SetTrigger("Mostrar");

        for (int i = 0; i < cena.tags.Count; i++)
        {
            EscolhasTagControle novaTag = Instantiate(escolhasTag.gameObject, transform).GetComponent<EscolhasTagControle>();

            if (tagAltura == -1)
            {
                tagAltura = novaTag.GetAltura();
            }

            novaTag.Configurar(cena.tags[i], this, CalcularPosicaoTag(i, cena.tags.Count));
        }

        Vector2 tamanho = rectTransform.sizeDelta;
        tamanho.y = (cena.tags.Count + 2) * tagAltura;
        rectTransform.sizeDelta = tamanho;
        
    }

    public void ExecutarEscolha(CenaHistoria cena, int p, float pEmp)
    {
        gameControle.pontosControle.AddPontos(p);
        gameControle.empatiaControle.GanharOuPerderEmpatia(pEmp);
        gameControle.IniciarCena(cena);
        animator.SetTrigger("Esconder");
    }

    private float CalcularPosicaoTag(int tagIndex, int tagCount)
    {
        if(tagCount % 2 == 0)
        {
            if(tagIndex < tagCount / 2)
            {
                return tagAltura * (tagCount / 2 - tagIndex - 1) + tagAltura / 2;
            }
            else
            {
                return -1 * (tagAltura * (tagIndex - tagCount / 2) + tagAltura / 2);
            }
        }
        else
        {
            if (tagIndex < tagCount / 2)
            {
                return tagAltura * (tagCount / 2 - tagIndex - 1) + tagAltura / 2;
            }
            else if (tagIndex > tagCount / 2)
            {
                return -1 * (tagAltura * (tagIndex - tagCount / 2));
            }
            else
            {
                return 0;
            }
        }
    }

    private void DestruirTags()
    {
        foreach(Transform childTransform in transform)
        {
            Destroy(childTransform.gameObject);
        }
    }

}
