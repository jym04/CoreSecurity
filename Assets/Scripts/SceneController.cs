using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using WI;

public class SceneController : MonoBehaviour
{
    public LoadSceneOption sceneOption;

    private OrbitalController cameraController;
    private FadeEffect fadeEffct;

    private void Start()
    {
        cameraController = FindObjectOfType<OrbitalController>();
        fadeEffct = FindObjectOfType<FadeEffect>();

        StartCoroutine(fadeEffct.FadeOut());
    }

    public void LoadScene()
    {
        StartCoroutine(LoadNextScene());
    }

    private IEnumerator LoadNextScene()
    {
        yield return StartCoroutine(fadeEffct.FadeIn());
        SceneManager.LoadScene(sceneOption.nextScene);

        cameraController.Rewind();
    }
}
