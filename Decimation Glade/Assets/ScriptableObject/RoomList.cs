using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/RoomList")]
public class RoomList : ScriptableObject
{
    [SerializeField] private List<Room> _variable = new List<Room>();
    public List<Room> Variable { get { return _variable; } }
}
