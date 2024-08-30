using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CenaInterruptor : MonoBehaviour
{

    public string cenaAhDescarregar;
    public string cenaCarregada;
    public string cenaAhCarregar;

    private void Start()
    {
        ComecarDescarregarCena();
    }

    private void ComecarDescarregarCena()
    {
        SceneManager.sceneUnloaded += OnStartUnloaded;
        SceneManager.UnloadSceneAsync(cenaAhDescarregar);
    }

    private void OnStartUnloaded(Scene scene)
    {
        SceneManager.sceneUnloaded -= OnStartUnloaded;
        SceneManager.sceneLoaded += OnEndLoaded;
        SceneManager.LoadSceneAsync(cenaAhCarregar, LoadSceneMode.Additive);
    }

    private void OnEndLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnEndLoaded;
        UnloadLoader();
    }

    private void UnloadLoader()
    {
        SceneManager.UnloadSceneAsync(cenaCarregada);
    }

}
