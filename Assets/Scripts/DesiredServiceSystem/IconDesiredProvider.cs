﻿using System;
using System.Collections.Generic;
using System.Linq;
using BuildingSystem.CashSystem;
using ItemSystem;
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
        
        public Sprite GetSpriteByItemType(ItemType itemType)
        {
            var spriteTypeDesired = GetSpriteTypeDesiredByItemType(itemType);
            return _iconDesireds.FirstOrDefault(iconDesired => iconDesired.SpriteTypeDesired == spriteTypeDesired).Sprite;
        }
        
        private SpriteTypeDesired GetSpriteTypeDesiredByItemType(ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.Burger:
                    return SpriteTypeDesired.Burger;
                case ItemType.Soda:
                    return SpriteTypeDesired.Cola;
            }

            return SpriteTypeDesired.Ticket;
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