using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertSpectrumFeature : MonoBehaviour
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

    // audio reference
    private AudioManager _audioManager;

    private Color InvertSpectrum(Renderer obj)
    {
        Color32 _convertedRGBColor = new Color(obj.material.color.r, obj.material.color.g, obj.material.color.b, 1);
        int _r = 255 - _convertedRGBColor.r;
        int _g = 255 - _convertedRGBColor.g;
        int _b = 255 - _convertedRGBColor.b;
        int _a = 1;
        Color _convertedFloatColor = new Color32((byte)_r, (byte)_g, (byte)_b, (byte)_a);
        return _convertedFloatColor;
    }


    private void InvertObjects()
    {
        Object[] _allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in _allObjects)
        {
            FirstPersonAIO _hasControllerScript = obj?.GetComponent<FirstPersonAIO>();
            MeshRenderer _hasMeshRenderer = obj?.GetComponent<MeshRenderer>();
            Ignore _hasIgnoreScript = obj?.GetComponent<Ignore>();
            Renderer _objRenderer = obj?.GetComponent<Renderer>();
            if (_hasControllerScript == null)
            {
                if (_hasIgnoreScript == null)
                {
                    if (_hasMeshRenderer != null && _objRenderer != null)
                    {
                        _objRenderer.material.color = InvertSpectrum(_objRenderer);
                    }
                }
            }
        }
    }



    public void InvertSpectrumOnClick()
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
                InvertObjects();
                break;
            case _modes.preset1:
                SelectedBorder.SetActive(true);
                UnselectedBorder.SetActive(false);
                FeatureSelectedIcon.SetActive(true);
                InvertObjects();
                break;
        }
    }

    private void Awake()
    {
        _audioManager = FindObjectOfType<AudioManager>();
    }

}
