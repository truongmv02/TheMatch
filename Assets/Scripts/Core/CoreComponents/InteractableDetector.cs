/*using System;
using System.Collections.Generic;
using Game.Interaction;
using UnityEngine;

namespace Game.CoreSystem
{
    [RequireComponent(typeof(Collider2D))]
    public class InteractableDetector : CoreComponent
    {
        public Action<IInteractable> OntryInteract;
        private readonly List<IInteractable> interactables = new();
        private IInteractable closestInteractable;
        private float distanceToClosestInteractable = float.PositiveInfinity;

        [ContextMenu("Try Interact")]

        public void TryInteract(bool inputValue)
        {
            if (!inputValue || closestInteractable == null) return;
            OntryInteract?.Invoke(closestInteractable);
        }

        private void Update()
        {
            if (interactables.Count <= 0)
            {
                return;
            }
            distanceToClosestInteractable = float.PositiveInfinity;
            var oldClosestInteractable = closestInteractable;

            if (closestInteractable != null)
            {
                distanceToClosestInteractable = FindDistanceTo(closestInteractable);
            }

            foreach (var interactable in interactables)
            {
                if (interactable == closestInteractable)
                    continue;
                var interactableDistance = FindDistanceTo(interactable);

                if (interactableDistance >= distanceToClosestInteractable)
                    continue;

                closestInteractable = interactable;
                distanceToClosestInteractable = interactableDistance;
            }

            if (closestInteractable == oldClosestInteractable)
            {
                return;
            }

            oldClosestInteractable?.DisableInteraction();
            closestInteractable?.EnableInteraction();
        }

        private float FindDistanceTo(IInteractable interactable)
        {
            return Vector3.Distance(transform.position, interactable.GetPosition());
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.IsInteractable(out var interactable))
            {
                interactables.Add(interactable);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.IsInteractable(out var interactable))
            {
                interactables.Remove(interactable);
                if (interactable == closestInteractable)
                {
                    interactable.DisableInteraction();
                    closestInteractable = null;
                }
            }
        }
        private void OnDrawGizmosSelected()
        {
            foreach (var item in interactables)
            {
                Gizmos.color = item == closestInteractable ? Color.red : Color.cyan;
                Gizmos.DrawLine(transform.position, item.GetPosition());
            }
        }

    }
}*/