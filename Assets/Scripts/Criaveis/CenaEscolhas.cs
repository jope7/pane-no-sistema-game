using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NovaCenaEscolha", menuName = "Data/Criar nova cena de Escolhas")]
[System.Serializable]
public class CenaEscolhas : CenaJogo
{

    public List<TagEscolha> tags;

    [System.Serializable]
    public struct TagEscolha
    {
        public string texto;
        public CenaHistoria proxCena;
        public int pontos;
    }

}
