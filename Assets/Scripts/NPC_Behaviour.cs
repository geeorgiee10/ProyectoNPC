using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class NPC_Behaviour : MonoBehaviour
{

    [SerializeField] private Vector3 destination;
    [SerializeField] private GameObject player;

    private Vector3 randomPoint;


    [SerializeField] private float playerDetectionDistance;
    [SerializeField] private bool playerDetected;

    private Coroutine runningPatroll;


    public void Start()
    {
        /*destination = RandomDestination();
        GetComponent<NavMeshAgent>().SetDestination(destination);*/

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
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomDirection = Random.insideUnitSphere * radius;
            randomDirection.y = 0;
            randomDirection += transform.position;

            randomPoint = randomDirection;

            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, radius, NavMesh.AllAreas))
            {
                return hit.position;
            }

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
            yield return new WaitForEndOfFrame();
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

            yield return new WaitForSeconds(1);
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
                    StopCoroutine(Patroll());
                    runningPatroll = null;
                }
                playerDetected = true;
                StartCoroutine(Follow());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                StopCoroutine(Follow());
                playerDetected = false;
                if(runningPatroll == null)
                    runningPatroll = StartCoroutine(Patroll());
            }
        }


    #endregion



    #region Collider Player

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

    #endregion



}
