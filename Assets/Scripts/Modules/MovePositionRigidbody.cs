using UnityEngine;

namespace TheSwordOfSpring.Modules
{

    [RequireComponent(typeof(Rigidbody2D))]
    public class MovePositionRigidbody : MonoBehaviour
    {
        [SerializeField] Rigidbody2D rb;


        private IBaseInput baseInput;
        private IStatComponent stats;

        private Vector2 input;


        private void Awake()
        {
            baseInput = GetComponent<IBaseInput>();
            stats = GetComponent<IStatComponent>();

        }

        private void Update()
        {

            input = baseInput.GetKeyBoardInput().normalized;
        }
        private void FixedUpdate()
        {

            rb.velocity = input * stats.GetSpeed();
        }

        private void OnValidate()
        {
            if (rb == null)
            {
                rb = GetComponent<Rigidbody2D>();
            }
        }
    }
}
