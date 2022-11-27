using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HireHelperSystem.UI
{
    public class ColorPayButtonProvider
    {
        private readonly List<ColorPayButton> _colorPayButtons;
        
        public ColorPayButtonProvider(Settings settings)
        {
            _colorPayButtons = settings.ColorPayButtons;
        }

        public Color GetColorByStatePayButton(StatePayButton statePayButton)
        {
            return _colorPayButtons.FirstOrDefault(state => state.StatePayButton == statePayButton).Color;
        }
        
        [Serializable]
        public class Settings
        {
            public List<ColorPayButton> ColorPayButtons = new List<ColorPayButton>();
        }

        [Serializable]
        public struct ColorPayButton
        {
            public Color Color;
            public StatePayButton StatePayButton;
        }
    }
}