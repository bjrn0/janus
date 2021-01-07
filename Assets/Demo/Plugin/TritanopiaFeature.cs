using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.UI;
using TMPro;

public class TritanopiaFeature : MonoBehaviour
{
    // ui reference
    public GameObject SelectedBorder;
    public GameObject UnselectedBorder;
    public GameObject FeatureText;
    public GameObject FeatureSelectedIcon;
    public GameObject ActiveSlot1;
    public GameObject InactiveSlot1;
    public GameObject ActiveSlot2;
    public GameObject InactiveSlot2;
    public GameObject ActiveSlot3;
    public GameObject InactiveSlot3;
    public GameObject CustomPanel;
    public GameObject RedChannelSlider;
    public GameObject GreenChannelSlider;

    // ui components
    private TextMeshProUGUI _featureText;
    private Slider _redChannelSlider;
    private Slider _greenChannelSlider;
    // ui state
    public bool TritanopiaActive = false;
    private _modes _activeMode = _modes.unselected;
    private enum _modes
    {
        unselected,
        preset1,
        preset2,
        preset3,
    }

    // modes references
    private ProtanopiaFeature _protanopiaReference;
    private DeuteranopiaFeature _deuteranopiaReference;

    // dictionary objects
    private IDictionary<int, Color> _initObjectsData = new Dictionary<int, Color>();
    private List<Object> _storedObjects = new List<Object>();

    // init color values 
    private Dictionary<int, float> _redColorValues = new Dictionary<int, float>();
    private Dictionary<int, float> _greenColorValues = new Dictionary<int, float>();
    private Dictionary<int, float> _blueColorValues = new Dictionary<int, float>();

    // audio reference
    private AudioManager _audioManager;

    private void Preset1()
    {
        SaveObjectColor();
        ApplyTritanopia();
        TritanopiaActive = true;
    }

    private void Preset2()
    {
        ActivateOutline();
        TritanopiaActive = true;
    }

    private void Preset3()
    {
        FocusColors();
        TritanopiaActive = true;
    }


    private void FocusColors()
    {
        Object[] _allGameObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in _allGameObjects)
        {
            Outline _objOutline = obj?.GetComponent<Outline>();
            if (_objOutline == null)
            {
                Renderer _objRenderer = obj?.GetComponent<Renderer>();
                if (_objRenderer != null)
                {
                    _objRenderer.material.color = new Color(1, 1, 0);
                }
            }
        }
    }

    private float InvertOutlineColor(float color, string type)
    {

        if (type == "red")
        {
            Color32 _newColor = new Color(color, 0, 0);
            int _convertedColor = _newColor.r - 255;
            int _normalizedColor;

            if (_convertedColor <= 0)
            {
                _normalizedColor = _convertedColor * (-1);
            }
            else _normalizedColor = _convertedColor;

            Color _floatColor = new Color32((byte)_normalizedColor, 0, 0, 1);

            return _floatColor.r;
        }
        else
        {
            Color32 _newColor = new Color(0, color, 0);
            int _convertedColor = _newColor.g - 255;
            int _normalizedColor;

            if (_convertedColor <= 0)
            {
                _normalizedColor = _convertedColor * (-1);
            }
            else _normalizedColor = _convertedColor;

            Color _floatColor = new Color32(0, (byte)_normalizedColor, 0, 1);

            return _floatColor.g;
        }
    }

    private void ActivateOutline()
    {
        Object[] _allGameObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in _allGameObjects)
        {
            Outline _objOutline = obj?.GetComponent<Outline>();

            if (_objOutline != null)
            {
                Renderer _objRenderer = obj?.GetComponent<Renderer>();
                _objOutline.enabled = true;
                _objOutline.OutlineMode = Outline.Mode.OutlineVisible;
                _objOutline.OutlineWidth = 5f;
                _objOutline.OutlineColor = new Color(InvertOutlineColor(_objRenderer.material.color.r, "red"), InvertOutlineColor(_objRenderer.material.color.g, "green"), 0);
            }
        }
    }

    private void DeactivateOutline()
    {
        Object[] _allGameObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in _allGameObjects)
        {
            Outline _objectOutline = obj?.GetComponent<Outline>();
            if (_objectOutline != null)
            {
                _objectOutline.enabled = false;
            }
        }
    }

    private void SaveObjectColor()
    {
        // create a dictionary and save unique instance id and color
        Object[] _allGameObjects = FindObjectsOfType<GameObject>();
        Stopwatch _calc = new Stopwatch();
        _calc.Start();
        foreach (GameObject obj in _allGameObjects)
        {
            FirstPersonAIO _hasControllerScript = obj?.GetComponent<FirstPersonAIO>();
            MeshRenderer _hasMeshRenderer = obj?.GetComponent<MeshRenderer>();
            Ignore _hasIgnoreComponent = obj?.GetComponent<Ignore>();
            if (_hasControllerScript == null)
            {
                if (_hasMeshRenderer != null && _hasIgnoreComponent == null)
                {
                    Renderer _checkColor = obj?.GetComponent<Renderer>();
                    _storedObjects.Add(obj);
                    _redColorValues.Add(obj.GetInstanceID(), _checkColor.material.color.r);
                    _greenColorValues.Add(obj.GetInstanceID(), _checkColor.material.color.g);
                    _blueColorValues.Add(obj.GetInstanceID(), _checkColor.material.color.b);
                }
            }
        }
        _calc.Stop();

        string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            _calc.Elapsed.Hours, _calc.Elapsed.Minutes, _calc.Elapsed.Seconds,
            _calc.Elapsed.Milliseconds / 10);

        UnityEngine.Debug.Log(elapsedTime);
    }

    private void ApplyTritanopia()
    {
        foreach (GameObject obj in _storedObjects)
        {
            try
            {
                Renderer _objRenderer = obj?.GetComponent<Renderer>();

                if (_objRenderer != null)
                {
                    _objRenderer.material.color = new Color(_objRenderer.material.color.r, _objRenderer.material.color.g, 0);
                }
            }
            catch
            {
                throw new System.Exception("couldnt fetch component from gameobject");
            }
        }
    }

    private void ResetTritanopia()
    {
        foreach (GameObject obj in _storedObjects)
        {
            foreach (KeyValuePair<int, float> redPair in _redColorValues)
            {
                if (obj.GetInstanceID() == redPair.Key)
                {
                    foreach (KeyValuePair<int, float> greenPair in _greenColorValues)
                    {
                        if (obj.GetInstanceID() == greenPair.Key)
                        {
                            foreach (KeyValuePair<int, float> bluePair in _blueColorValues)
                            {
                                if (obj.GetInstanceID() == bluePair.Key)
                                {
                                    Renderer _objRenderer = obj?.GetComponent<Renderer>();

                                    if (_objRenderer != null)
                                    {
                                        _objRenderer.material.color = new Color(redPair.Value, greenPair.Value, bluePair.Value);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        _redColorValues.Clear();
        _greenColorValues.Clear();
        _blueColorValues.Clear();
        DeactivateOutline();
    }

    // preset2 -> outline, preset3 -> outline + highlight

    private float EqualValueSlider(float value, float objValue)
    {
        if ((value - 0.5f) + objValue <= 1)
        {
            return (value - 0.5f) + objValue;
        }
        else return 1;

    }

    public void RedChannelSliderOnChange()
    {
        Slider _redSliderComponent = _redChannelSlider.GetComponent<Slider>();
        float _sliderValue = _redSliderComponent.value;
        //grab all materials from storedobjects
        foreach (GameObject obj in _storedObjects)
        {
            Renderer _objRenderer = obj?.GetComponent<Renderer>();
            _objRenderer.material.color = new Color(EqualValueSlider(_sliderValue, _objRenderer.material.color.r), _objRenderer.material.color.g, 0);
        }
    }

    public void GreenChannelSliderOnChange()
    {
        Slider _greenSliderComponent = _greenChannelSlider?.GetComponent<Slider>();
        float _sliderValue = _greenSliderComponent.value;

        foreach (GameObject obj in _storedObjects)
        {
            Renderer _objRenderer = obj?.GetComponent<Renderer>();
            _objRenderer.material.color = new Color(_objRenderer.material.color.r, EqualValueSlider(_sliderValue, _objRenderer.material.color.g), 0);
        }
    }


    public void ResetColorValues()
    {
        _redChannelSlider.value = 0.5f;
        _greenChannelSlider.value = 0.5f;
        ResetTritanopia();
        SaveObjectColor();
        ApplyTritanopia();
    }

    public void TritanopiaOnClick()
    {
        _audioManager.Play("featureClick");
        if (!_protanopiaReference.ProtanopiaActive && !_deuteranopiaReference.DeuteranopiaActive)
        {
            if ((int)_activeMode < 3)
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
                    InactiveSlot1.SetActive(true);
                    InactiveSlot2.SetActive(true);
                    InactiveSlot3.SetActive(true);
                    ActiveSlot1.SetActive(false);
                    ActiveSlot2.SetActive(false);
                    ActiveSlot3.SetActive(false);
                    CustomPanel.SetActive(false);
                    _featureText.text = "Tritanopia \n (Blue)";
                    ResetTritanopia();
                    TritanopiaActive = false;
                    break;
                case _modes.preset1:
                    SelectedBorder.SetActive(true);
                    UnselectedBorder.SetActive(false);
                    FeatureSelectedIcon.SetActive(true);
                    ActiveSlot1.SetActive(true);
                    ActiveSlot2.SetActive(false);
                    ActiveSlot3.SetActive(false);
                    InactiveSlot1.SetActive(false);
                    InactiveSlot2.SetActive(true);
                    InactiveSlot3.SetActive(true);
                    CustomPanel.SetActive(true);
                    _featureText.text = "Tritanopia \n (Preset1)";
                    Preset1();
                    break;
                case _modes.preset2:
                    SelectedBorder.SetActive(true);
                    UnselectedBorder.SetActive(false);
                    FeatureSelectedIcon.SetActive(true);
                    ActiveSlot1.SetActive(false);
                    ActiveSlot2.SetActive(true);
                    ActiveSlot3.SetActive(false);
                    InactiveSlot1.SetActive(true);
                    InactiveSlot2.SetActive(false);
                    InactiveSlot3.SetActive(true);
                    CustomPanel.SetActive(false);
                    _featureText.text = "Tritanopia \n (Preset2)";
                    Preset2();
                    break;
                case _modes.preset3:
                    SelectedBorder.SetActive(true);
                    UnselectedBorder.SetActive(false);
                    FeatureSelectedIcon.SetActive(true);
                    ActiveSlot1.SetActive(false);
                    ActiveSlot2.SetActive(false);
                    ActiveSlot3.SetActive(true);
                    InactiveSlot1.SetActive(true);
                    InactiveSlot2.SetActive(true);
                    InactiveSlot3.SetActive(false);
                    CustomPanel.SetActive(false);
                    _featureText.text = "Tritanopia \n (Preset3)";
                    Preset3();
                    break;
            }

        }
    }

    private void Awake()
    {
        _protanopiaReference = FindObjectOfType<ProtanopiaFeature>();
        _deuteranopiaReference = FindObjectOfType<DeuteranopiaFeature>();
        _featureText = FeatureText.GetComponent<TextMeshProUGUI>();
        _featureText.text = "Tritanopia \n (Blue)";
        _redChannelSlider = RedChannelSlider.GetComponent<Slider>();
        _greenChannelSlider = GreenChannelSlider.GetComponent<Slider>();
        _audioManager = FindObjectOfType<AudioManager>();
        DeactivateOutline();
    }
}
