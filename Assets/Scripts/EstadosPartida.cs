using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EstadosPartida : MonoBehaviour
{
    public static EstadosPartida instance;

    public GameObject PanelPerder;
    public GameObject PanelGanar;

    public TextMeshProUGUI Temporizador;

    public GameObject mensaje;


    private float tiempoRestante = 5f; // 1 minuto


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1f;
        PanelPerder.SetActive(false);
        PanelGanar.SetActive(false);
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        TemporizadorUpdate();
    }

    public void Perder()
    {
        Time.timeScale = 0f;
        Temporizador.text = "";
        mensaje.SetActive(false);
        PanelPerder.SetActive(true);
    }

    public void Ganar()
    {
        Time.timeScale = 0f;
        Temporizador.text = "";
        mensaje.SetActive(false);
        PanelGanar.SetActive(true);
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

    public void TemporizadorUpdate()
    {
        if (tiempoRestante > 0) 
        { 
            tiempoRestante -= Time.deltaTime; 
            int minutos = Mathf.FloorToInt(tiempoRestante / 60); 
            int segundos = Mathf.FloorToInt(tiempoRestante % 60); 
            Temporizador.text = $"{minutos:00}:{segundos:00}"; 
        } 
        else 
        { 
            tiempoRestante = 0; 
            Temporizador.text = "00:00"; 
            Ganar();
        }
    }
}
