using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimeSkipper : MonoBehaviour
{
    public bool CanTimeSkip { get; private set; }

    [SerializeField] private InputActionReference _timeSkipReference;
    private TimeSkipper _timeSkipper;

    private void TimeSkip()
    {

    }

    private void OnTimeSkip(InputAction.CallbackContext context)
    {
        if (CanTimeSkip)
            TimeSkip();
    }

    private void OnEnable()
    {
        PlayableCharactersManager.Instance.CurrentActiveCharacterInput.actions[_timeSkipReference.action.name].performed += OnTimeSkip;
    }

    private void OnDisable()
    {
        PlayableCharactersManager.Instance.CurrentActiveCharacterInput.actions[_timeSkipReference.action.name].performed -= OnTimeSkip;
    }
}