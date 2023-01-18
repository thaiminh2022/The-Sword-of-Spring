using UnityEngine;
using System;
using Redcode.Extensions;

namespace TheSwordOfSpring.CharacterSystem
{

    public class PlayerAnimationWrapper : MonoBehaviour
    {
        [SerializeField]
        private PlayerAnimation playerAnimation;

        [SerializeField]
        private CharacterBase characterBase;

        [SerializeField]
        private CharacterBaseInput playerInput;

        [SerializeField]
        private CharacterAttackComponent attackComponent;


        private void Start()
        {
            characterBase.OnTakeDamage += CharacterBase_OnTakeDamage;
            attackComponent.OnAttack += CharacterAttackComponent_OnAttack;
        }

        private void CharacterBase_OnTakeDamage(object sender, float damageAmount)
        {
            playerAnimation.SetHitAnimation();
        }
        private void CharacterAttackComponent_OnAttack(object sender, EventArgs args)
        {
            playerAnimation.SetAttackAnimation();
        }

        private void Update()
        {
            Vector2 inputVector = playerInput.GetKeyBoardInput();
            HandleFlipCharacter(inputVector.x);
            if (inputVector != Vector2.zero)
            {
                // player is moving
                playerAnimation.SetRunAnimation();
                return;
            }
            // Player is idling
            playerAnimation.SetIdleAnimation();
        }

        private void HandleFlipCharacter(float xCoords)
        {
            if (xCoords < 0)
            {
                // Flip player to the left
                transform.SetLocalScaleX(-1);
            }
            else if (xCoords > 0)
            {
                // Flip player to the right
                transform.SetLocalScaleX(1);

            }

            // xCoords == 0 do nothing
        }

        private void OnDestroy()
        {
            characterBase.OnTakeDamage -= CharacterBase_OnTakeDamage;
            attackComponent.OnAttack -= CharacterAttackComponent_OnAttack;
        }

        private void OnValidate()
        {
            //! U probably wonder what the fuck is "??="
            //? "??=": Doesnt evaluate the right side if left side is non-null

            playerAnimation ??= GetComponent<PlayerAnimation>();
            characterBase ??= GetComponent<CharacterBase>();
            playerInput ??= GetComponent<CharacterBaseInput>();
            attackComponent ??= GetComponent<CharacterAttackComponent>();

        }


    }
}