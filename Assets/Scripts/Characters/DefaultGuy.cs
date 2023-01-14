using System.Collections;
using System.Collections.Generic;
using TheSwordOfSpring.CharacterSystem;
using UnityEngine;

namespace TheSwordOfSpring
{
    public class DefaultGuy : CharacterBase
    {
        private void Update()
        {
            if (Input.GetKeyDown("t"))
            {
                this.GetCharacterBase()
                .Health.AddModifier(new Stats.StatModifier(10, Stats.StatModType.Flat));
            }
        }
    }
}
