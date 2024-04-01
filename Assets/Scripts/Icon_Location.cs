using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Icon_Location : MonoBehaviour
{
    public List<Image> images;
    private Button button;
    private SceneController sceneController;

    private void Awake()
    {
        images = transform.GetComponentsInChildren<Image>().ToList();
        button = transform.GetComponentInChildren<Button>();

        sceneController = FindObjectOfType<SceneController>();

        button.onClick.AddListener(sceneController.LoadScene);
    }
}
