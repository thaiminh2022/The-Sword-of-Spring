using TheSwordOfSpring.Misc;
using UnityEngine;
using Redcode.Extensions;
namespace TheSwordOfSpring.EffectsSystem
{

    public class StunEffects : Effect
    {
        private IStunable stunObject;

        private void Start()
        {
            GameObject go = Instantiate(PSStaticHolder.Instance.Stun_PS, transform.position, Quaternion.identity, transform);
            go.transform.SetLocalPositionY(1f + transform.localScale.y / 2);

            stunObject = GetComponent<IStunable>();

            stunObject?.StartStun();
        }

        public override void DestroySelf(float time)
        {
            stunObject?.StopStun();
            base.DestroySelf(time);
        }
    }
}