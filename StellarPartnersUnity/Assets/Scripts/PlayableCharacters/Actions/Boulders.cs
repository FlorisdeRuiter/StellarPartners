using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulders : MonoBehaviour, IInteractable
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        StartCoroutine(DoInteraction());
    }

    public IEnumerator DoInteraction()
    {
        _animator.SetTrigger("Break");

        yield return null;
    }
}
