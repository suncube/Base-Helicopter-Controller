using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace HelicopterECS
{
    public class InputSystem : ComponentSystem
    {

        private string horizontalAxis = "Horizontal";
        private string verticalAxis = "Vertical";
        private string jumpButton = "Jump";

        struct PlayerData
        {
            public readonly int Length;

            public ComponentDataArray<PlayerInput> Input;
        }

        [Inject] private PlayerData m_Players;

        protected override void OnUpdate()
        {
            //  float dt = Time.deltaTime;

            for (int i = 0; i < m_Players.Length; i++)
            {
                UpdatePlayerInput(i);//, dt);
            }
        }

        private void UpdatePlayerInput(int i)//, float dt)
        {
            PlayerInput pi;

            pi.Move.x = SimpleInput.GetAxis(horizontalAxis);
            pi.Move.y = SimpleInput.GetAxis(verticalAxis);
            pi.UpMove = SimpleInput.GetAxis(jumpButton);

            //pi.UpMove.y = Input.GetAxis("Jump");

            //pi.FireCooldown = Mathf.Max(0.0f, m_Players.Input[i].FireCooldown - dt);

            m_Players.Input[i] = pi;
        }


        //private void UpdatePlayerInput(int i)//, float dt)
        //{
        //    PlayerInput pi;

        //    pi.Move.x = Input.GetAxis("Horizontal");
        //    pi.Move.y = Input.GetAxis("Vertical");
        //    pi.UpMove = Input.GetAxis("Jump");

        //    //pi.UpMove.y = Input.GetAxis("Jump");

        //    //pi.FireCooldown = Mathf.Max(0.0f, m_Players.Input[i].FireCooldown - dt);

        //    m_Players.Input[i] = pi;
        //}
    }
}