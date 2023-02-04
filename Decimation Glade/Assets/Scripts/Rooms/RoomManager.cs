using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private Room _starterRoom = null;
    [SerializeField] private RoomList _rooms = null;
    [SerializeField] private StringList _notes = null;
    [SerializeField] private int _roomsBetweenNotes = 2;

    // Next room is a list because the path can split
    private List<Room> _nextRooms = new List<Room>();
    private List<string> _noteList = new List<string>();

    private Room _currentRoom = null;
    private Room _otherPath = null;

    private int _roomCount = 0;

    private Room GetRandomRoom()
    {
        return _rooms.Variable[Random.Range(0, _rooms.Variable.Count)];
    }

    private string GetRandomNote()
    {
        int rand = Random.Range(0, _noteList.Count);
        string note = _noteList[rand];
        _noteList.RemoveAt(rand);
        return note;
    }

    private void Start()
    {
        _noteList = new List<string>(_notes.Variable);

        _currentRoom = _starterRoom;
        _roomCount = _roomsBetweenNotes;

        Transform spawnPoint = _currentRoom.NextRoomAnchor;
        _nextRooms.Add(Instantiate(GetRandomRoom(), spawnPoint.position, spawnPoint.rotation));
        
        _nextRooms[0].OnEnteringRoom += SpawnNextRoom;
    }

    private void SpawnNextRoom(Room roomEntered)
    {
        int index = _nextRooms.IndexOf(roomEntered);
        if (index == 0)
        {
            if (_nextRooms.Count > 1)
                _otherPath = _nextRooms[1];   
        }
        else
            _otherPath = _nextRooms[0];

        TerminatePreviousRoom();
        _currentRoom = roomEntered;

        ++_roomCount;
        if (_roomCount >= _roomsBetweenNotes)
        {
            _roomCount = 0;

            // Spawn note when entering room because we don't know what direction the player is going to go during a split
            roomEntered.SpawnNote(GetRandomNote());
        }

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
            r.OnExitingRoom += TriggerExitEvent;
        }
    }

    private void TriggerExitEvent(Room room)
    {

    }

    private void SetNextCurrentRoom(Room room)
    {
        _currentRoom = room;
    }

    private void TerminatePreviousRoom()
    {
        if (_currentRoom != null)
            Destroy(_currentRoom.gameObject);

        if (_otherPath != null)
            Destroy(_otherPath.gameObject);
    }
}
