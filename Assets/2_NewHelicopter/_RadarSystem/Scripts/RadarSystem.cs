using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace SunCube.Radar
{
    public class RadarSystem : MonoBehaviour
    {
        public static RadarSystem runtime;

        public Transform PlayerTarget;
        public Transform PlayerCamera;
        [Header("View")]
        public CanvasScaler RadarCanvas;
        public float ScaleFactor = 3f;

        [Header("")]
        public float RadarDistance;

        [Header("[View Settings]")]
        public Image Backgroung;
        public Transform PlayerView;
        public Transform Compass;

        [Header("[Service Settings]")]
        public RectTransform EnemyTarget;
        public RectTransform EnemyPoint;
        public Transform Root;

        [Header("[Target Type Icons]")]
        public RadarTypeInfo[] RadarTypeInfo;
        //
        private Dictionary<GameObject, EnemyMap> EnemyList = new Dictionary<GameObject, EnemyMap>();


        private void Awake()
        {
            runtime = this;
            if (!PlayerCamera)
                PlayerCamera = Camera.main.transform;

            RadarCanvas.scaleFactor = ScaleFactor;
        }
        private void FixedUpdate()
        {
            if (PlayerTarget == null) return;

            transform.eulerAngles = new Vector3(90f, PlayerTarget.eulerAngles.y, 0f);
            RadarDraw();
        }

        public void AddTarget(GameObject enemy)
        {
            if(enemy == null) return;

            AddTarget(enemy.gameObject, RadarTargetType.Simple);
        }
        public void AddTarget(RadarItem enemy)
        {
            if (enemy == null) return;

            AddTarget(enemy.gameObject,enemy.TargetType);
        }
        private void AddTarget(GameObject enemy, RadarTargetType type)
        {
            if (enemy == null) return;
            if (EnemyList.ContainsKey(enemy)) return;

            var enemyInfo = CreateEnemyInfo(type);

            EnemyList.Add(enemy, enemyInfo);
            enemy.GetComponent<AdventureBehaviour>().OnDestroyAction += OnEnemyDestroy;
        }
    
        private EnemyMap CreateEnemyInfo(RadarTargetType type)
        {
            var enemyInfo = new EnemyMap
            {
                EnemyArrow = (RectTransform) Instantiate(EnemyTarget, new Vector3(0, 0, 0), Quaternion.identity),
                EnemyPoint = (RectTransform)Instantiate(EnemyPoint, new Vector3(0, 0, 0), Quaternion.identity)
            };
            var radarTypeInfo = GetIconInfo(type);

            enemyInfo.EnemyArrow.transform.parent = Root;
            enemyInfo.EnemyArrow.localPosition = new Vector3(0, 0, 0);
            enemyInfo.EnemyArrow.GetComponent<Image>().sprite = radarTypeInfo.ArrowIcon;

            enemyInfo.EnemyPoint.transform.parent = Root;
            enemyInfo.EnemyPoint.localPosition = new Vector3(0, 0, 0);
            enemyInfo.EnemyPoint.GetComponent<Image>().sprite = radarTypeInfo.Icon;

            return enemyInfo;
        }
        private RadarTypeInfo GetIconInfo(RadarTargetType type)
        {
            for (int i = 0; i < RadarTypeInfo.Length; i++)
            {
                if (RadarTypeInfo[i].Type == type)
                    return RadarTypeInfo[i];
            }

            return RadarTypeInfo[0];
        }

        private void RadarDraw()
        {
            PlayerView.localRotation = Quaternion.AngleAxis(PlayerCamera.eulerAngles.y - PlayerTarget.eulerAngles.y, new Vector3(0, 0, -1));
            Compass.localRotation = Quaternion.AngleAxis(transform.eulerAngles.y, new Vector3(0, 0, 1));
            GetComponent<Camera>().rect = new Rect(0, 0, 200f / Screen.width, 200f / Screen.height);

            foreach (var enemy in EnemyList)
            {
                transform.position = new Vector3(PlayerTarget.position.x, enemy.Key.transform.position.y + RadarDistance, PlayerTarget.position.z);
                if (Vector3.Distance(transform.position, enemy.Key.transform.position) < RadarDistance + RadarDistance * 0.05f)
                {
                    var screenPos = GetComponent<Camera>().WorldToScreenPoint(enemy.Key.transform.position);

                    var size = Backgroung.rectTransform.sizeDelta.x;
                    screenPos = new Vector3((screenPos.x - size) / 2, (screenPos.y - size) / 2); // anchor back in center
                    enemy.Value.EnemyPoint.localPosition = new Vector3(screenPos.x, screenPos.y);

                    enemy.Value.EnemyArrow.gameObject.SetActive(false);
                    enemy.Value.EnemyPoint.gameObject.SetActive(true);

                }
                else
                {
                    var olRot = transform.eulerAngles;
                    transform.LookAt(enemy.Key.transform.position);
                    enemy.Value.EnemyArrow.localRotation = Quaternion.AngleAxis(transform.eulerAngles.y - PlayerTarget.eulerAngles.y, new Vector3(0, 0, -1));
                    transform.eulerAngles = olRot;

                    enemy.Value.EnemyArrow.gameObject.SetActive(true);
                    enemy.Value.EnemyPoint.gameObject.SetActive(false);
                }

            }
        }

        private void OnEnemyDestroy(AdventureBehaviour obj)
        {
            EnemyMap enemyInfo;
            obj.OnDestroyAction -= OnEnemyDestroy;

            if (EnemyList.TryGetValue(obj.gameObject, out enemyInfo))
            {
                EnemyList.Remove(obj.gameObject);

                if (enemyInfo.EnemyPoint != null)
                    Destroy(enemyInfo.EnemyPoint.gameObject);

                if (enemyInfo.EnemyArrow != null)
                    Destroy(enemyInfo.EnemyArrow.gameObject);
            }


        }

        #region IPlayerGhost
        public void SetPlayer(Transform player)
        {
            transform.parent.gameObject.SetActive(true);
            PlayerTarget = player.transform;
        }

        public void DeletePlayer(Transform player)
        {
            transform.parent.gameObject.SetActive(false);
            PlayerTarget = null;
        }
        #endregion IPlayerGhost
    }

    class EnemyMap
    {
        public RectTransform EnemyPoint;
        public RectTransform EnemyArrow;
    }
}
