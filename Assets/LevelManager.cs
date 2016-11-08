using UnityEngine;
using System.Collections;
using Assets.Scripts.Planet;

public class LevelManager : MonoBehaviour {

    public enum LevelStarsEnum { OneStar, TwoStars, ThreeStars}

    public delegate void LevelBeganAction(int pointsCount);
    public delegate void LevelEndedAction(LevelStarsEnum stars);

    public LevelBeganAction OnLevelBegan;
    public LevelEndedAction OnLevelEnded;
    public SlingshotManager slingShotManager;
    public int ThreeStarCap;
    public int TwoStarCap;
    private int pointsCount;
    private int planetCount;
    private Point[] points;
    private LevelStarsEnum levelStars;

    // Use this for initialization
    void Start () {
        var points = FindObjectsOfType< Point > ();
        pointsCount = points.Length;
        foreach(var p in points)
        {
            p.OnPointCollected += OnPointCollected;
        }
        slingShotManager.OnPlanetSpawned += OnPlanetSpawned;
        if(OnLevelBegan != null)
        {
            OnLevelBegan(pointsCount);
        }
	
	} 

    private void OnPlanetSpawned(Planet planet)
    {
        planetCount++;
    }

    private void OnPointCollected(Point point, Planet planet)
    {
        pointsCount--;
        if(pointsCount == 0)
        {
            if (planetCount <= ThreeStarCap)
            {
                levelStars = LevelStarsEnum.ThreeStars;
            }
            else if (planetCount <= TwoStarCap)
            {
                levelStars = LevelStarsEnum.TwoStars;
            }
            else
                levelStars = LevelStarsEnum.OneStar;

            if (OnLevelEnded != null)
                OnLevelEnded(levelStars);
        }
    }
}
