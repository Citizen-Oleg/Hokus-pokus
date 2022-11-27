using System;
using System.Collections.Generic;
using Base.SimpleEventBus_and_MonoPool;
using BuildingSystem.CashSystem;
using DG.Tweening;
using Events;
using ResourceSystem;
using Tools.SimpleEventBus;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace HireHelperSystem.UI
{
    public class HireHelperScreen : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _container;
        [SerializeField]
        private GridLayoutGroup _gridLayoutGroup;
        [SerializeField]
        private ScrollRect _scrollRect;
        [SerializeField]
        private float _additionalSizeIncrease;
        [SerializeField]
        private SettingsStaffViewPool _settingsStaffViewPool;
        [SerializeField]
        private SettingsServiceViewPool _settingsServiceViewPool;
        [SerializeField]
        private AnimationScreenController _animationScreenController;

        private DefaultMonoBehaviourPool<StaffView> _staffViewPool;
        private DefaultMonoBehaviourPool<ServiceView> _serviceViewPool;
        
        private HireHelperController _hireHelperController;
        private HireHelperDataBaseInformation _hireHelperDataBaseInformation;
        private ResourceManagerGame _resourceManagerGame;

        private ColorPayButtonProvider _colorPayButtonProvider;
        private ProfessionIconProvider _professionIconProvider;
        private StaffIconProvider _staffIconProvider;
        private ServiceIconProvider _serviceIconProvider;
        private ServiceNameProvider _serviceNameProvider;

        private IDisposable _subscription;
        private float _startYSize;

        [Inject]
        public void Constructor(HireHelperController hireHelperController, HireHelperDataBaseInformation hireHelperDataBaseInformation, 
            ResourceManagerGame resourceManagerGame, ServiceNameProvider serviceNameProvider, ServiceIconProvider serviceIconProvider,
            StaffIconProvider staffIconProvider, ProfessionIconProvider professionIconProvider, ColorPayButtonProvider colorPayButtonProvider)
        {
            _serviceNameProvider = serviceNameProvider;
            _serviceIconProvider = serviceIconProvider;
            _colorPayButtonProvider = colorPayButtonProvider;
            _professionIconProvider = professionIconProvider;
            _staffIconProvider = staffIconProvider;
            
            _resourceManagerGame = resourceManagerGame;
            _hireHelperController = hireHelperController;
            _hireHelperDataBaseInformation = hireHelperDataBaseInformation;
            
            _staffViewPool = new DefaultMonoBehaviourPool<StaffView>(_settingsStaffViewPool.StaffViewPrefab, _settingsStaffViewPool.Parent, 
                _settingsStaffViewPool.PoolSize);
            _serviceViewPool = new DefaultMonoBehaviourPool<ServiceView>(_settingsServiceViewPool.StaffViewPrefab, _settingsServiceViewPool.Parent,
                _settingsServiceViewPool.PoolSize);

            _subscription = EventStreams.UserInterface.Subscribe<EventOpenHireHelperScreen>(Show);
            _startYSize = _container.sizeDelta.y;
        }

        private void Show(EventOpenHireHelperScreen eventOpenHireHelperScreen)
        {
            CreateView();
            ChangeSizeContainer();
            _resourceManagerGame.OnResourceChange += ChangeColorStaffView;
            _animationScreenController.Show();
        }

        private void OnDisable()
        {
            _serviceViewPool.ReleaseAll(false);
            _staffViewPool.ReleaseAll(false);
            _resourceManagerGame.OnResourceChange -= ChangeColorStaffView;
        }

        private void CreateView()
        {
            var service = _hireHelperDataBaseInformation.ActiveService;

            foreach (var serviceInformation in service)
            {
                if (serviceInformation.NeedStaff.Count == 0)
                {
                    continue;
                }
                    
                
                var serviceView = _serviceViewPool.Take();
                var staffViews = new List<StaffView>();

                foreach (var staffType in serviceInformation.NeedStaff)
                {
                    var staffView = _staffViewPool.Take();

                    var staffIcon = _staffIconProvider.GetSpriteByStaffType(staffType);
                    var professionIcon = _professionIconProvider.GetSpriteByServiceZone(staffType, serviceInformation.ServiceZone);
                    staffView.Initialize(staffType, staffIcon, professionIcon);
                    
                    staffViews.Add(staffView);
                }

                var iconService = _serviceIconProvider.GetSpriteByServiceZone(serviceInformation.ServiceZone);
                var name = _serviceNameProvider.GetNameByService(serviceInformation.ServiceZone);
                serviceView.transform.SetParent(_container, false);
                serviceView.Initialize(this, serviceInformation.ServiceZone, staffViews, iconService, name);
            }
        }

        public bool TryBuyStaff(StaffView staffView, ServiceZone serviceZone)
        {
            if (_hireHelperController.HasBuyStaff(staffView.StaffType))
            {
                _hireHelperController.BuyStaff(staffView.StaffType, serviceZone);
                _staffViewPool.Release(staffView);
                
                return true;
            }

            return false;
        }

        private void ChangeColorStaffView(Resource resource)
        {
            ChangeColorStaffView();
        }

        private void ChangeColorStaffView()
        {
            foreach (var staffView in _staffViewPool.UsedItems)
            {
                staffView.BuyButtonStaff.ChangeColor(
                    _hireHelperController.HasBuyStaff(staffView.StaffType) ?
                        _colorPayButtonProvider.GetColorByStatePayButton(StatePayButton.Open) :
                        _colorPayButtonProvider.GetColorByStatePayButton(StatePayButton.Close));
            }
        }

        public void ReleaseServiceView(ServiceView serviceView)
        {
            _serviceViewPool.Release(serviceView);
            ChangeSizeContainer();
        }

        private void ChangeSizeContainer()
        {
            var height = _hireHelperDataBaseInformation.ActiveService.Count * (_gridLayoutGroup.cellSize.y + _additionalSizeIncrease);
            _container.sizeDelta = new Vector2(_container.sizeDelta.x, _startYSize + height);
            _scrollRect.verticalNormalizedPosition = 1f;

        }

        private void OnDestroy()
        {
            _subscription?.Dispose();
        }

        [Serializable]
        public struct SettingsStaffViewPool
        {
            public int PoolSize;
            public Transform Parent;
            public StaffView StaffViewPrefab;
        }
        
        [Serializable]
        public struct SettingsServiceViewPool
        {
            public int PoolSize;
            public Transform Parent;
            public ServiceView StaffViewPrefab;
        }
    }
}