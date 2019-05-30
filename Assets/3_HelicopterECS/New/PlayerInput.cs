using System;
using Boo.Lang;
using Unity.Entities;
using Unity.Mathematics;

namespace HelicopterECS
{
    public struct PlayerInput : IComponentData
    {
        public float2 Move;
        public float UpMove;
	    
        //public float FireCooldown;
        //public bool Fire => FireCooldown <= 0.0 && math.length(UpMove) > 0.5f;
    }

    public struct HelicopterState : IComponentData
    {

    }
}