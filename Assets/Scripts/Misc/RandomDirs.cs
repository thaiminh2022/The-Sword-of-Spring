using UnityEngine;
using System.Collections.Generic;

namespace TheSwordOfSpring.Misc
{
    public static class RandomDirs
    {

        public static Vector2 GetRandomDir()
        {
            float randX = Random.Range(-1f, 1f);
            float randY = Random.Range(-1f, 1f);

            return new Vector2(randX, randY);

        }
        public static Vector2[] GetRandomDirs(int length)
        {
            List<Vector2> vector2s = new List<Vector2>();

            for (int i = 0; i < length; i++)
            {
                Vector2 dir = GetRandomDir();
                vector2s.Add(dir);
            }

            return vector2s.ToArray();

        }
    }
}