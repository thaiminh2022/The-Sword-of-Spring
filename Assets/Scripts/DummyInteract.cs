using UnityEngine;
using TheSwordOfSpring.Modules;

namespace TheSwordOfSpring
{
    public class DummyInteract : MonoBehaviour, IInteractable

    {
        public bool Interact(object source)
        {
            // Interaction succeed
            return true;
        }
    }
}