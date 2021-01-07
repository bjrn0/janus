using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

public class DyslexiaFontFeature : MonoBehaviour
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

    // ui components
    private TextMeshProUGUI _featureText;

    // ui state
    private _modes _activeMode = _modes.unselected;
    private enum _modes
    {
        unselected,
        preset1,
        preset2,
        preset3,
    }

    // font reference 

    Dictionary<int, TMP_FontAsset> _storedFonts = new Dictionary<int, TMP_FontAsset>();

    // font types
    private TMP_FontAsset _openDyslexia;
    private TMP_FontAsset _easyReading;
    private TMP_FontAsset _arial;

    // audio reference
    private AudioManager _audioManager;

    private void Preset1()
    {
        StoreInitFonts();
        LoadPreset1();
    }

    private void Preset2()
    {
        LoadPreset2();
    }

    private void Preset3()
    {
        LoadPreset3();
    }

    private void LoadPreset1()
    {
        Object[] _allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in _allObjects)
        {
            TextMeshProUGUI _objText = obj?.GetComponent<TextMeshProUGUI>();
            TextMeshPro _objNormalText = obj?.GetComponent<TextMeshPro>();
            if (_objText != null)
            {
                _objText.font = _openDyslexia;
            }
            else if (_objNormalText != null)
            {
                _objNormalText.font = _openDyslexia;

            }
        }
    }

    private void LoadPreset2()
    {
        Object[] _allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in _allObjects)
        {
            TextMeshProUGUI _objText = obj?.GetComponent<TextMeshProUGUI>();
            TextMeshPro _objNormalText = obj?.GetComponent<TextMeshPro>();
            if (_objText != null)
            {
                _objText.font = _easyReading;
            }
            else if (_objNormalText != null)
            {
                _objNormalText.font = _easyReading;

            }
        }
    }

    private void LoadPreset3()
    {
        Object[] _allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in _allObjects)
        {
            TextMeshProUGUI _objText = obj?.GetComponent<TextMeshProUGUI>();
            TextMeshPro _objNormalText = obj?.GetComponent<TextMeshPro>();
            if (_objText != null)
            {
                _objText.font = _arial;
            }
            else if (_objNormalText != null)
            {
                _objNormalText.font = _arial;

            }
        }
    }


    private void StoreInitFonts()
    {
        Object[] _allGameObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in _allGameObjects)
        {
            TextMeshProUGUI _objText = obj?.GetComponent<TextMeshProUGUI>();
            TextMeshPro _objNormalText = obj?.GetComponent<TextMeshPro>();
            if (_objText != null)
            {
                _storedFonts.Add(obj.GetInstanceID(), _objText.font);
            }
            else if (_objNormalText != null)
            {
                _storedFonts.Add(obj.GetInstanceID(), _objNormalText.font);
            }
        }
    }


    private void ResetFonts()
    {
        Object[] _allGameObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in _allGameObjects)
        {
            TextMeshProUGUI _objText = obj?.GetComponent<TextMeshProUGUI>();
            TextMeshPro _objNormalText = obj?.GetComponent<TextMeshPro>();
            if (_objText != null)
            {
                foreach (KeyValuePair<int, TMP_FontAsset> pairs in _storedFonts)
                {
                    if (pairs.Key == obj.GetInstanceID())
                    {
                        _objText.font = pairs.Value;
                    }
                }
            }
            else if (_objNormalText != null)
            {
                foreach (KeyValuePair<int, TMP_FontAsset> pairs in _storedFonts)
                {
                    if (pairs.Key == obj.GetInstanceID())
                    {
                        _objNormalText.font = pairs.Value;
                    }
                }
            }
        }

        _storedFonts.Clear();
    }

    public void DyslexiaFontOnClick()
    {
        _audioManager.Play("featureClick");
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
                _featureText.text = "Dyslexia Font \n (Unset)";
                ResetFonts();
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
                _featureText.text = "Dyslexia Font \n (Preset1)";
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
                _featureText.text = "Dyslexia Font \n (Preset2)";
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
                _featureText.text = "Dyslexia Font \n (Preset3)";
                Preset3();
                break;
        }
    }

    private void Awake()
    {
        _featureText = FeatureText.GetComponent<TextMeshProUGUI>();
        _featureText.text = "Dyslexia Font \n (Unset)";
        _openDyslexia = Resources.Load<TMP_FontAsset>("Fonts/openDyslexia/OpenDyslexic-Regular SDF");
        _easyReading = Resources.Load<TMP_FontAsset>("Fonts/easyReading/EasyReadingPRO SDF");
        _arial = Resources.Load<TMP_FontAsset>("Fonts/arial/ArialCE SDF");
        _audioManager = FindObjectOfType<AudioManager>();
    }
}
