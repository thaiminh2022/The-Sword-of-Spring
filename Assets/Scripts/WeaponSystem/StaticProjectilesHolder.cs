using UnityEngine;
using TheSwordOfSpring.Misc;

namespace TheSwordOfSpring.WeaponSystem
{
    [CreateAssetMenu(menuName = "TheSwordOfSpring/Datas/ProjectilesHolder", fileName = "ProjectilesHolder")]
    public class StaticProjectilesHolder : SingletonScriptableObject<StaticProjectilesHolder>
    {
        public GameObject shovelProjectile;
        public GameObject scytheProjectile;
        public GameObject forkProjectile;



        public GameObject GetRandomProjectile()
        {
            GameObject[] projectiles = new GameObject[] {
                shovelProjectile,
                scytheProjectile,
                forkProjectile,
            };

            return projectiles[Random.Range(0, projectiles.Length)];
        }

    }
}