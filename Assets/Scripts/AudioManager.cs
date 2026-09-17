using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private AudioSource musicSource;
    [SerializeField]
    private AudioSource sfxSource;
    [SerializeField]
    private AudioClip musicClip;
    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider sfxSlider;


    private SoundEffectLibrary soundEffectLibrary;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        soundEffectLibrary = GetComponent<SoundEffectLibrary>();
    }

    public void PlayMusic()
    {
        if (musicSource.isPlaying)
            return;
        musicSource.clip = musicClip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySfx(string name)
    {
        AudioClip clip = soundEffectLibrary.GetAudioClip(name);
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip, 2f);
        }
    }

    public void SetMusicVolume()
    {
        SetVolume(musicSlider.value, "MusicVolume");
    }

    public void SetSFXVolume()
    {
        SetVolume(sfxSlider.value, "SFXVolume");
    }

    private void SetVolume(float volume, string volumeName)
    {
        PlayerPrefs.SetFloat(volumeName, volume);
        PlayerPrefs.Save();

        if (volume <= 0.001f)
        {
            audioMixer.SetFloat(volumeName, -80f);
            return;
        }
        audioMixer.SetFloat(volumeName, Mathf.Log10(volume) * 20f);
        Debug.Log(audioMixer.GetFloat(volumeName, out volume));
    }
    
}
