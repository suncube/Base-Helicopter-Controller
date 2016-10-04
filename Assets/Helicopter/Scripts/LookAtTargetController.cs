using UnityEngine;
using System.Collections;

public class LookAtTargetController : MonoBehaviour
{
    public Transform Target;
    public bool smooth = true;
    public float damping = 6.0f;

	// Use this for initialization
	void Start () {
	
	}
	
    void LateUpdate()
    {
        if (Target!=null)
        {
            if (smooth)
            {
                // Look at and dampen the rotation
                var rotation = Quaternion.LookRotation(Target.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
            }
            else
            {
                transform.LookAt(Target);
            }
        }
    }
}
