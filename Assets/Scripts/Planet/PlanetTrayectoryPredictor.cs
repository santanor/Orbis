using UnityEngine;
using System.Collections;

public class PlanetTrayectoryPredictor : MonoBehaviour {

    public enum PlanetTrayectoryPredictorStatusEnum {Predicting, stopped }

    public int PredictionPoints = 20;
    public float PredictionInterval = 0.2f;

    private PlanetTrayectoryPredictorStatusEnum status;
    public LineRenderer lineRenderer;
    int maxCount  = 20;
    int simplify = 5;

	// Use this for initialization
	void Start () {
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
            lineRenderer = this.gameObject.AddComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	public void StartPredicting()
    {
        this.status = PlanetTrayectoryPredictorStatusEnum.Predicting;
    }

    public void StopPredicting()
    {
        this.status = PlanetTrayectoryPredictorStatusEnum.stopped;
    }

    public void Predict(Vector3 gravityPull, float force)
    {
        

    }

    Vector3[] TrajectoryPrediction(int points, float timeInterval, Vector3 gravityPullDirection, float force)
    {
        Vector3[] final = new Vector3[points];
        final[0] = this.transform.parent.position;
        float currentTime = 0;

        for(int i = 1; i < points; i++)
        {
            float X = final[i-1].x * GetPredictedPossition(final[i-1], force);
            float Y = final[i-1].y * GetPredictedPossition(final[i-1], force) + (force * (currentTime*currentTime))/2;
            float Z = 1;

            currentTime += timeInterval;
            final[i] = new Vector3(X, Y, Z);
        }
        return final;
        //Vector3 considVel = gravityPullDirection.normalized ;//You have this from the impulse force you would add

        //for (int i = 1; i < points; i++)
        //{

        //    final[i] = final[i - 1] + considVel * timeInterval + (GetPredictedPossition(final[i - 1], force) * GetPredictedPossition(final[i-1], force)) * gravityPullDirection * timeInterval;
        //    //g should be a method like your RealGravity used only for this prediction. It will use final[i-1] as      the object's position for the distance and it will be without rb.mass as it will be canceled out when a=F/mass

        //    considVel = considVel + GetGravityPredictedPull(gravityPullDirection, force) * timeInterval;

        //}
        //return final;
    }

    float GetPredictedPossition(Vector3 predictedObjectPossition, float force)
    {
        Vector3 pullDirection = predictedObjectPossition  - transform.parent.position;
        pullDirection.z = 0;
        float magsqr = pullDirection.sqrMagnitude;
        if (magsqr == 0)
            return 0;
        var result = force  / magsqr;
        return result;
    }

    Vector3 GetGravityPredictedPull(Vector3 pullSource, float gravityPull)
    {
        Vector3 pullDirection = pullSource - transform.position;
        //we're doing 2d physics, so don't want to try and apply z forces!
        pullDirection.z = 0;

        //get the squared distance between the objects
        float magsqr = pullDirection.sqrMagnitude;
        if (magsqr == 0)
            return Vector3.zero;
        return gravityPull * pullDirection.normalized / magsqr;
    }
}
