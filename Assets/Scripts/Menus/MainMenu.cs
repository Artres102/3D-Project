using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        AudioManager.Instance.Play(AudioManager.SoundType.Music_Menu);
    }
    public void Newgame()
    {
        SceneManager.LoadScene(1);
    }
    public void Quitgame()
    {
        Application.Quit();
    }
}
