using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteControle : MonoBehaviour
{
    
    private SpriteInterruptor interruptor;
    private Animator animator;
    private RectTransform rect;
    private CanvasGroup canvasGp;

    private void Awake()
    {
        interruptor = GetComponent<SpriteInterruptor>();
        animator = GetComponent<Animator>();
        rect = GetComponent<RectTransform>();
        canvasGp = GetComponent<CanvasGroup>();
    }

    public void Iniciar(Sprite sprite)
    {
        interruptor.SetImagem(sprite);
    }

    public void Mostrar(Vector2 posicao, bool ehAnimado = true)
    {

        if (ehAnimado)
        {
            animator.enabled = true;
            animator.SetTrigger("Mostrar");
        } 
        else
        {
            animator.enabled = false;
            canvasGp.alpha = 1;
        }

        rect.localPosition = posicao;
    }

    public void Esconder(bool ehAnimado = true)
    {

        if (ehAnimado)
        {
            animator.enabled = true;
            interruptor.SincronizarImagem();
            animator.SetTrigger("Esconder");
        }
        else
        {
            animator.enabled = false;
            canvasGp.alpha = 0;
        }

        
    }

    public void TrocarSprite(Sprite sprite, bool ehAnimado = true)
    {
        
        if(interruptor.GetImagem() != sprite) 
        {

            if (ehAnimado)
            {
                interruptor.TrocarImagem(sprite);
            } 
            else
            {
                interruptor.SetImagem(sprite);
            }
        }

    }


}
