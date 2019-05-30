using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace HelicopterECS
{
    public class HelicopterModelSystem : ComponentSystem
    {
        struct PlayerData
        {
            public readonly int Length;
            public ComponentArray<HelicopterModel> Model;
        }

        [Inject] private PlayerData m_Group;

        struct InputData
        {
            public readonly int Length;

            public ComponentDataArray<PlayerInput> Value;
        }

        [Inject] private InputData m_Input;

        private PlayerInput Input { get { return m_Input.Value[0]; } }

        protected override void OnUpdate()
        {
            if (m_Input.Length == 0)
                return;


            for (int i = 0; i < m_Group.Length; i++)
            {
                Processing(m_Group.Model[i]);
            }
        }

        private void Processing(HelicopterModel Model)//, float dt)
        {

            if (Input.UpMove > 0)
            {
                Model.EngineForce += 0.1f;
            }
            else
            if (Input.UpMove < 0)
            {
                Model.EngineForce -= 0.12f;
            }
            
            LiftProcess(Model);

            if(Model.IsOnGround) return;

            MoveProcess(Model);
            TiltProcess(Model);
        }

        private Vector2 hTilt = Vector2.zero;
        private float hTurn = 0f;
        private void MoveProcess(HelicopterModel Model)
        {
            var turn = Model.TurnForce * Mathf.Lerp(Input.Move.x, Input.Move.x * (Model.turnTiltForcePercent - Mathf.Abs(Input.Move.y)), Mathf.Max(0f, Input.Move.y));
            hTurn = Mathf.Lerp(hTurn, turn, Time.fixedDeltaTime * Model.TurnForce);
            Model.Helicopter.AddRelativeTorque(0f, hTurn * Model.Helicopter.mass, 0f);
            Model.Helicopter.AddRelativeForce(Vector3.forward * Mathf.Max(0f, Input.Move.y * Model.ForwardForce * Model.Helicopter.mass));
        }

        private void LiftProcess(HelicopterModel Model)
        {
            var upForce = 1 - Mathf.Clamp(Model.transform.position.y / Model.EffectiveHeight, 0, 1);
            upForce = Mathf.Lerp(0f, Model.EngineForce, upForce) * Model.Helicopter.mass;
            Model.Helicopter.AddRelativeForce(Vector3.up * upForce);
        }

        private void TiltProcess(HelicopterModel Model)
        {
            hTilt.x = Mathf.Lerp(hTilt.x, Input.Move.x * Model.TurnTiltForce, Time.deltaTime);
            hTilt.y = Mathf.Lerp(hTilt.y, Input.Move.y * Model.ForwardTiltForce, Time.deltaTime);
            Model.transform.localRotation = Quaternion.Euler(hTilt.y, Model.transform.localEulerAngles.y, -hTilt.x);
        }
    }
}