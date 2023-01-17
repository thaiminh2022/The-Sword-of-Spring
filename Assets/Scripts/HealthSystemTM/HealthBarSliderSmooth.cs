using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace TheSwordOfSpring.HealthSystemTM
{
    public class HealthBarSliderSmooth : MonoBehaviour
    {
        [SerializeField] float smoothTime = 1;
        [SerializeField] GameObject getHealthSystemObj;
        [SerializeField] Slider slider;
        private HealthSystem healthSystem;
        private float lastHealthNormalized = 0;
        private float currentHealthNormalized = 0;

        float vel;

        private bool canUpdate = true;

        private void Start()
        {
            if (HealthSystem.TryGetHealthSystem(getHealthSystemObj, out var system))
            {
                SetHealthSystem(system);
            }
        }

        public void SetHealthSystem(HealthSystem healthSystem)
        {
            if (this.healthSystem != null)
            {
                this.healthSystem.OnHealthChanged -= HealthSystem_OnHealthChanged;
            }
            this.healthSystem = healthSystem;

            UpdateHealthBar();
            healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
        }

        private void HealthSystem_OnHealthChanged(object sender, System.EventArgs e)
        {
            UpdateHealthBar();
        }

        private void UpdateHealthBar()
        {
            canUpdate = true;
        }

        private void Update()
        {
            if (canUpdate == false)
            {
                return;
            }
            currentHealthNormalized = healthSystem.GetHealthNormalized();
            lastHealthNormalized = LeanSmooth.damp(lastHealthNormalized, currentHealthNormalized, ref vel, smoothTime);

            slider.value = lastHealthNormalized;

            float normalizedDiff = Mathf.Abs(currentHealthNormalized - lastHealthNormalized);

            if (normalizedDiff < .001f)
            {
                if (Mathf.Round(lastHealthNormalized) == Mathf.Round(currentHealthNormalized))
                {
                    //Snap when it's enough
                    slider.value = currentHealthNormalized;
                    lastHealthNormalized = currentHealthNormalized;
                    print("Filled");
                    canUpdate = false;
                }

            }
        }



        private void OnDestroy()
        {
            healthSystem.OnHealthChanged -= HealthSystem_OnHealthChanged;
        }


    }
}