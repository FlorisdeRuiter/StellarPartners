using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] private List<Transform> CurrentInteractables = new();
    [SerializeField] private InputActionReference _interactReference;

    private void OnInteract(InputAction.CallbackContext context)
    {
        StartCoroutine(PlayInteraction());
    }

    private IEnumerator PlayInteraction()
    {
        GetComponentInParent<Animator>().SetTrigger("Kick");

        yield return new WaitForSeconds(0.5f);

        if (CurrentInteractables.Count != 0)
            GetClosestInteractable(CurrentInteractables).GetComponent<IInteractable>().Interact();

        yield return null;
    }

    public Transform GetClosestInteractable(List<Transform> interactablesPool)
    {
        Transform closestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Transform target in interactablesPool)
        {
            Vector3 directionToTarget = target.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                closestTarget = target;
            }
        }
        return closestTarget;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IInteractable>() != null)
        {
            CurrentInteractables.Add(collision.gameObject.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<IInteractable>() != null)
        {
            CurrentInteractables.Remove(collision.gameObject.transform);
        }
    }

    private void OnEnable()
    {
        PlayableCharactersManager.Instance.CurrentActiveCharacterInput.actions[_interactReference.action.name].performed += OnInteract;
    }

    private void OnDisable()
    {
        PlayableCharactersManager.Instance.CurrentActiveCharacterInput.actions[_interactReference.action.name].performed -= OnInteract;
    }
}