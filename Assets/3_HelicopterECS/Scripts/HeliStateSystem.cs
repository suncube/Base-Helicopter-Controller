using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace HelicopterECS
{
    public class HeliStateSystem : ComponentSystem
    {
        //struct Group
        //{
        //    public ComponentDataArray<InputData> Input;
        //    public int Length;
        //}

        //struct GroupSetting
        //{
        //    public ComponentDataArray<HeliSettingsData> StateData;
        //    public int Length;
        //}


        //struct GroupModel
        //{
        //    public HelicopterModel HelicopterModel;
        //    public Transform Position;
        //}



        //[Inject] Group m_Group;
        //[Inject] GroupSetting m_GroupSetting;

        protected override void OnUpdate()
        {
            //if (m_Group.Length == 0 || m_GroupSetting.Length == 0)
            //    return;

            //var data = new HeliSettingsData();//m_GroupSetting.StateData[0];

            //data = new HeliSettingsData()
            //{
            //    TurnForce = m_GroupSetting.StateData[0].TurnForce,
            //    ForwardForce = m_GroupSetting.StateData[0].ForwardForce,
            //    ForwardTiltForce = m_GroupSetting.StateData[0].ForwardTiltForce,
            //    TurnTiltForce = m_GroupSetting.StateData[0].TurnTiltForce,
            //    EffectiveHeight = m_GroupSetting.StateData[0].EffectiveHeight,

            //    turnTiltForcePercent = m_GroupSetting.StateData[0].turnTiltForcePercent,
            //    turnForcePercent = m_GroupSetting.StateData[0].turnForcePercent,

            //    EngineForce = m_GroupSetting.StateData[0].EngineForce,
            //    IsOnGround = m_GroupSetting.StateData[0].IsOnGround,
            //    Tforce = m_GroupSetting.StateData[0].Tforce,
            //    mass = m_GroupSetting.StateData[0].mass,
            //    Torque = m_GroupSetting.StateData[0].Torque,
            //    FForce = m_GroupSetting.StateData[0].FForce,
            //    TiltAngel = m_GroupSetting.StateData[0].TiltAngel,
            //    upForce = m_GroupSetting.StateData[0].upForce

            //};

            //var pressedKeyCodes = m_Group.Input[0].GetCommands();

            //foreach (var entity in GetEntities<GroupModel>())
            //{
            //    OnKeyPressed(pressedKeyCodes, data);
            //    Processing(data, entity.Position.position, entity.Position.localRotation);
            //}

            //for (int i = 0; i < m_GroupSetting.Length; i++)
            //{
            //    // m_GroupSetting.StateData[i] = new HeliSettingsData();
            //    m_GroupSetting.StateData[i] = new HeliSettingsData()
            //    {
            //        TurnForce = data.TurnForce,
            //        ForwardForce = data.ForwardForce,
            //        ForwardTiltForce = data.ForwardTiltForce,
            //        TurnTiltForce = data.TurnTiltForce,
            //        EffectiveHeight = data.EffectiveHeight,

            //        turnTiltForcePercent = data.turnTiltForcePercent,
            //        turnForcePercent = data.turnForcePercent,

            //        EngineForce = data.EngineForce,
            //        IsOnGround = data.IsOnGround,
            //        Tforce = data.Tforce,
            //        mass = data.mass,
            //        Torque = data.Torque,
            //        FForce = data.FForce,
            //        TiltAngel = data.TiltAngel,
            //        upForce = data.upForce

            //    };
            //}

        }

        //private Vector2 hMove = Vector2.zero;
        //private Vector2 hTilt = Vector2.zero;
        //private float hTurn = 0f;

        //private void OnKeyPressed(PressedKeyCode[] obj, HeliSettingsData stateData)
        //{
        //    float tempY = 0;
        //    float tempX = 0;

        //    // stable forward
        //    if (hMove.y > 0)
        //        tempY = -Time.fixedDeltaTime;
        //    else
        //        if (hMove.y < 0)
        //        tempY = Time.fixedDeltaTime;

        //    // stable lurn
        //    if (hMove.x > 0)
        //        tempX = -Time.fixedDeltaTime;
        //    else
        //        if (hMove.x < 0)
        //        tempX = Time.fixedDeltaTime;


        //    foreach (var pressedKeyCode in obj)
        //    {
        //        switch (pressedKeyCode)
        //        {
        //            case PressedKeyCode.SpeedUpPressed:
        //                stateData.EngineForce += 0.1f;

        //                Debug.Log("stateData.EngineForce" + stateData.EngineForce);

        //                break;
        //            case PressedKeyCode.SpeedDownPressed:

        //                stateData.EngineForce -= 0.12f;
        //                if (stateData.EngineForce < 0) stateData.EngineForce = 0;
        //                break;

        //            case PressedKeyCode.ForwardPressed:

        //                if (stateData.IsOnGround == 0) break;
        //                tempY = Time.fixedDeltaTime;
        //                break;
        //            case PressedKeyCode.BackPressed:

        //                if (stateData.IsOnGround == 0) break;
        //                tempY = -Time.fixedDeltaTime;
        //                break;
        //            case PressedKeyCode.LeftPressed:

        //                if (stateData.IsOnGround == 0) break;
        //                tempX = -Time.fixedDeltaTime;
        //                break;
        //            case PressedKeyCode.RightPressed:

        //                if (stateData.IsOnGround == 0) break;
        //                tempX = Time.fixedDeltaTime;
        //                break;
        //            case PressedKeyCode.TurnRightPressed:
        //                {
        //                    if (stateData.IsOnGround == 0) break;
        //                    stateData.Tforce = (stateData.turnForcePercent - Mathf.Abs(hMove.y)) * stateData.mass;
        //                    //HelicopterModel.AddRelativeTorque(0f, force, 0);
        //                }
        //                break;
        //            case PressedKeyCode.TurnLeftPressed:
        //                {
        //                    if (stateData.IsOnGround == 0) break;

        //                    stateData.Tforce = -(stateData.turnForcePercent - Mathf.Abs(hMove.y)) * stateData.mass;
        //                  //  HelicopterModel.AddRelativeTorque(0f, force, 0);
        //                }
        //                break;

        //        }
        //    }

        //    hMove.x += tempX;
        //    hMove.x = Mathf.Clamp(hMove.x, -1, 1);

        //    hMove.y += tempY;
        //    hMove.y = Mathf.Clamp(hMove.y, -1, 1);

        //}

        //void Processing(HeliSettingsData stateData,Vector3 position, Quaternion rotation)
        //{
        //    LiftProcess(stateData,position.y);
        //    MoveProcess(stateData);
            

        //    TiltProcess(stateData, rotation.eulerAngles.y);
        //}

        //private void MoveProcess(HeliSettingsData stateData)
        //{
        //    var turn = stateData.TurnForce * Mathf.Lerp(hMove.x, hMove.x * (stateData.turnTiltForcePercent - Mathf.Abs(hMove.y)),
        //                   Mathf.Max(0f, hMove.y));
        //    hTurn = Mathf.Lerp(hTurn, turn, Time.fixedDeltaTime * stateData.TurnForce);
        //    stateData.Torque = hTurn * stateData.mass;//todo HelicopterModel.mass;
        //    stateData.FForce = Vector3.forward * Mathf.Max(0f, hMove.y * stateData.ForwardForce * stateData.mass);
        // //   Debug.Log(" stateData.Torque " + stateData.Torque);
        //  //  Debug.Log(" stateData.FForce " + stateData.FForce);
            
        //    //  HelicopterModel.AddRelativeTorque(0f, hTurn * HelicopterModel.mass, 0f);
        //    //  HelicopterModel.AddRelativeForce(Vector3.forward * Mathf.Max(0f, hMove.y * ForwardForce * HelicopterModel.mass));
        //}

        //private void LiftProcess(HeliSettingsData stateData, float positionY)
        //{
        //    var upForce = 1 - Mathf.Clamp(positionY / stateData.EffectiveHeight, 0, 1);
        //    stateData.upForce = Mathf.Lerp(0f, stateData.EngineForce, upForce) * stateData.mass;
        //    //Debug.Log("stateData.upForce " + stateData.upForce);
        //    //  HelicopterModel.AddRelativeForce(Vector3.up * upForce);
        //}

        //private void TiltProcess(HeliSettingsData stateData, float rotationY)
        //{
        //    hTilt.x = Mathf.Lerp(hTilt.x, hMove.x * stateData.TurnTiltForce, Time.deltaTime);
        //    hTilt.y = Mathf.Lerp(hTilt.y, hMove.y * stateData.ForwardTiltForce, Time.deltaTime);
        //    stateData.TiltAngel = Quaternion.Euler(hTilt.y, rotationY, -hTilt.x);
        //    //Debug.Log("stateData.TiltAngel" + stateData.TiltAngel);

        //    //  HelicopterModel.transform.localRotation =
        //    //      Quaternion.Euler(hTilt.y, HelicopterModel.transform.localEulerAngles.y, -hTilt.x);
        //}
    }
}