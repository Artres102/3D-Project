using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu :MonoBehaviour
{
    public GameObject PauseMenuu;
    private bool menuActivated = false;
    // Start is called before the first frame update
    void Start()
    {
        PauseMenuu.SetActive(menuActivated);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuActivated = !menuActivated;
            PauseMenuu.SetActive(menuActivated);
            if (menuActivated == true) Cursor.lockState = CursorLockMode.None; else Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = !Cursor.visible;
        }

        //freeze
        if (menuActivated)
        {
            Time.timeScale = 0;
        }
        else if (!menuActivated)
        {
            Time.timeScale = 1;
        }
    }

    public void Resume()
    {
        menuActivated = !menuActivated;
        PauseMenuu.SetActive(menuActivated);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    public void ReturnToMenu()
    {
        Destroy(GameManager.Instance);
        SceneManager.LoadScene(0);
    }
}
