using UnityEngine;

public class SpawnNPC : MonoBehaviour
{
    public GameObject npcPrefab;

    public Transform puntoSpawn;

    public float separacion = 1.5f;

    void Start()
    {
        int cantidadNPC = MenuController.instance.numeroFantasmas;

        for (int i = 0; i < cantidadNPC; i++)
        {
            Vector3 offset = puntoSpawn.right * separacion * i;
            Vector3 posicion = puntoSpawn.position + offset;

            Instantiate(npcPrefab, posicion, puntoSpawn.rotation);

        }
    }
}
