using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Toolbar : MonoBehaviour
{
    private SceneController sceneController;
    private Button Button_Earth;

    private void Awake()
    {
        sceneController = FindObjectOfType<SceneController>();
        Button_Earth = transform.Find("Button_Earth").GetComponent<Button>();

    }
    private void Start()
    {
        Button_Earth.onClick.AddListener(sceneController.LoadScene);
    }
}
