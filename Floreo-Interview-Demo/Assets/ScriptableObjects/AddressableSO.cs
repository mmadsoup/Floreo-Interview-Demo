using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "AddressableSO", menuName = "Scriptable Objects/AddressableSO")]
public class AddressableSO : ScriptableObject
{
    [SerializeField] public string addressableName;
    [SerializeField] public string addressablePath;
}
