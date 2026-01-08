using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class NPC_Behaviour : MonoBehaviour
{

    [SerializeField] private Vector3 destination;
    [SerializeField] private GameObject player;

    [SerializeField] private float playerDetectionDistance;
    [SerializeField] private bool playerDetected;

    [SerializeField] private int childrenIndex;

    private Coroutine runningPatroll;
    private Coroutine runningFollow;



    public void Start()
    {
        /*destination = RandomDestination();
        GetComponent<NavMeshAgent>().SetDestination(destination);*/

        //path = GameObject.FindGameObjectWithTag("Path").transform;
        player = GameObject.FindGameObjectWithTag("Player");

        runningPatroll = StartCoroutine(Patroll());
        //StartCoroutine(DistanceDetection()); 

    }


    public void Update()
    {
        /*if(playerDetected)
        {
            StopCoroutine(Patroll());
        }
        else if(runningPatroll == null)
        {
            StartCoroutine(Patroll());
        }*/
    }
    

    private Vector3 RandomNavMeshPoint(float radius)
    {
        
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection.y = 0;
        randomDirection += transform.position;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, radius, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return transform.position + transform.forward * 2f;

    }

    #region Always Detect

    IEnumerator Follow()
    {
        while (true)
        {
            destination = player.transform.position;
            GetComponent<NavMeshAgent>().SetDestination(destination);
            //yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(0.25f);
        }
    }
    #endregion


    #region Patroll Movement
    IEnumerator Patroll()
    {
        destination = RandomNavMeshPoint(10f);
        GetComponent<NavMeshAgent>().SetDestination(destination);

        while (true)
        {
            if(Vector3.Distance(transform.position, destination) < 1f)
            {
                //yield return new WaitForSeconds(2f);

                destination = RandomNavMeshPoint(10f);
                GetComponent<NavMeshAgent>().SetDestination(destination);
            }
            yield return null;
        }
    }
    #endregion


    #region Distance Detection

    IEnumerator DistanceDetection()
    {
        while (true)
        {
            if(Vector3.Distance(transform.position, player.transform.position) < playerDetectionDistance)
            {
                if(runningPatroll != null)
                {
                    StopCoroutine(Patroll());
                    runningPatroll = null;
                } 
                playerDetected = true;
                destination = player.transform.position;
                GetComponent<NavMeshAgent>().SetDestination(destination);
            }  
            else
            {
                playerDetected = false;
                if(runningPatroll == null)
                    runningPatroll = StartCoroutine(Patroll());
            }

            yield return new WaitForSeconds(0.25f);
        }
    }

    #endregion

    #region Collider Detection

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                if(runningPatroll != null)
                {
                    StopCoroutine(runningPatroll);
                    runningPatroll = null;
                }
                playerDetected = true;
                if (runningFollow == null) 
                    runningFollow = StartCoroutine(Follow());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                if (runningFollow != null) 
                { 
                    StopCoroutine(runningFollow); 
                    runningFollow = null; 
                } 
                // Volver a patrullar 
                if (runningPatroll == null) 
                    runningPatroll = StartCoroutine(Patroll());
            }
        }


    #endregion



    #region Collider Player

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                EstadosPartida.instance.Perder();
            }
        }
    #endregion



}
