using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextSpacingFeature : MonoBehaviour
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

    // font settings
    private int _resetFontSpacing = -12;
    private int _increase1 = 2;
    private int _increase2 = 4;
    private int _increase3 = 6;

    // audio reference 
    private AudioManager _audioManager;


    private void IncreaseFontSpacing(int increment)
    {
        Object[] _allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in _allObjects)
        {
            TextMeshProUGUI _objText = obj?.GetComponent<TextMeshProUGUI>();
            TextMeshPro _objNormalText = obj?.GetComponent<TextMeshPro>();
            if (_objText != null)
            {
                _objText.characterSpacing = _objText.characterSpacing + increment;
            }
            else if (_objNormalText != null)
            {
                _objNormalText.characterSpacing = _objNormalText.characterSpacing + increment;
            }
        }
    }

    public void FontSpacingOnClick()
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
                _featureText.text = "Font Spacing \n (Unset)";
                IncreaseFontSpacing(_resetFontSpacing);
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
                _featureText.text = "Font Spacing \n (Preset1)";
                IncreaseFontSpacing(_increase1);
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
                _featureText.text = "Font Spacing \n (Preset2)";
                IncreaseFontSpacing(_increase2);
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
                _featureText.text = "Font Spacing \n (Preset3)";
                IncreaseFontSpacing(_increase3);
                break;
        }
    }

    private void Awake()
    {
        _featureText = FeatureText.GetComponent<TextMeshProUGUI>();
        _featureText.text = "Font Spacing \n (Unset)";
        _audioManager = FindObjectOfType<AudioManager>();
    }
}
