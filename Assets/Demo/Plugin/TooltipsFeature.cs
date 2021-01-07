using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipsFeature : MonoBehaviour
{
    // ui reference
    public GameObject SelectedBorder;
    public GameObject UnselectedBorder;
    public GameObject FeatureSelectedIcon;
    public GameObject TooltipBox;
    public GameObject TooltipText;

    // ui state
    private _modes _activeMode = _modes.unselected;
    private enum _modes
    {
        unselected,
        preset1,
    }

    // player position
    public GameObject PlayerObject;
    public GameObject Camera;
    private FirstPersonAIO _playerScript;
    private Color _objInitColor;
    private Renderer _objHit;
    private Dictionary<int, Color> _storedColors = new Dictionary<int, Color>();

    // raycast settings
    private int _raycastLength = 5;
    private float _heightAdjust = 1;

    // audio reference
    private AudioManager _audioManager;

    private void Preset1()
    {
        if (_activeMode == _modes.preset1)
        {
            if (_objHit != null)
            {
                _objHit.material.color = _objInitColor;
                TooltipBox.SetActive(false);
                _objHit = null;
            }
            RaycastHit _rayHit;
            if (Physics.Raycast(new Vector3(_playerScript.transform.position.x, _playerScript.transform.position.y + _heightAdjust, _playerScript.transform.position.z), Camera.transform.forward, out _rayHit, _raycastLength))
            {
                Tooltip _objectHasTooltip = _rayHit.collider?.GetComponent<Tooltip>();
                if (_objectHasTooltip != null)
                {
                    Renderer _objRender = _objectHasTooltip?.GetComponent<Renderer>();
                    if (_objRender != null)
                    {
                        if (_objHit == null)
                        {
                            _objInitColor = _objRender.material.color;
                        }
                        _objRender.material.color = Color.yellow;
                        _objHit = _objRender;
                        TextMeshProUGUI _tooltipText = TooltipText?.GetComponent<TextMeshProUGUI>();
                        if (_tooltipText != null)
                        {
                            TooltipBox.SetActive(true);
                            _tooltipText.text = _objectHasTooltip._toolTipText; 
                        }
                    }
                }
            } 
        }
    }

    private void GetInitColors()
    {
        Object[] _allGameObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in _allGameObjects)
        {
            Tooltip _objTooltip = obj?.GetComponent<Tooltip>();
            if (_objTooltip != null)
            {
                Renderer _objRenderer = _objTooltip?.GetComponent<Renderer>();
                if (_objRenderer != null)
                {
                    _storedColors.Add(_objTooltip.GetInstanceID(), _objRenderer.material.color);
                }
            }
        }
    }

    private void ClearSettings()
    {
        Object[] _allGameObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in _allGameObjects)
        {
            foreach (KeyValuePair<int, Color> pairs in _storedColors)
            {
                Tooltip _objTooltip = obj?.GetComponent<Tooltip>();
                if (_objTooltip != null)
                {
                    Renderer _objRenderer = _objTooltip?.GetComponent<Renderer>();
                    if (_objRenderer != null)
                    {
                        if (pairs.Key == _objRenderer.GetInstanceID())
                        {
                            _objRenderer.material.color = pairs.Value;
                        }
                    }
                }
            }
        }
        _storedColors.Clear();
    }


    public void TooltipsOnClick()
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
                ClearSettings();
                break;
            case _modes.preset1:
                SelectedBorder.SetActive(true);
                UnselectedBorder.SetActive(false);
                FeatureSelectedIcon.SetActive(true);
                GetInitColors();
                break;
        }
    }

    private void FixedUpdate()
    {
        Preset1();
    }

    private void Awake()
    {
        _playerScript = PlayerObject?.GetComponent<FirstPersonAIO>();
        _audioManager = FindObjectOfType<AudioManager>();
    }
}
