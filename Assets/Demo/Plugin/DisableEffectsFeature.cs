using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableEffectsFeature : MonoBehaviour
{
    // ui reference
    public GameObject SelectedBorder;
    public GameObject UnselectedBorder;
    public GameObject FeatureSelectedIcon;

    // audio reference
    private AudioManager _audioManager;


    // ui state
    private _modes _activeMode = _modes.unselected;
    private enum _modes
    {
        unselected,
        preset1,
    }


    private void DisableEffects()
    {
        GameObject[] _allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in _allObjects)
        {
            ParticleSystem _objParticleSystem = obj?.GetComponent<ParticleSystem>();
            TrailRenderer _objTrailRenderer = obj?.GetComponent<TrailRenderer>();
            LineRenderer _objLineRenderer = obj?.GetComponent<LineRenderer>();
            Projector _objProjector = obj?.GetComponent<Projector>();
            if (_objLineRenderer != null)
            {
                _objLineRenderer.enabled = false;
            }
            else if (_objTrailRenderer != null)
            {
                _objTrailRenderer.enabled = false;
            }
            else if (_objProjector != null)
            {
                _objProjector.enabled = false;
            }
            else if (_objParticleSystem != null)
            {
                _objParticleSystem.Stop();
            }
        }
    }

    private void EnableEffects()
    {
        GameObject[] _allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in _allObjects)
        {
            ParticleSystem _objParticleSystem = obj?.GetComponent<ParticleSystem>();
            TrailRenderer _objTrailRenderer = obj?.GetComponent<TrailRenderer>();
            LineRenderer _objLineRenderer = obj?.GetComponent<LineRenderer>();
            Projector _objProjector = obj?.GetComponent<Projector>();
            if (_objLineRenderer != null)
            {
                _objLineRenderer.enabled = true;
            }
            else if (_objTrailRenderer != null)
            {
                _objTrailRenderer.enabled = true;
            }
            else if (_objProjector != null)
            {
                _objProjector.enabled = true;
            }
            else if (_objParticleSystem != null)
            {
                _objParticleSystem.Play();
            }
        }
    }
 

    public void DisableEffectsOnClick()
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
                EnableEffects();
                break;
            case _modes.preset1:
                SelectedBorder.SetActive(true);
                UnselectedBorder.SetActive(false);
                FeatureSelectedIcon.SetActive(true);
                DisableEffects();
                break;
        }
    }

    private void Awake()
    {
        _audioManager = FindObjectOfType<AudioManager>();
    }
}
