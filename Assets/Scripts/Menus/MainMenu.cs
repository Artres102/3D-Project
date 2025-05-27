using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public GameObject QuitMenu;
    private bool menuActivated = false;
    // Start is called before the first frame update
    void Start()
    {
        QuitMenu.SetActive(menuActivated);
    }

    public void Resume()
    {
        menuActivated = !menuActivated;
        QuitMenu.SetActive(menuActivated);
    }
    public void Newgame()
    {
        SceneManager.LoadScene(1);
    }
    public void Quitgame()
    {
        Application.Quit();
    }
    
    public void QuitConfirm()
    {
        menuActivated = !menuActivated;
        QuitMenu.SetActive(menuActivated);
    }
}
