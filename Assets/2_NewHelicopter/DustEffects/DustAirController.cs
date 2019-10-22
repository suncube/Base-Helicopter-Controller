using UnityEngine;

namespace NewHelicopter
{
    public class DustAirController: MonoBehaviour
    {
        public ParticleSystem DustParticleSystem;
        public float MaxDustDistance = 10f;

        [HideInInspector]
        public float _intensity = 1f;
        public float Intensity
        {
            get { return _intensity; }
            set
            {
                _intensity = Mathf.Clamp01(value);
                color.a = _intensity;
            }
        }

        [HideInInspector]
        public float _sensitive = 0.2f;

        public float Sensitive
        {
            get { return _sensitive; }
            set { _sensitive = Mathf.Clamp01(value); }
        }

        private Color color;
        private float engine;
        private void Start()
        {
        }
        public void ProgressEngineValue(float engineValue)
        {
            engine = engineValue;
        }

        [System.Obsolete]
        public void VisualizeDustGround(float distance, Vector3 point)
        {
            //DustParticleSystem.gameObject.SetActive(distance < MaxDustDistance);

            //if (DustParticleSystem.gameObject.activeSelf)
            //{
                DustParticleSystem.transform.position = point;
                var distanceFactor = Mathf.Clamp01(distance / MaxDustDistance);
                
                DustParticleSystem.startSpeed = 1-distanceFactor;
                DustParticleSystem.startLifetime = (1-distanceFactor)*2;
                // var c = color;
                // c.a = Mathf.Lerp(c.a * engine, c.a * Sensitive, distanceFactor);
                // DustParticleSystem.startColor = c;
            //}

        }
    }
}