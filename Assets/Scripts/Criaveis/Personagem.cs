using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NovoPersonagem", menuName = "Data/Criar novo Personagem")]
[System.Serializable]
public class Personagem : ScriptableObject
{
    public string nome;
    public Color corNome;
    public List<Sprite> sprites;
    public SpriteControle prefab;
}
