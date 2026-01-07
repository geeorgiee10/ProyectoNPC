using UnityEngine;
using UnityEngine.SceneManagement;

public class EstadosPartida : MonoBehaviour
{
    public static EstadosPartida instance;

    public GameObject PanelPerder;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PanelPerder.SetActive(false);
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Perder()
    {
        Time.timeScale = 0f;
        PanelPerder.SetActive(true);
    }

    public void VolverMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void Reiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
