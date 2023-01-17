using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityRandom = UnityEngine.Random;

namespace TheSwordOfSpring.Modules
{
    public class InteractRigidbody : MonoBehaviour
    {

        private IBaseInput baseInput;
        private IStatComponent statComponent;

        private bool mouseReleased = true;

        [Header("Debug")]
        [SerializeField] private bool useGizmos = false;
        [SerializeField] private Color gizmosColor = Color.green;

        void Start()
        {
            baseInput = GetComponent<IBaseInput>();
            statComponent = GetComponent<IStatComponent>();
        }
        private void Update()
        {
            if (!baseInput.InteractPressed())
            {
                mouseReleased = true;
                return;
            }

            if (mouseReleased)
            {
                bool canInteract = TryGetClosetInteractable(out var interactable);

                if (canInteract)
                {
                    interactable?.Interact(gameObject);
                }


                mouseReleased = false;
            }
        }

        private bool TryGetClosetInteractable(out IInteractable interactable)
        {
            List<Collider2D> colliders = Physics2D.OverlapCircleAll(transform.position, statComponent.GetViewRange()).ToList().FindAll(item => item.gameObject != transform.root.gameObject);

            if (colliders.Count <= 0)
            {
                interactable = null;
                return false;

            }

            float smallestDistance = colliders.Min((selector) => disc(selector));
            if (smallestDistance > statComponent.GetViewRange())
            {
                interactable = null;
                return false;
            }

            interactable = colliders
            .FindAll(item => item.GetComponent<IInteractable>() != null)
            .Aggregate((min, next) => disc(min) > disc(next) ? next : min)
            .GetComponent<IInteractable>();

            print(interactable);

            return true;
        }

        private float disc(Collider2D objectTransform)
        {
            return Vector2.Distance(transform.position, objectTransform.transform.position);
        }


        private void OnDrawGizmosSelected()
        {
            if (!useGizmos)
                return;

            Gizmos.color = gizmosColor;

            float range = statComponent != null ? statComponent.GetViewRange() : 1f;
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }
}