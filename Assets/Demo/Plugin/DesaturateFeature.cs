using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesaturateFeature : MonoBehaviour
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


    private Color DesaturateColors(Renderer obj)
    {
        float _h, _s, _v;
        Color.RGBToHSV(new Color(obj.material.color.r, obj.material.color.g, obj.material.color.b, 1), out _h, out _s, out _v);
        float _newSaturation = _s / 2;
        Color _desaturatedColor = Color.HSVToRGB(_h, _newSaturation, _v);

        return _desaturatedColor;
    }

    private Color ResaturateColors(Renderer obj)
    {
        float _h, _s, _v;
        Color.RGBToHSV(new Color(obj.material.color.r, obj.material.color.g, obj.material.color.b, 1), out _h, out _s, out _v);
        float _newSaturation = _s * 2;
        Color _resaturatedColor = Color.HSVToRGB(_h, _newSaturation, _v);

        return _resaturatedColor;
    }
   

    private void DesaturateObjects()
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
                        _objRenderer.material.color = DesaturateColors(_objRenderer);
                    }
                }
            }
        }
    }

    private void ResaturateObjects()
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
                        _objRenderer.material.color = ResaturateColors(_objRenderer);
                    }
                }
            }
        }
    }


    public void DesaturateOnClick()
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
                ResaturateObjects();
                break;
            case _modes.preset1:
                SelectedBorder.SetActive(true);
                UnselectedBorder.SetActive(false);
                FeatureSelectedIcon.SetActive(true);
                DesaturateObjects();
                break;
        }
    }

    private void Awake()
    {
        _audioManager = FindObjectOfType<AudioManager>();
    }
}
