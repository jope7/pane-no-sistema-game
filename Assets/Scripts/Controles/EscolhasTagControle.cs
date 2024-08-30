using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System.Reflection.Emit;

public class EscolhasTagControle : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    public Color defaultColor;
    public Color hoverColor;

    private CenaHistoria cena;
    private TextMeshProUGUI textMesh;
    private EscolhasControle controle;
    private int pontos;
    private float pontosEmpaticos;

    void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.color = defaultColor;
    }

    public float GetAltura()
    {
        return textMesh.rectTransform.sizeDelta.y * textMesh.rectTransform.localScale.y;
    }

    public void Configurar(CenaEscolhas.TagEscolha tag, EscolhasControle controle, float y)
    {
        cena = tag.proxCena;
        textMesh.text = tag.texto;
        pontos = tag.pontos;
        this.controle = controle;
        
        if (pontos > 0)
        {
            pontosEmpaticos = 1;
        } else if (pontos <= 0)
        {
            pontosEmpaticos = -1;
        }

        Vector3 posicao = textMesh.rectTransform.localPosition;
        posicao.y = y;
        textMesh.rectTransform.localPosition = posicao;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        controle.ExecutarEscolha(cena, pontos, pontosEmpaticos);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        textMesh.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textMesh.color = defaultColor;
    }

}
