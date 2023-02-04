using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSplit : Room
{
    [Header("Anchors")]
    [SerializeField] private Transform _nextRoomAnchorRight = null;
    [SerializeField] private Transform _nextRoomAnchorLeft = null;

    public Transform NextRoomAnchorRight { get { return _nextRoomAnchorRight; } }
    public Transform NextRoomAnchorLeft { get { return _nextRoomAnchorLeft; } }

    private void Start()
    {
        _blockage.SetActive(false);

        _enterTrigger.OnTrigger += EnterRoom;
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
}
