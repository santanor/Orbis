using UnityEngine;
using System.Collections;
using Assets.Scripts.Planet;

public class SlingshotManager : MonoBehaviour {

    public delegate void PlanetSpawnedAction(Planet planet);

    public GameObject[] Planets;
    public PlanetSpawnedAction OnPlanetSpawned { get; set; }
    private GameObject currentPlanet;
    private Vector3 sourcePosition;
    public float GravityForceMultiplier = 5000;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.touchCount >= 1)
        {
            //Always get the first one. Obviate the rest
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                currentPlanet = SpawnPlanetOnScreenLocation(touch.position);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                ReleasePlanet(touch.position);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            currentPlanet = SpawnPlanetOnScreenLocation(Input.mousePosition);
        }
        if(Input.GetMouseButtonUp(0))
        {
            ReleasePlanet(Input.mousePosition);
        }
        if (Input.GetMouseButton(0))
        {
            PredictTrayectory(Input.mousePosition);
        }
	}

    private GameObject SpawnPlanetOnScreenLocation(Vector2 screenLocation)
    {
        var spawnTransform = Camera.main.ScreenToWorldPoint(screenLocation);
        spawnTransform = spawnTransform + new Vector3(0, 0, 1);
        sourcePosition = spawnTransform;
        GameObject planet = (GameObject)Instantiate(Planets[Random.Range(0, Planets.Length)], spawnTransform, Quaternion.identity);
        if(OnPlanetSpawned != null)
        {
            OnPlanetSpawned(planet.GetComponent<Planet>());
        }
        return planet;
    }

    private void ReleasePlanet(Vector2 screenLocation)
    {

        var target = Camera.main.ScreenToWorldPoint(screenLocation);
        target = target + new Vector3(0, 0, 1);
        var forceDirection = sourcePosition - target;
        float force = Vector3.Distance(sourcePosition, target);
        currentPlanet.GetComponent<Planet>().ReleasePlanet(forceDirection, force * GravityForceMultiplier);
        currentPlanet = null;
    }

    private void PredictTrayectory (Vector2 screenLocation)
    {
        if(currentPlanet != null)
        {
            var target = Camera.main.ScreenToWorldPoint(screenLocation);
            target = target + new Vector3(0, 0, 1);
            var forceDirection = sourcePosition - target;
            float force = Vector3.Distance(sourcePosition, target);
            currentPlanet.GetComponent<Planet>().PredictTrayectory(forceDirection, force*GravityForceMultiplier);
        }
    }

}
