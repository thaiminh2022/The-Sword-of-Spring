using UnityEngine;
using TheSwordOfSpring.Modules;

namespace TheSwordOfSpring
{
    public class DummyInteract : MonoBehaviour, IInteractable

    {
        public bool Interact()
        {
            // Interaction succeed
            return true;
        }
    }
}