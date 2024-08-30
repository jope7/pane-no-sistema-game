using UnityEngine;
using UnityEngine.UI;

public class SpriteInterruptor : MonoBehaviour
{

    public bool foiTrocado = false;
    public Image imagem1;
    public Image imagem2;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void TrocarImagem(Sprite sprite)
    {
        if (!foiTrocado)
        {
            imagem2.sprite = sprite;
            animator.SetTrigger("TrocarPrimeiro");
        } else
        {
            imagem1.sprite = sprite;
            animator.SetTrigger("TrocarSegundo");
        }
        foiTrocado = !foiTrocado;
    }

    public void SetImagem(Sprite sprite)
    {
        if (!foiTrocado)
        {
            imagem1.sprite = sprite;
        }
        else
        {
            imagem2.sprite = sprite;
        }
    }

    public void SincronizarImagem()
    {
        if (!foiTrocado)
        {
            imagem2.sprite = imagem1.sprite;
        }
        else
        {
            imagem1.sprite = imagem2.sprite;
        }
    }

    public Sprite GetImagem()
    {
        if (!foiTrocado)
        {
            return imagem1.sprite;
        }
        else
        {
            return imagem2.sprite;
        }
    }


}
