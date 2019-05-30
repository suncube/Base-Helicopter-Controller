using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Transforms2D;
using UnityEngine;

namespace HelicopterECS
{
    public class Bootstrap : MonoBehaviour
    {
        public GameObject PlayerPrefab;

        void Start()
        {

            //var player = Object.Instantiate(PlayerPrefab);

            var entityManager = World.Active.GetOrCreateManager<EntityManager>();

            var PlayerArchetype = entityManager.CreateArchetype(
                typeof(PlayerInput), typeof(Position), typeof(Rotation), typeof(HelicopterState));

            Entity player = entityManager.CreateEntity(PlayerArchetype);
            
            entityManager.SetComponentData(player, new PlayerInput() { Move = new float2(1,1)});
        }


    }
}