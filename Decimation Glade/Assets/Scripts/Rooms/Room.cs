using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public enum RoomType { SingleExit, DoubleExit }
    public RoomType roomType = RoomType.SingleExit;

    [SerializeField] private Transform _nextRoomAnchor = null;

    [SerializeField] protected GameObject _blockage = null;

    [Header("Triggers")]
    [SerializeField] protected RoomTrigger _enterTrigger = null;
    //[SerializeField] protected RoomTrigger _exitTrigger = null;

    public Transform NextRoomAnchor { get { return _nextRoomAnchor; } }

    public System.Action<Room> OnEnteringRoom;
    //public System.Action<Room> OnExitingRoom;

    protected bool _enterTriggered = false;
    //protected bool _exitTriggered = false;

    private void Start()
    {
        _blockage.SetActive(false);

        _enterTrigger.OnTrigger += EnterRoom;
        //_exitTrigger.OnTrigger += ExitRoom;
    }

    private void EnterRoom()
    {
        if (_enterTriggered)
            return;
        _enterTriggered = true;

        Debug.Log("EnterRoom", gameObject);

        // Trigger enter room events
        _blockage.SetActive(true);
        OnEnteringRoom?.Invoke(this);
    }

    //private void ExitRoom()
    //{
    //    if (_exitTriggered)
    //        return;
    //    _exitTriggered = true;

    //    Debug.Log("Exit Room");

    //    OnExitingRoom?.Invoke(this);
    //}
}
