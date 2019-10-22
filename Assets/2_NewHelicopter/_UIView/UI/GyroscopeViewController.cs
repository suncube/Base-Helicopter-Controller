using UnityEngine;
using UnityEngine.UI;

public class GyroscopeViewController : MonoBehaviour
{
    public Transform TargetPlayer;
    public Text VerticalAngle;
    public Text HorisontalAngle;
    public RectTransform Vertical;
    public RectTransform Horisontal;

	// Update is called once per frame
	void Update () {

        if(TargetPlayer ==null) return;

        Vector3 targetDir = Vector3.up;
        var angleF = Vector3.Angle(targetDir, TargetPlayer.forward) -90f;

        Vertical.localPosition = new Vector3(Horisontal.localPosition.x, angleF * 1.5f, Horisontal.localPosition.z);
	    VerticalAngle.text = string.Format("{0}*", (int) angleF);
        var angleL = Vector3.Angle(targetDir, TargetPlayer.right) - 90f;
	    Horisontal.localRotation = Quaternion.AngleAxis(angleL*-1, new Vector3(0, 0, 1));
        HorisontalAngle.text = string.Format("{0}*", (int)angleL);
	}

    public void SetPlayer(Transform player)
    {
        TargetPlayer = player;
    }

    public void DeletePlayer(Transform player)
    {
        TargetPlayer = null;
    }
}
