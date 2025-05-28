using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VolumeManager : Singleton<VolumeManager>
{
    public Slider volumeSlider;
    private const string VolumePrefKey = "MusicVolume";
    private float defaultVolume = 1f;

    protected override void Awake()
    {
        base.Awake();

        SceneManager.sceneLoaded += OnSceneLoaded;

        float savedVolume = PlayerPrefs.GetFloat(VolumePrefKey, defaultVolume);
        ApplyVolumeToAllMusicSources(savedVolume);

        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
            volumeSlider.onValueChanged.AddListener(OnSliderValueChanged);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        float savedVolume = PlayerPrefs.GetFloat(VolumePrefKey, defaultVolume);
        ApplyVolumeToAllMusicSources(savedVolume);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSliderValueChanged(float newVolume)
    {
        PlayerPrefs.SetFloat(VolumePrefKey, newVolume);
        ApplyVolumeToAllMusicSources(newVolume);
    }

    public void ApplyVolumeToAllMusicSources(float volume)
    {
        AudioSource[] allSources = FindObjectsOfType<AudioSource>();

        foreach (AudioSource source in allSources)
        {
            if (source.CompareTag("Audio"))
            {
                source.volume = volume;
            }
        }
    }
}