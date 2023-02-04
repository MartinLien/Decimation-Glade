using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/String List")]
public class StringList : ScriptableObject
{
    [SerializeField] private List<string> _variable = new List<string>();
    public List<string> Variable { get { return _variable; } }
}
