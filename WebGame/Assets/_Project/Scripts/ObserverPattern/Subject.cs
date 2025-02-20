using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Platformer397
{
    public class Subject : MonoBehaviour
    {
        [SerializeField] private List<IObserver> observers = new List<IObserver>();

        public void AddObserver(IObserver observer) => observers.Add(observer);

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        public void RemoveObserver(IObserver observer) => observers.Remove(observer);

        public void NotifyObservers()
        {
            foreach(IObserver observer in observers)
            {
                observer.OnNotify();
            }
        }
    }
}
