using UnityEngine;

namespace TheSwordOfSpring.Modules
{
    public class IdleRandomPosition : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] float offset;
        [SerializeField] float startWaitTime;


        [Header("Visual")]
        [SerializeField] bool drawGizmos;

        private IStatComponent stats;
        private Vector2 moveToPosition;
        private float waitTime;

        private Vector2 initialPosition;

        void Start()
        {
            stats = GetComponent<IStatComponent>();
            moveToPosition = ChooseNewPosition();

            initialPosition = transform.position;
        }

        void Update()
        {

            if (Vector2.Distance(transform.position, moveToPosition) > offset)
            {
                transform.position = Vector2.MoveTowards(transform.position, moveToPosition, stats.GetSpeed() * Time.deltaTime);
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
            return Random.insideUnitCircle * stats.GetViewRange() + initialPosition;
        }

        private void OnDrawGizmosSelected()
        {
            if (!drawGizmos)
                return;

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(moveToPosition, offset);

        }
    }
}

