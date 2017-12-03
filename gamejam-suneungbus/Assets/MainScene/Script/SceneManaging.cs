using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManaging : MonoBehaviour
{
    private static SceneManaging instance;
    public static SceneManaging GetInstance()
    {
        if (!instance)
        {
            instance = (SceneManaging) FindObjectOfType(typeof(SceneManaging));
            if (!instance)
                Debug.LogError("There needs to be one active SceneManaging script on a SceneManaging in your scene.");
        }

        return instance;
    }

    void Awake()
    {
        DontDestroyOnLoad(instance);
    }

    public void ChangeScene(int num)
    {
        SoundManager.GetInstance().PlayButton();
        switch (num)
        {
            case 0: SceneManager.LoadScene("MainScene"); break;
            case 1: SceneManager.LoadScene("FireScene"); break;
            case 2: SceneManager.LoadScene("WaterFilteringScene"); break;
            case 3: SceneManager.LoadScene("FoodBakingScene"); break;
        }
    }
}
