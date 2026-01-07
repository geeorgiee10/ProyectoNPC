using UnityEngine;


public class MenuController : MonoBehaviour
{
    public static MenuController instance;

    [Header("Datos")]
    public int numeroFantasmas;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
