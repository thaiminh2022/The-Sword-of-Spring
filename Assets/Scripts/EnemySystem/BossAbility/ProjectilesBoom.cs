using UnityEngine;
using System.Collections.Generic;
using TheSwordOfSpring.WeaponSystem;
using System;

namespace TheSwordOfSpring.EnemySystem.BossAbility
{
    ///<summary>
	/// Shoots a punch of projectiles towards set position 
    ///</summary>
    public class ProjectilesBoom : MonoBehaviour
    {
        private Vector2[] dirs;
        Vector3 startPosition;
        private float damage;

        private void Init()
        {
            foreach (var dir in dirs)
            {
                GameObject randomProjectile = StaticProjectilesHolder.Instance.GetRandomProjectile();
                GameObject go = Instantiate(randomProjectile, startPosition, Quaternion.identity);
                Projectile projectile = go.GetComponent<Projectile>();
                projectile.SetValue(dir, damage, 10f);
            }
            Destroy(gameObject, 1f);
        }
        ///<summary>
        /// Create a game object and return a function to init it. 
        ///</summary>
        public static Action Create(Vector3 startPosition, Vector2[] dirs, float damage)
        {
            GameObject go = new GameObject("ProjectilesBoom");
            var comp = go.AddComponent<ProjectilesBoom>();
            comp.dirs = dirs;
            comp.damage = damage;
            comp.startPosition = startPosition;

            return comp.Init;
        }

    }
}