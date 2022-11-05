using System;
using System.Collections.Generic;

namespace ResourceSystem
{
    public class ResourceManagerLevel : ResourceManager
    {
        public ResourceManagerLevel(Settings settings)
        {
            Resources = new List<Resource>(settings.StartResource);
        }

        [Serializable]
        public class Settings
        {
            public List<Resource> StartResource = new List<Resource>();
        }
    }
}