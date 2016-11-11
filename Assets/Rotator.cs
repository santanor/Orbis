using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

    public bool IsRotating;
    public bool RotateOnAwake;

    public float PitchPerSecond;
    public float YawPerSecond;
    public float RollPerSecond;

	// Use this for initialization
	void Start () {
        if (RotateOnAwake)
            IsRotating = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (IsRotating)
        {
            var pitchThisFrame = PitchPerSecond  *Time.deltaTime;
            var YawThisFrame = YawPerSecond *  Time.deltaTime;
            var rollThisFrame= RollPerSecond *  Time.deltaTime;
            this.transform.Rotate(rollThisFrame, pitchThisFrame, YawThisFrame);
        }
	}
}
