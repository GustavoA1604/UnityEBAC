using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SOUIIntUpdate : MonoBehaviour
{
    public SOInt soInt;
    public TextMeshProUGUI uiTextValue;

    void OnValidate()
    {
        if (uiTextValue == null)
        {
            uiTextValue = GetComponent<TextMeshProUGUI>();
        }
    }

    void Start()
    {
        if (uiTextValue == null)
        {
            uiTextValue = GetComponent<TextMeshProUGUI>();
        }
        Debug.Assert(uiTextValue != null);
        Debug.Assert(soInt != null);
        uiTextValue.text = soInt.value.ToString();
    }

    void Update()
    {
        uiTextValue.text = soInt.value.ToString();
    }
}
