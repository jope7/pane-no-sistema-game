using UnityEngine;

public class GerenteDeSalvamento : MonoBehaviour
{
    public static string JOGO_SALVO = "jogoSalvo";

    public static void SalvarJogo(SalvarDados dados)
    {
        PlayerPrefs.SetString(JOGO_SALVO, JsonUtility.ToJson(dados));
    }

    public static SalvarDados CarregarJogo()
    {
        return JsonUtility.FromJson<SalvarDados>(PlayerPrefs.GetString(JOGO_SALVO));
    }

    public static bool EstaSalvado()
    {
        return PlayerPrefs.HasKey(JOGO_SALVO);
    }

    public static void LimparJogoSalvo()
    {
        PlayerPrefs.DeleteKey(JOGO_SALVO);
    }

}
