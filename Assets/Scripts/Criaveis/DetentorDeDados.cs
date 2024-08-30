using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NovoDetentorDeDados", menuName = "Data/Novo Detentor de Dados")]
[System.Serializable]
public class DetentorDeDados : ScriptableObject
{
    [Header("ARMAZENAR TODAS AS CENAS DO JOGO!")]
    public List<CenaJogo> cenas;
}
