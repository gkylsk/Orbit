using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private Sprite[] spacecraftSprite;
    [SerializeField]
    private SpacecraftController spacecraft;
    [SerializeField]
    private Planet[] planets;
    [SerializeField]
    private int destinationIndex;
    private int currentPlanetIndex = 0;

    private void Start()
    {
        int selected = PlayerPrefs.GetInt("SelectedSpacecraft", 0);
        if(selected >=0 && selected < spacecraftSprite.Length)
        {
            spacecraft.GetComponentInChildren<SpriteRenderer>().sprite = spacecraftSprite[selected];
        }
    }

    public void ReachPlanet(Planet planet)
    {
        for (int i = 0; i < planets.Length; i++)
        {
            if (planets[i] == planet)
            {
                currentPlanetIndex = i;
                break;
            }
        }


        if (currentPlanetIndex == destinationIndex)
        {
            LevelComplete();
            return;
        }

        Debug.Log("planet "+ currentPlanetIndex);
    }

    private void LevelComplete()
    {
        spacecraft.Stop();
        Debug.Log(currentPlanetIndex);
        AudioManager.instance.PlaySfx("Success");
        UIManager.instance.Success();
    }

    public void NextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if(nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
