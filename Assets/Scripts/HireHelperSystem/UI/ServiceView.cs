using System;
using System.Collections.Generic;
using BuildingSystem.CashSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HireHelperSystem.UI
{
    public class ServiceView : MonoBehaviour
    {
        [SerializeField]
        private Image _iconService;
        [SerializeField]
        private TextMeshProUGUI _name;
        [SerializeField]
        private RectTransform _container;

        private ServiceZone _serviceZone;
        private HireHelperScreen _hireHelperScreen;
        private List<StaffView> _staffViews;
        
        public void Initialize(HireHelperScreen hireHelperScreen, ServiceZone serviceZone, List<StaffView> staffViews, 
            Sprite serviceIcon, string name)
        {
            _name.text = name;
            _hireHelperScreen = hireHelperScreen;
            _serviceZone = serviceZone;
            _staffViews = staffViews;
            _iconService.sprite = serviceIcon;
            
            foreach (var staffView in _staffViews)
            {
                staffView.transform.SetParent(_container, false);
                staffView.OnBuyStaff += BuyStaff;
            }
        }

        private void BuyStaff(StaffView staffView)
        {
            if (_hireHelperScreen.TryBuyStaff(staffView, _serviceZone))
            {
                _staffViews.Remove(staffView);

                if (_staffViews.Count == 0)
                {
                    _hireHelperScreen.ReleaseServiceView(this);
                }
            }
        }

        private void OnDisable()
        {
            if (_staffViews != null)
            {
                foreach (var staffView in _staffViews)
                {
                    staffView.OnBuyStaff -= BuyStaff;
                }
            }
        }
    }
}