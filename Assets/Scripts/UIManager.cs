using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField]
    private TMP_Text speed_text;
    [SerializeField]
    private TMP_Text direction_text;
    [SerializeField]
    private GameObject retry_screen;
    [SerializeField]
    private GameObject success_screen;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeSpeedText(string speed)
    {
        speed_text.text = speed;
    }

    public void ChangeDirectionText(string direction)
    {
        direction_text.text = direction;
    }

    public void Retry()
    {
        retry_screen.SetActive(true);
    }

    public void Success()
    {
        success_screen.SetActive(true);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayButtonSFX()
    {
        AudioManager.instance.PlaySfx("Button");
    }
}
