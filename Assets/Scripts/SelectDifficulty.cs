using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectDifficulty : MonoBehaviour
{
    public void Facil()
    {
        MenuController.instance.numeroFantasmas = 3;
        SceneManager.LoadScene("SampleScene");
    }

    public void Medio()
    {
        MenuController.instance.numeroFantasmas = 7;
        SceneManager.LoadScene("SampleScene");
    }

    public void Dificil()
    {
        MenuController.instance.numeroFantasmas = 15;
        SceneManager.LoadScene("SampleScene");
    }
}
