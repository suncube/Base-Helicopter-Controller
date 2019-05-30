using System;
using UnityEngine;

//rename
public class HeliRotorController : MonoBehaviour
{
	public enum Axis
	{
		X,
		Y,
		Z,
	}

	public bool InverceRotate = false;
	public Axis RotateAxis;
    private float _rotarSpeed;
    public float RotarSpeed
    {
        get { return _rotarSpeed; }
        set { _rotarSpeed = Mathf.Clamp(value,0,3000); }
    }

    private float rotateDegree;
    private Vector3 OriginalRotate;

    void Start ()
	{
        OriginalRotate = transform.localEulerAngles;
	}

	void Update ()
	{
		if(InverceRotate)
        	rotateDegree -= RotarSpeed * Time.deltaTime;
		else
			rotateDegree += RotarSpeed * Time.deltaTime;

		    rotateDegree = rotateDegree%360;

		switch (RotateAxis)
		{
		    case Axis.Y:
		        transform.localRotation = Quaternion.Euler(OriginalRotate.x, rotateDegree, OriginalRotate.z);
		        break;
		    case Axis.Z:
		        transform.localRotation = Quaternion.Euler(OriginalRotate.x, OriginalRotate.y, rotateDegree);
		        break;
		    default:
		        transform.localRotation = Quaternion.Euler(rotateDegree, OriginalRotate.y, OriginalRotate.z);
		        break;
		}
	}
}
