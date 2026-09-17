using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        AudioManager.instance.PlayMusic();
    }
    public void Play()
    {
        SceneManager.LoadScene("SpacecraftSelection");
    }
}
