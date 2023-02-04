using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForNote : MonoBehaviour
{
    [SerializeField] private Transform _pickedupPoint = null;
    [SerializeField] private float _rayDistance = 5f;
    [SerializeField] private PopupUI _pickupText = null;

    private bool _notePicked = false;

    private Note _note = null;
    private Transform _originalNotePosition = null;

    private Coroutine _coroutine = null;

    // Raycast for note
    private void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit, _rayDistance))
        {
            if (_notePicked)
            {
                _pickupText.SetText($"Press 'F' to drop");
                _pickupText.ToggleUI(true);

                if (Input.GetKeyDown(KeyCode.F))
                {
                    _pickupText.ToggleUI(false);

                    if (_coroutine != null)
                        StopCoroutine(_coroutine);
                    StartCoroutine(DropNote());

                    _notePicked = false;
                }
            }
            else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Note"))
            {
                if (!_notePicked)
                {
                    _note = hit.collider.GetComponent<Note>();
                    _pickupText.SetText($"Press 'F' to pick up");
                    _pickupText.ToggleUI(true);

                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        _pickupText.ToggleUI(false);

                        _originalNotePosition = _note.SpawnPoint;

                        _notePicked = true;

                        if (_coroutine != null)
                            StopCoroutine(_coroutine);
                        StartCoroutine(PickupNote());
                    }
                }
                
            }
            else
            {
                _pickupText.ToggleUI(false);
            }
        }
    }

    private IEnumerator PickupNote()
    {
        float duration = 0.5f;
        float timer = 0;

        _note.transform.rotation = _pickedupPoint.rotation;
        while (timer < duration)
        {
            _note.transform.position = Vector3.Slerp(_originalNotePosition.position, _pickedupPoint.position, timer / duration);

            timer += Time.deltaTime;
            yield return null;
        }
        _coroutine = null;
    }

    private IEnumerator DropNote()
    {
        float duration = 0.5f;
        float timer = 0;

        _note.transform.rotation = _originalNotePosition.rotation;
        while (timer < duration)
        {
            _note.transform.position = Vector3.Slerp(_pickedupPoint.position, _originalNotePosition.position, timer / duration);

            timer += Time.deltaTime;
            yield return null;
        }
        _coroutine = null;
    }
}
