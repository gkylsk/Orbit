using UnityEngine;
using UnityEngine.SceneManagement;

public class SpacecraftSelection : MonoBehaviour
{
    private int selectedSpacecraft = 0;

    public void SelectedSpacecraft(int index)
    {
        Debug.Log(index);
        AudioManager.instance.PlaySfx("Button");
        selectedSpacecraft = index;
    }

    public void Continue()
    {
        AudioManager.instance.PlaySfx("Button");
        PlayerPrefs.SetInt("SelectedSpacecraft", selectedSpacecraft);
        PlayerPrefs.Save();

        SceneManager.LoadScene("Level1");
    }
}
