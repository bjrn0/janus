using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accessibility : MonoBehaviour
{

    // plugin active state
    private bool _pluginActive = false;

    // plugin reference 
    [SerializeField]
    private GameObject _plugin;
    [SerializeField]
    private GameObject _pluginIcon;

    // controller reference
    [SerializeField]
    private GameObject _controller;
    private FirstPersonAIO _controllerScript;

    // audio reference
    private AudioManager _audioManager;

    public void AccessibilityIconOnClick()
    {
        _pluginActive = !_pluginActive;
    }

    public void ToggleAccessibilityPlugin()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            _pluginActive = !_pluginActive;       
        }
    }

    public void PluginCloseButtonOnClick()
    {
        _pluginActive = !_pluginActive;
    }

    private void CheckPluginActive()
    {
        if (_pluginActive)
        {
            _plugin.SetActive(true);
            _pluginIcon.SetActive(false);
            _controllerScript.enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            _plugin.SetActive(false);
            _pluginIcon.SetActive(true);
            _controllerScript.enabled = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }


    public void GuideOnClick()
    {
        _audioManager.Play("projectJanusGuide");
    }

    public void HelpOnClick()
    {
        Application.OpenURL("https://www.youtube.de");
    }
    
    public void FeedbackOnClick()
    {
        Application.OpenURL("https://www.google.de");
    }

    void Awake()
    {
        _controllerScript = _controller.GetComponent<FirstPersonAIO>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _audioManager = FindObjectOfType<AudioManager>();
    }

 
    void Update()
    {
        CheckPluginActive();
        ToggleAccessibilityPlugin();
    }
}
