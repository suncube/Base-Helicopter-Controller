using System;
using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour
{

    public Action CheckpointActivated; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnTriggerEnter()
    {
        Debug.Log("Activate");
        if (CheckpointActivated != null)
            CheckpointActivated();
    }
}
