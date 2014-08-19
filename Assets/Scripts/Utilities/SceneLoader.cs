using UnityEngine;
using System.Collections;

public class SceneLoader : MonoBehaviour
{

    public string sceneName;
    public UIProgressBar bar;
    public float requiredValue = 1f;
    public bool ignoreFirst = true;

    public void LoadScene()
    {
        Application.LoadLevel(sceneName);
    }

    public void OnProgress()
    {
        if (this.ignoreFirst)
        {
            this.ignoreFirst = false;
            return;
        }

        if (this.bar != null && this.bar.value >= this.requiredValue)
            LoadScene();
    }
}
