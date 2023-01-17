using UnityEngine;
using System.Collections.Generic;

namespace TheSwordOfSpring.EffectsSystem
{
    public abstract class Effect : MonoBehaviour
    {

        public void DestroySelf(float time)
        {
            Destroy(this, time);
        }

        public void Cleanse()
        {
            Destroy(this);
        }

    }
}