﻿using System;
using Portal.PlayerComponent;
using ResourceSystem;
using TMPro;
using UnityEngine;
using Zenject;

namespace BuildingSystem
{
    [RequireComponent(typeof(Collider))]
    public class PlaceInstallation : MonoBehaviour
    {
        [SerializeField]
        private float _activationTime;
        [SerializeField]
        private Resource _priceCreateBuilding;
        [SerializeField]
        private GameObject _openObject;
     
        [SerializeField]
        private PlaceAnimationController.Settings _settingsPlaceAnimationController;

        [Inject]
        private ResourceManagerGame _resourceManagerGame;

        private PlaceAnimationController _placeAnimationController;

        private void Awake()
        {
            GetComponent<Collider>().isTrigger = true;
            _placeAnimationController = new PlaceAnimationController(_settingsPlaceAnimationController, _priceCreateBuilding.Amount);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player) && _resourceManagerGame.HasEnough(_priceCreateBuilding))
            {
                _placeAnimationController.StartAnimation(_activationTime, () =>
                { 
                    _resourceManagerGame.Pay(_priceCreateBuilding);
                    gameObject.SetActive(false);
                    _openObject.SetActive(true);
                    _placeAnimationController.ZoomAnimation(_openObject);
                });
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                _placeAnimationController.StopAnimation();
            }
        }
    }
}