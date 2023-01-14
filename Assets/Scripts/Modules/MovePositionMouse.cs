using UnityEngine;

namespace TheSwordOfSpring.Modules
{
    public class MovePositionMouse : MonoBehaviour
    {

        [SerializeField] Transform targetObject;

        private IBaseInput baseInput;
        private IStatComponent stats;

        private float speed;

        Vector2 lastMousePosition;

        private void Start()
        {
            baseInput = GetComponent<IBaseInput>();
            stats = GetComponent<IStatComponent>();


        }

        private void Update()
        {
            speed = stats.GetSpeed();

            if (baseInput.MouseRightClick())
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                lastMousePosition = mousePosition;

            }

            targetObject.position = Vector2.MoveTowards(targetObject.position, lastMousePosition, stats.GetSpeed() * Time.deltaTime);

        }

    }
}

