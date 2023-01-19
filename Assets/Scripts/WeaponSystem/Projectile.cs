using UnityEngine;
using TheSwordOfSpring.Misc;
using TheSwordOfSpring.HealthSystemTM;

namespace TheSwordOfSpring.WeaponSystem
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float launchForce;

        [SerializeField] Rigidbody2D rb;

        Vector2 dir;
        Vector2 targetPosition;
        float damage;

        float rotateAngle = 0;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            Destroy(gameObject, 10f);
        }

        private void Update()
        {
            transform.rotation = Quaternion.Euler(0, 0, rotateAngle);
            rb.velocity = dir * launchForce;
        }

        public void SetValue(Transform target, float damage, float? speed = null)
        {
            targetPosition = target.position;
            dir = (target.position - transform.position).normalized;
            launchForce = speed ?? launchForce;
            rotateAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            this.damage = damage;
        }
        public void SetValue(Vector2 dir, float damage, float? speed = null)
        {
            this.dir = dir;
            launchForce = speed ?? launchForce;
            rotateAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            this.damage = damage;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<IDamageable>().Damage(damage);
            }

        }
    }
}