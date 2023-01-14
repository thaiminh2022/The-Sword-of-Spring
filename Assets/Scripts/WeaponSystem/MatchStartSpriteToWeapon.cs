using UnityEngine;

namespace TheSwordOfSpring.WeaponSystem
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class MatchStartSpriteToWeapon : MonoBehaviour
    {
        [SerializeField]
        private Transform parentTransform;

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        private void Start()
        {
            spriteRenderer.sprite = parentTransform.GetComponent<WeaponBase>().GetWeaponScriptableObject().sprite;
        }
        private void OnValidate()
        {
            if (parentTransform == null)
            {
                parentTransform = transform.parent;
            }
            if (spriteRenderer == null)
            {
                spriteRenderer = GetComponent<SpriteRenderer>();
            }
        }


    }
}