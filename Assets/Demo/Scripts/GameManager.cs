using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // menu
    private bool _menuActive = false;
    [SerializeField]
    private GameObject _menuBox;

    // controller
    [SerializeField]
    private GameObject _controller;
    [SerializeField]
    private GameObject _controllerCamera;
    private int _cameraRotationAngle = -94;
    private FirstPersonAIO _controllerSettings;

    // spawn points
    [SerializeField]
    private GameObject _spawnRed;
    [SerializeField]
    private GameObject _spawnGreen;
    [SerializeField]
    private GameObject _spawnBlue;
    [SerializeField]
    private GameObject _spawnFonts;
    [SerializeField]
    private GameObject _spawnEffects;

    // audio reference
    private AudioManager _audioManager;

    private void Awake()
    {
        _controllerSettings = _controller.GetComponent<FirstPersonAIO>();
        _audioManager = FindObjectOfType<AudioManager>();
    }


    void Start()
    {
        _menuBox.SetActive(false);
        _audioManager.Play("ambience");
    }


    private void Update()
    {
        ToggleMenu();
        QuitDemo();
        SelectSpawnPoint();
    }

    private void FixedUpdate()
    {
        
    }

    private void QuitDemo()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void ToggleMenu()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            // triggers the menu
            _menuActive = !_menuActive;
            if (_menuActive)
            {
                _menuBox.SetActive(true);
            }
            else _menuBox.SetActive(false);
        }
    }

    private void SelectSpawnPoint()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _controller.transform.position = _spawnRed.transform.position;
            _controllerSettings.RotateCamera(new Vector2(0, _cameraRotationAngle), true);
        }

        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            _controller.transform.position = _spawnGreen.transform.position;
            _controllerSettings.RotateCamera(new Vector2(0, _cameraRotationAngle), true);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _controller.transform.position = _spawnBlue.transform.position;
            _controllerSettings.RotateCamera(new Vector2(0, _cameraRotationAngle), true);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _controller.transform.position = _spawnFonts.transform.position;
            _controllerSettings.RotateCamera(new Vector2(0, _cameraRotationAngle), true);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            _controller.transform.position = _spawnEffects.transform.position;
            _controllerSettings.RotateCamera(new Vector2(0, _cameraRotationAngle), true);
        }
    }
}
