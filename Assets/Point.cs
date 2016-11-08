using UnityEngine;
using System.Collections;
using Assets.Scripts.Planet;

public class Point : MonoBehaviour {


    public delegate void PointCollectedAction(Point self, Planet collector);

    private Vector3 originalPosition;
    public PointCollectedAction OnPointCollected { get; set; }
    public Lerper Lerper;
    public ParticleSystem CollectedParticles;

	// Use this for initialization
	void Start () {
        this.originalPosition= transform.position;	
	}
	
    void OnTriggerEnter2D(Collider2D coll)
    {
        Planet planet = coll.gameObject.GetComponent<Planet>();
        if (planet != null && !coll.usedByEffector)
        {
            if (OnPointCollected != null)
            {
                OnPointCollected(this, planet);                
            }
            CollectedParticles.Emit(30);
            GetComponent<SpriteRenderer>().enabled = false;
            Destroy(this.gameObject,0.2f);
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.usedByEffector)
        {
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            Lerper.StartLerp(originalPosition);
        }
    }
}
