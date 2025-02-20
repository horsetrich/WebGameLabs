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

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            destination = waypoints[index].position;
        }

        private void OnEnable()
        {
            player.AddObserver(this);
        }

        private void Start()
        {
            agent.destination = destination;
            player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        }

        private void OnDisable()
        {
            player.RemoveObserver(this);
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Update()
        {
            if (Vector3.Distance(destination, transform.position) < distanceThreshold)
            {
                index = (index + 1) % waypoints.Count;
                destination = waypoints[index].position;
                agent.destination = destination;
            }
            //var destination = GameObject.FindWithTag("Player").transform.position;
            //agent.destination = destination;
        }
        public void OnNotify()
        {

        }
    }
}
