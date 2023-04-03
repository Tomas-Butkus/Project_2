using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class SharedFloat : ScriptableObject
{
    private float _amount;

    public float Amount
    {
        get => _amount;
        set
        {
            _amount = value;
            onValueChanged.Invoke();
        }
    }

    public UnityEvent onValueChanged;
}