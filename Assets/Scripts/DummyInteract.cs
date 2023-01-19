using UnityEngine;
using TheSwordOfSpring.Modules;
using TheSwordOfSpring.IndicatorSystem;

namespace TheSwordOfSpring
{
    public class DummyInteract : MonoBehaviour
    {
        private void Start()
        {
            Vector2 position = new Vector2(-3, 0);
            IndicatorManager.CreateCircleIndicator(position, 1f, 999f);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, 1f);
        }
    }
}