using UnityEngine;
using System.Collections.Generic;

namespace TheSwordOfSpring.UISystem
{
    public class UIHooks : MonoBehaviour
    {
        [SerializeField] GameObject[] closableUIElements;
        IUIInput baseInput;

        private void Start()
        {
            baseInput = GetComponent<IUIInput>();
        }
        private void Update()
        {
            if (baseInput.EscapeUIMode())
            {
                foreach (var element in closableUIElements)
                {
                    element.SetActive(false);
                }

                UIManager.UsePlayerMode();
            }
        }
    }
}