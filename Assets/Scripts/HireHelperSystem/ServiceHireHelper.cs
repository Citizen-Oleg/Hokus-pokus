using System;
using Events;
using Portal.PlayerComponent;
using Tools.SimpleEventBus;
using UnityEngine;

namespace HireHelperSystem
{
    [RequireComponent(typeof(Collider))]
    public class ServiceHireHelper : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Collider>().isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                EventStreams.UserInterface.Publish(new EventOpenHireHelperScreen());
            }
        }
    }
}