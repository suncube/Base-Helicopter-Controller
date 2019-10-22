using UnityEngine;
using System.Collections;

namespace SunCube.Radar
{
    public class RadarItem : AdventureBehaviour
    {
        public RadarTargetType TargetType;

        void Start()
        {
            if (RadarSystem.runtime != null)
                RadarSystem.runtime.AddTarget(this);
        }
    }
}