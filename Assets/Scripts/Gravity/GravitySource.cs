using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GravitySource : MonoBehaviour {

    public delegate void GravityPulseAction(Vector3 pullSource, float gravityPull);
    public GravityPulseAction OnGravityPulse;

    public float GravitationalPullForce = 1;

	// Use this for initialization
	void Start () {
	
	}
	
    void FixedUpdate()
    {
        if(OnGravityPulse != null)
        {
            OnGravityPulse(transform.position, GravitationalPullForce);
        }
    }
    
}
