using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

//namespace HelicopterECS
//{
//    public class HelicopterSystem : ComponentSystem
//    {

//        struct GroupModel
//        {
//            public HelicopterModel HelicopterModel;
//            //public HeliSettingsData StateData;
//            public Transform Position;

//        }

//        struct GroupSetting
//        {
//            public ComponentDataArray<HeliSettingsData> StateData;
//            public int Length;
//        }

//        [Inject] GroupSetting m_Group;

//        protected override void OnUpdate()
//        {
//            if (m_Group.Length == 0)
//                return;

//            var data = m_Group.StateData[0];
//            //foreach (var entity in GetEntities<GroupModel>())
//            //{
//            //    Debug.Log(entity.Position.gameObject.name);
//            //}
//            var groupArray = GetEntities<GroupModel>();
//            for (var i = 0; i < groupArray.Length; i++)
//            {
//                var model = groupArray[i].HelicopterModel.Helicopter;

//                model.AddRelativeTorque(0f, data.Tforce, 0);
//                model.AddRelativeTorque(0f, data.Torque, 0f);
//                model.AddRelativeForce(data.FForce);
//                model.AddRelativeForce(Vector3.up * data.upForce);
//                groupArray[i].Position.localRotation = data.TiltAngel;
//            }

//        }



//        private void MoveProcess()
//        {
//            var turn = TurnForce * Mathf.Lerp(hMove.x, hMove.x * (turnTiltForcePercent - Mathf.Abs(hMove.y)),
//                           Mathf.Max(0f, hMove.y));
//            hTurn = Mathf.Lerp(hTurn, turn, Time.fixedDeltaTime * TurnForce);
//            HelicopterModel.AddRelativeTorque(0f, hTurn * HelicopterModel.mass, 0f);
//            HelicopterModel.AddRelativeForce(Vector3.forward *
//                                             Mathf.Max(0f, hMove.y * ForwardForce * HelicopterModel.mass));
//        }

//        private void LiftProcess()
//        {
//            var upForce = 1 - Mathf.Clamp(HelicopterModel.transform.position.y / EffectiveHeight, 0, 1);
//            upForce = Mathf.Lerp(0f, EngineForce, upForce) * HelicopterModel.mass;
//            HelicopterModel.AddRelativeForce(Vector3.up * upForce);
//        }

//        private void TiltProcess()
//        {
//            hTilt.x = Mathf.Lerp(hTilt.x, hMove.x * TurnTiltForce, Time.deltaTime);
//            hTilt.y = Mathf.Lerp(hTilt.y, hMove.y * ForwardTiltForce, Time.deltaTime);
//            HelicopterModel.transform.localRotation =
//                Quaternion.Euler(hTilt.y, HelicopterModel.transform.localEulerAngles.y, -hTilt.x);
//        }

//    }
//}