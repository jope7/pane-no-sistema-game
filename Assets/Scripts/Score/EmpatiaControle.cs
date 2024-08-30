using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmpatiaControle : MonoBehaviour
{

    [Header("Gauger")]
    public Image gauger;

    [Header("Nivel de Empatia")]
    [Range(-10, 10)]
    public int empatia;
    
    private void Start()
    {
        empatia = 0;
        // StartCoroutine(TestandoCoroutine());
    }

    /*private IEnumerator TestandoCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(6f);
            Debug.Log("Empatia: " + empatia);
            Debug.Log("Posicão: " + (ObterPosicaoX() * (float)empatia + 50.0f));
        }
    }
    */

    private float ObterPosicaoX()
    {
        return gauger.rectTransform.anchoredPosition.x;
    }

    public void GanharOuPerderEmpatia(float e)
    {

        // implementar para empatia só ter entre -10 e 10 na pontuação

        if (e != 1)
        {
            empatia--;
        }
        else
        {
            empatia++;
        }

        float posicaoX = ObterPosicaoX() + (float)empatia * 50.0f;

        Vector2 posicao;

        if (empatia >= 0 && empatia <= 10 && posicaoX >= 0)
        {

            if (posicaoX <= 0)
            {
                posicaoX += ObterPosicaoX();
            }
            else
            {
                posicaoX -= ObterPosicaoX();
            }

            posicao = new Vector2(posicaoX, gauger.rectTransform.anchoredPosition.y);
            
        } else if (empatia < 0 && empatia >= -10) 
        {
            if (posicaoX > 0)
            {
                posicaoX += ObterPosicaoX();
            } else
            {
                posicaoX -= ObterPosicaoX();
            }

            posicao = new Vector2(posicaoX, gauger.rectTransform.anchoredPosition.y);
        } else
        {
            posicao = new Vector2(ObterPosicaoX(), gauger.rectTransform.anchoredPosition.y);
        }

        // Vector2 posicao = new Vector2(ObterPosicaoX() * (float)empatia + 100.0f, gauger.rectTransform.anchoredPosition.y);

        gauger.rectTransform.anchoredPosition = posicao;
        
    }

}
