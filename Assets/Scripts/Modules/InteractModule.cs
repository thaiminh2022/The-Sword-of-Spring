using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace TheSwordOfSpring.Modules
{
    public class InteractModule : MonoBehaviour
    {

        public static List<GameObject> interactableGameObjects { get; private set; }

        private IBaseInput baseInput;
        private IStatComponent statComponent;

        private bool mouseReleased = true;

        [Header("Debug")]
        [SerializeField] private bool useGizmos = false;
        [SerializeField] private Color gizmosColor = Color.green;



        private void Awake()
        {
            if (interactableGameObjects == null || interactableGameObjects.Count <= 0)
            {
                interactableGameObjects = GameObject
                .FindObjectsOfType<GameObject>()
                .Where(x => x.GetComponent<IInteractable>() != null)
                .ToList();
            }
        }
        private void Start()
        {
            baseInput = GetComponent<IBaseInput>();
            statComponent = GetComponent<IStatComponent>();

            // print(interactables.Count);
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
                    bool succeed = interactable.Interact();
                }


                mouseReleased = false;
            }
        }

        private bool TryGetClosetInteractable(out IInteractable interactable)
        {
            float smallestDistance = interactableGameObjects.Min((selector) => disc(selector));

            if (smallestDistance < statComponent.GetViewRange())
            {
                interactable = null;
                return false;
            }

            interactable = interactableGameObjects
            .Aggregate((min, next) => disc(min) > disc(next) ? next : min)
            .GetComponent<IInteractable>();

            return true;
        }

        private float disc(GameObject objectTransform)
        {
            return Vector2.Distance(transform.position, objectTransform.transform.position);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = gizmosColor;

            Gizmos.DrawWireSphere(transform.position, statComponent.GetViewRange());
        }

    }
}
