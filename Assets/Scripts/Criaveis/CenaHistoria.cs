using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NovaCena", menuName = "Data/Criar nova Cena")]
[System.Serializable]
public class CenaHistoria : CenaJogo
{

    public List<Sentenca> sentencas;
    public Sprite background;
    public CenaJogo proxCena;

    public AudioClip music;     // posso colocar dentro da Sentenc

    [System.Serializable]
    public struct Sentenca
    {
        public string texto;
        public Personagem personagem;
        public List<Acao> acoes; // ações
        public AudioClip fx; 

        [System.Serializable]
        public struct Acao // ação
        {
            public Personagem personagem;
            public int spriteIndex;
            public Tipo tipoAcao;   // tipo de ação sendo realizada
            public Vector2 posicao;
            // public float velocidadeMovimento; // n iremos utilizar no nosso projeto
            
            [System.Serializable]
            public enum Tipo
            {
                NENHUMA, APARECENDO, DESAPARECENDO //, MOVIMENTANDO
            }

        }
    }

}

public class CenaJogo : ScriptableObject { }