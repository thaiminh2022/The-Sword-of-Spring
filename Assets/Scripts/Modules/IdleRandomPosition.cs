using UnityEngine;
using Redcode.Extensions;

namespace TheSwordOfSpring.Modules
{
    public class IdleRandomPosition : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] float offset;
        [SerializeField] float startWaitTime;


        [Header("Visual")]
        [SerializeField] bool drawGizmos;

        private Vector2 moveToPosition;
        private float waitTime;

        private Vector2 initialPosition;

        void Start()
        {
            moveToPosition = ChooseNewPosition();

            initialPosition = transform.position;
        }

        void Update()
        {
            Flip();

            if (Vector2.Distance(transform.position, moveToPosition) > offset)
            {
                transform.position = Vector2.MoveTowards(transform.position, moveToPosition, 1f * Time.deltaTime);
                return;
            }
            if (waitTime > 0)
            {
                waitTime -= Time.deltaTime;
                return;
            }

            moveToPosition = ChooseNewPosition();
            waitTime = startWaitTime;
        }

        private Vector2 ChooseNewPosition()
        {
            return Random.insideUnitCircle * 3f + initialPosition;
        }

        private void OnDrawGizmosSelected()
        {
            if (!drawGizmos)
                return;

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(moveToPosition, offset);

        }

        private void Flip()
        {
            float dirX = (moveToPosition - (Vector2)transform.position).normalized.x;
            float scaleX = Mathf.Abs(transform.localScale.x);

            if (dirX > 0)
            {
                transform.SetLocalScaleX(scaleX);
            }
            else if (dirX < 0)
            {
                transform.SetLocalScaleX(scaleX * -1);
            }
        }
    }
}

