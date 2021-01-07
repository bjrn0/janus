using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigCursorFeature : MonoBehaviour
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

    // cursor settings
    private Texture2D _cursorTexture;
    private CursorMode _cursorMode = CursorMode.Auto;
    private Vector2 _cursorOffset = Vector2.zero;

    // audio reference
    private AudioManager _audioManager;

    private void Preset1()
    {
        LoadPreset1();
    }

    private void LoadPreset1()
    {
        Cursor.SetCursor(_cursorTexture, _cursorOffset, _cursorMode);
        Debug.Log(_cursorTexture);
    }

    private void ResetCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    

    public void BigCursorOnClick()
    {
        _audioManager.Play("featureClick");
        if ((int)_activeMode < 1)
        {
            _activeMode++;
        }
        else _activeMode = 0;

        switch (_activeMode)
        {
            case _modes.unselected:
                SelectedBorder.SetActive(false);
                UnselectedBorder.SetActive(true);
                FeatureSelectedIcon.SetActive(false);
                ResetCursor();
                break;
            case _modes.preset1:
                SelectedBorder.SetActive(true);
                UnselectedBorder.SetActive(false);
                FeatureSelectedIcon.SetActive(true);
                Preset1();
                break;
        }
    }

    private void Awake()
    {
        _cursorTexture = Resources.Load<Texture2D>("Icons/cursorIcon");
        _audioManager = FindObjectOfType<AudioManager>();
    }
}
