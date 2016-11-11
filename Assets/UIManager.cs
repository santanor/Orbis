using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Scripts.Planet;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public Text ScoreCounter;
    private int planetCounter;
    public SlingshotManager slingshotManager;
    public LevelManager levelManager;
    public GameObject LevelEndPanel;
    public GameObject FirstStar;
    public GameObject SecondStar;
    public GameObject ThirdStar;
    public GameObject NextLevelButton;
    public Text LevelText;
    private int CurrentLevel;

	// Use this for initialization
	void Start () {
        slingshotManager.OnPlanetSpawned += OnPlanetSpawned;
        levelManager.OnLevelEnded += OnLevelEnded;
        CurrentLevel = SceneManager.GetActiveScene().buildIndex;
	}
	
    private void OnPlanetSpawned(Planet planet)
    {
        planetCounter++;
        ScoreCounter.text = planetCounter.ToString();
    }

    private void OnLevelEnded(LevelManager.LevelStarsEnum stars)
    {
        slingshotManager.gameObject.SetActive(false);
        StartCoroutine(LevelEnded(stars));
    }

    private IEnumerator LevelEnded(LevelManager.LevelStarsEnum stars)
    {
        yield return new WaitForSeconds(1.5f);
        LevelEndPanel.SetActive(true);
        LevelText.text = "Level " + SceneManager.GetActiveScene().buildIndex + " cleared ";  
        
        if(stars == LevelManager.LevelStarsEnum.ZeroStars)
        {
            NextLevelButton.GetComponent<Button>().interactable = false;
            var txt = NextLevelButton.GetComponentInChildren<Text>();
            txt.text = "Get at least one star";
            txt.resizeTextForBestFit = true;
        }
        else if(stars == LevelManager.LevelStarsEnum.OneStar)
        {
            FirstStar.SetActive(true);
        }
        else if (stars == LevelManager.LevelStarsEnum.TwoStars)
        {
            FirstStar.SetActive(true);
            SecondStar.SetActive(true);
        }
        else if (stars == LevelManager.LevelStarsEnum.ThreeStars)
        {
            FirstStar.SetActive(true);
            SecondStar.SetActive(true);
            ThirdStar.SetActive(true);
        }
    }

    public void MainMenu()
    {
        Application.Quit();
    }
    
    public void NextLevel()
    {
        SceneManager.LoadScene(CurrentLevel + 1); 
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(CurrentLevel);
    }
}
