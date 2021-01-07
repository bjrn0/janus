using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetSettings : MonoBehaviour
{
    // objects
    private List<Object> _storedObjects = new List<Object>();

    // init color values 
    private Dictionary<int, float> _redColorValues = new Dictionary<int, float>();
    private Dictionary<int, float> _greenColorValues = new Dictionary<int, float>();
    private Dictionary<int, float> _blueColorValues = new Dictionary<int, float>();

    private void SaveObjectColor()
    {
        // create a dictionary and save unique instance id and color
        Object[] _allGameObjects = FindObjectsOfType<GameObject>();
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
    }

    public void ResetColorOnClick()
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
    }

    private void Awake()
    {
        SaveObjectColor();
    }
}
