using UnityEngine;
using UnityEngine.UI;

namespace NewHelicopter
{
    // USING
    // Input.GetAxis
    //
    public class HelicopterController : MonoBehaviour
    {
        private string horizontalAxis = "Horizontal";
        private string verticalAxis = "Vertical";
        private string jumpButton = "Jump";

        [Header("Inputs")]
        public bool isVirtualJoystick = false;

        [Header("View")]
        // to helicopter model
        public AudioSource HelicopterSound;
        public Rigidbody HelicopterModel;
        public HeliRotorController MainRotorController;
        public HeliRotorController SubRotorController;
        public DustAirController DustAirController;

        [Header("Fly Settings")]
        public LayerMask GroundMaskLayer = 1;
        public float TurnForce = 3f;
        public float ForwardForce = 10f;
        public float ForwardTiltForce = 20f;
        public float TurnTiltForce = 30f;
        public float EffectiveHeight = 100f;

        public float turnTiltForcePercent = 1.5f;
        public float turnForcePercent = 1.3f;

        private float _engineForce;
        public float EngineForce
        {
            get { return _engineForce; }
            set
            {
                MainRotorController.RotarSpeed = value * 80;
                SubRotorController.RotarSpeed = value * 40;
                HelicopterSound.pitch = Mathf.Clamp(value / 40, 0, 1.2f);
                if (UIGameController.runtime != null && UIGameController.runtime.EngineForceView != null)
                    UIGameController.runtime.EngineForceView.text = string.Format("Engine value [ {0} ] ", (int)value);

                _engineForce = value;
            }
        }

        private float distanceToGround ;
        public float DistanceToGround
        {
            get {return distanceToGround; }
        }

        private Vector3 pointToGround;
        public Vector3 PointToGround
        {
            get { return pointToGround; }
        }


        private Vector2 hMove = Vector2.zero;
        private Vector2 hTilt = Vector2.zero;
        private float hTurn = 0f;
        public bool IsOnGround = true;

        void FixedUpdate()
        {
            ProcessingInputs();
            LiftProcess();
            MoveProcess();
            TiltProcess();

            Visualize();
        }

        private void MoveProcess()
        {
            var turn = TurnForce * Mathf.Lerp(hMove.x, hMove.x * (turnTiltForcePercent - Mathf.Abs(hMove.y)), Mathf.Max(0f, hMove.y));
            hTurn = Mathf.Lerp(hTurn, turn, Time.fixedDeltaTime * TurnForce);
            HelicopterModel.AddRelativeTorque(0f, hTurn * HelicopterModel.mass, 0f);
            HelicopterModel.AddRelativeForce(Vector3.forward * Mathf.Max(0f, hMove.y * ForwardForce * HelicopterModel.mass));
        }

        private void LiftProcess()
        {
            // to ground distance
            RaycastHit hit;
            var direction = transform.TransformDirection(Vector3.down);
            var ray = new Ray(transform.position, direction);
            if (Physics.Raycast(ray, out hit, 300, GroundMaskLayer))
            {
                Debug.DrawLine(transform.position, hit.point, Color.cyan);
                distanceToGround = hit.distance;
                pointToGround = hit.point;

                //isOnGround = hit.distance < 2f;
            }

            var upForce = 1 - Mathf.Clamp(HelicopterModel.transform.position.y / EffectiveHeight, 0, 1);
            upForce = Mathf.Lerp(0f, EngineForce, upForce) * HelicopterModel.mass;
            HelicopterModel.AddRelativeForce(Vector3.up * upForce);
        }

        private void TiltProcess()
        {
            hTilt.x = Mathf.Lerp(hTilt.x, hMove.x * TurnTiltForce, Time.deltaTime);
            hTilt.y = Mathf.Lerp(hTilt.y, hMove.y * ForwardTiltForce, Time.deltaTime);
            HelicopterModel.transform.localRotation = Quaternion.Euler(hTilt.y, HelicopterModel.transform.localEulerAngles.y, -hTilt.x);
        }

        private void ProcessingMobileInputs()
        {
            if (!IsOnGround)
            {
                hMove.x = Input.GetAxis( horizontalAxis);
                hMove.y = Input.GetAxis( verticalAxis);
            }
            else
            {
                hMove.x = 0;
                hMove.y = 0;
            }

            if (Input.GetAxis(jumpButton) > 0)
            {
                EngineForce += 0.1f;
            }
            else
            if (Input.GetAxis(jumpButton) < 0)
            {
                EngineForce -= 0.12f;
            }
        }


        private void ProcessingInputs()
        {
            if (!IsOnGround)
            {
                hMove.x = GetInput( horizontalAxis);
                hMove.y = GetInput( verticalAxis);
            }

            if (GetInput(jumpButton) > 0)
                EngineForce += 0.1f;
            else
            if (GetInput(jumpButton) < 0)
                EngineForce -= 0.12f;

        }

        private float GetInput(string input)
        {
            if(isVirtualJoystick)
                return SimpleInput.GetAxis(input);
            else
                return Input.GetAxis(input);
        }
        

        private void OnCollisionEnter()
        {
            IsOnGround = true;
        }

        private void OnCollisionExit()
        {
            IsOnGround = false;
        }

//TODO temp move to events
        private void Visualize()
        {
            if (DustAirController != null)
            {
                DustAirController.ProgressEngineValue(EngineForce);
                DustAirController.VisualizeDustGround(DistanceToGround, PointToGround);
            }

            if (UIViewController.runtime.HeigthUpView != null)
                UIViewController.runtime.HeigthUpView.text = string.Format("To Ground [ {0} ] m",
                    (int)DistanceToGround);

            if (UIViewController.runtime.EngineForceView != null)
                UIViewController.runtime.EngineForceView.text = string.Format("Engine [ {0} ] ", (int)EngineForce);

            // if (UIViewController.runtime.UpDragView != null)
            //     UIGameController.runtime.UpDragView.text = string.Format("Up Force [ {0} ] f",
            //         (int)CurrentHeightForce);

            if (UIViewController.runtime.HeigthView != null)
                UIViewController.runtime.HeigthView.text = string.Format("Heigth  [ {0} ] m", (int)transform.position.y);
        }

    }
}