using UnityEngine;
using System.Collections;

public class Lerper : MonoBehaviour {

    public Vector3 TargetPosition;
    public float TransitionTime;

	public void StartLerp(Vector3 tPosition)
    {
        TargetPosition = tPosition;
        StartCoroutine(Lerp());
    }

    public void StartLerp()
    {
        StartCoroutine(Lerp());
    }

    private IEnumerator Lerp()
    {
        Vector3 currentPosition = transform.position;
        while(Vector3.Distance(transform.position, TargetPosition) >= 0.01f)
        {
            currentPosition = Vector3.Slerp(currentPosition, TargetPosition, TransitionTime);
            this.transform.position = currentPosition;
            yield return null;
        }
        this.transform.position = TargetPosition;
    }
}
