using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSplit : Room
{
    [SerializeField] private Transform _nextRoomAnchorRight = null;
    [SerializeField] private Transform _nextRoomAnchorLeft = null;

    //[Header("Triggers")]
    //[SerializeField] private RoomTrigger _rightExitTrigger = null;
    //[SerializeField] private RoomTrigger _leftExitTrigger = null;

    public Transform NextRoomAnchorRight { get { return _nextRoomAnchorRight; } }
    public Transform NextRoomAnchorLeft { get { return _nextRoomAnchorLeft; } }

    private void Start()
    {
        _blockage.SetActive(false);

        _enterTrigger.OnTrigger += EnterRoom;
        //_rightExitTrigger.OnTrigger += ExitRightRoom;
        //_leftExitTrigger.OnTrigger += ExitLeftRoom;
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

    //private void ExitRightRoom()
    //{
    //    if (_exitTriggered)
    //        return;
    //    _exitTriggered = true;

    //    Debug.Log("Exit Right Room");

    //    OnExitingRoom?.Invoke(this);
    //}

    //private void ExitLeftRoom()
    //{
    //    if (_exitTriggered)
    //        return;
    //    _exitTriggered = true;

    //    Debug.Log("Exit Left Room");

    //    OnExitingRoom?.Invoke(this);
    //}
}
