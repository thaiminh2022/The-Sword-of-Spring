using UnityEngine;


namespace TheSwordOfSpring.Modules
{
    public class AttackMouseClick : MonoBehaviour
    {
        IAttackComponent attackComponent;
        IBaseInput baseInput;
        IStatComponent statComponent;


        private bool mouseReleased = true;

        private void Start()
        {
            attackComponent = GetComponent<IAttackComponent>();
            baseInput = GetComponent<IBaseInput>();
            statComponent = GetComponent<IStatComponent>();
        }

        private void Update()
        {
            if (!baseInput.AttackPressed())
            {
                mouseReleased = true;
                return;
            }

            if (mouseReleased)
            {
                attackComponent.StartAttack();
                mouseReleased = false;
            }

        }

    }


}
