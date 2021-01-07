using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadingGuideFeature : MonoBehaviour
{
    // ui reference
    public GameObject SelectedBorder;
    public GameObject UnselectedBorder;
    public GameObject FeatureSelectedIcon;


    // ui state
    private _modes _activeMode = _modes.unselected;
    private enum _modes
    {
        unselected,
        preset1,
    }

    // reading guide
    private bool _readingGuideActive = false;
    private ReadingGuide _readingGuide;
    private FirstPersonAIO _cameraReference;

    // audio reference
    private AudioManager _audioManager;

    private void ReadingGuide()
    {
        if (_activeMode == _modes.preset1)
        {
            _readingGuide.transform.position = new Vector3(_readingGuide.transform.position.x, Input.mousePosition.y + 15, 0);
            _cameraReference.enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else _cameraReference.enabled = true;
    }


    private void CheckButtonClicked()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ReadingGuideOnClick();
            _readingGuideActive = !_readingGuideActive;
            if (_readingGuideActive)
            {
                SelectMode();
            }
            else SelectMode();
        }
    }

    private void SelectMode()
    {
        switch (_activeMode)
        {
            case _modes.unselected:
                SelectedBorder.SetActive(false);
                UnselectedBorder.SetActive(true);
                FeatureSelectedIcon.SetActive(false);
                _readingGuideActive = false;
                _readingGuide.gameObject.SetActive(false);
                break;
            case _modes.preset1:
                SelectedBorder.SetActive(true);
                UnselectedBorder.SetActive(false);
                FeatureSelectedIcon.SetActive(true);
                _readingGuideActive = true;
                _readingGuide.gameObject.SetActive(true);
                break;
        }
    }

    public void ReadingGuideOnClick()
    {
        _audioManager.Play("featureClick");
        if ((int)_activeMode < 1)
        {
            _activeMode++;
        }
        else _activeMode = 0;

        SelectMode();
    }


    private void Update()
    {
        ReadingGuide();
        CheckButtonClicked();
    }

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _readingGuide = FindObjectOfType<ReadingGuide>();
        _cameraReference = FindObjectOfType<FirstPersonAIO>();
        _readingGuide.gameObject.SetActive(false);
        _audioManager = FindObjectOfType<AudioManager>();
    }
}
