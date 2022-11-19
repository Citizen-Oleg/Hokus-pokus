using System;
using System.Collections.Generic;
using System.Linq;
using BuildingSystem.CashSystem;
using TMPro;
using UnityEngine;

namespace DesiredServiceSystem
{
    public class IconDesiredProvider
    {
        private readonly List<IconDesired> _iconDesireds;
        
        public IconDesiredProvider(Settings setting)
        {
            _iconDesireds = setting.IconDesireds;
        }

        public Sprite GetSpriteBySpriteTypeDesired(SpriteTypeDesired spriteTypeDesired)
        {
            return _iconDesireds.FirstOrDefault(iconDesired => iconDesired.SpriteTypeDesired == spriteTypeDesired).Sprite;
        }
        
        [Serializable]
        public class Settings
        {
            public List<IconDesired> IconDesireds = new List<IconDesired>();
        }

        [Serializable]
        public class IconDesired
        {
            public SpriteTypeDesired SpriteTypeDesired;
            public Sprite Sprite;
        }
    }
}