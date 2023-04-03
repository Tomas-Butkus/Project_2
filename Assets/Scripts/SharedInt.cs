using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class SharedInt : ScriptableObject
{
    private int _amount;

    public int Amount
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