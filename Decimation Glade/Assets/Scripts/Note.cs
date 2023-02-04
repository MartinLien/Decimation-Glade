using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Note : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textField = null;

    private Transform _spawnPoint = null;
    public Transform SpawnPoint { get { return _spawnPoint; } }

    public void SetNote(string content, Transform spawnPoint)
    {
        _textField.text = content;
        _spawnPoint = spawnPoint;
    }

    public string GetText()
    {
        return _textField.text;
    }
}
