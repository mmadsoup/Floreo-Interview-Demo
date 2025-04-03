using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "AddressableDatabaseSO", menuName = "Scriptable Objects/AddressableDatabaseSO")]
public class AddressableDatabaseSO : ScriptableObject
{
    [SerializeField] public AddressableSO[] addressables;
}
