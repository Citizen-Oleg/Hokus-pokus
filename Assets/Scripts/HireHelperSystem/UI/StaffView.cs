using System;
using StaffSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HireHelperSystem.UI
{
    public class StaffView : MonoBehaviour
    {
        public event Action<StaffView> OnBuyStaff;
        public StaffType StaffType => _staffType;
        
        public BuyButtonStaff BuyButtonStaff => _buyButtonStaff;

        [SerializeField]
        private Image _iconStaff;
        [SerializeField]
        private Image _professionIcon;
        [SerializeField]
        private BuyButtonStaff _buyButtonStaff;

        private StaffType _staffType;

        private void Awake()
        {
            _buyButtonStaff.OnClick += OnClickBuyButton;
        }

        private void OnClickBuyButton()
        {
            OnBuyStaff?.Invoke(this);
        }

        public void Initialize(StaffType staffType, Sprite iconStaff, Sprite professionIcon)
        {
            _iconStaff.sprite = iconStaff;
            _professionIcon.sprite = professionIcon;
            _staffType = staffType;
        }

        private void OnDestroy()
        {
            _buyButtonStaff.OnClick -= OnClickBuyButton;
        }
    }
}