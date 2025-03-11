using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Platformer397
{
    public class EnemyNavigation : MonoBehaviour, IObserver
    {
        private NavMeshAgent agent;
        [SerializeField] private PlayerController player;
        [SerializeField] private List<Transform> waypoints = new List<Transform>();
        [SerializeField] private float distanceThreshold = 1.0f;
        private int index = 0;
        private Vector3 destination;
        // Enemy Sensing Stats
        [SerializeField] private LayerMask mask; // The layer that correspond to player
        [SerializeField] private int viewDistance = 10; // Distance of eyesight of enemy
        [SerializeField] private EnemyStates state = EnemyStates.Patrolling; // Initial state

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            destination = waypoints[index].position;
        }

        private void OnEnable()
        {
            player.AddObserver(this);
        }

        private void Start()
        {
            agent.destination = destination;

        }

        private void OnDisable()
        {
            player.RemoveObserver(this);
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Update()
        {
            switch (state)
            {
                case EnemyStates.Patrolling:
                    if (Vector3.Distance(destination, transform.position) < distanceThreshold)
                    {
                        index = (index + 1) % waypoints.Count;
                        destination = waypoints[index].position;
                        agent.destination = destination;
                    }
                    break;
                case EnemyStates.Chasing:
                    agent.destination = player.gameObject.transform.position;
                    break;
                default:
                    Debug.Log("State not configured",this);
                    break;
            }
            agent.destination = destination;

            //var destination = GameObject.FindWithTag("Player").transform.position;
            //agent.destination = destination;
        }

        private void FixedUpdate()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, viewDistance, mask))
            {
                if (hit.transform.gameObject.CompareTag("Player"))
                {
                    state = EnemyStates.Chasing;
                    Debug.Log("Hit Player");
                }
                Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.green);
            }
            else
            {
                state = EnemyStates.Patrolling;
                Debug.DrawRay(transform.position, transform.forward * viewDistance, Color.yellow);
            }
        }

        private void OnDrawGizmos()
        {
            if(waypoints.Count > 0)
            {
                Gizmos.color = Color.red;
                for(int i = 0; i < waypoints.Count; i++)
                {
                    Gizmos.DrawSphere(waypoints[i].position, distanceThreshold);
                    if (i > 0)
                    {
                        Gizmos.DrawLine(waypoints[i - 1].position, waypoints[i].position);
                    }
                }
            }
        }

        public void OnNotify()
        {

        }
    }
}
