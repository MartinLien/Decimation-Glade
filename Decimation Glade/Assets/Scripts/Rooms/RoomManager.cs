using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    private List<Room> _nextRooms = new List<Room>();

    [SerializeField] private Room _starterRoom = null;
    [SerializeField] private RoomList _rooms = null;

    //private Room _nextRoom = null;
    private Room _currentRoom = null;
    private Room _offRoom = null;

    private Room GetRandomRoom()
    {
        return _rooms.Variable[Random.Range(0, _rooms.Variable.Count)];
    }

    private void Start()
    {
        _currentRoom = _starterRoom;

        Transform spawnPoint = _currentRoom.NextRoomAnchor;
        _nextRooms.Add(Instantiate(GetRandomRoom(), spawnPoint.position, spawnPoint.rotation));
        
        _nextRooms[0].OnEnteringRoom += SpawnNextRoom;
    }

    private void SpawnNextRoom(Room room)
    {
        Debug.Log("SpawnNextRoom");

        TerminatePreviousRoom();

        int index = _nextRooms.IndexOf(room);
        if (index == 0)
        {
            if (_nextRooms.Count > 1)
                _offRoom = _nextRooms[1];   
        }
        else
            _offRoom = _nextRooms[0];

        _currentRoom = room;

        _nextRooms.Clear();

        if (_currentRoom.roomType == Room.RoomType.DoubleExit)
        {
            RoomSplit split = (RoomSplit)_currentRoom;

            Transform spawnPoint = split.NextRoomAnchorLeft;
            _nextRooms.Add(Instantiate(GetRandomRoom(), spawnPoint.position, spawnPoint.rotation));

            spawnPoint = split.NextRoomAnchorRight;
            _nextRooms.Add(Instantiate(GetRandomRoom(), spawnPoint.position, spawnPoint.rotation));
        }
        else
        {
            Transform spawnPoint = _currentRoom.NextRoomAnchor;
            _nextRooms.Add(Instantiate(GetRandomRoom(), spawnPoint.position, spawnPoint.rotation));
        }

        foreach (var r in _nextRooms)
        {
            r.OnEnteringRoom += SpawnNextRoom;
            //room.OnExitingRoom += SetNextCurrentRoom;
        }
    }

    private void SetNextCurrentRoom(Room room)
    {
        _currentRoom = room;
    }

    private void TerminatePreviousRoom()
    {
        if (_currentRoom != null)
            Destroy(_currentRoom.gameObject);

        if (_offRoom != null)
            Destroy(_offRoom);
    }
}
