using System;
using StaffSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HireHelperSystem.UI
{
    public class BuyButtonStaff : MonoBehaviour, IPointerClickHandler
    {
        public event Action OnClick; 
        
        [SerializeField]
        private Image _button;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke();
        }
        
        public void ChangeColor(Color color)
        {
            _button.color = color;
        }
    }
}