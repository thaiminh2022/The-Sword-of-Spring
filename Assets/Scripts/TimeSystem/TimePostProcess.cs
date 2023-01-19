using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering;
using System;

namespace TheSwordOfSpring.TimeSystem
{
    public class TimePostProcess : MonoBehaviour
    {
        [SerializeField] Volume nightVolume;

        [SerializeField] float speed = .3f;

        float weight;


        private void Update()
        {
            if (TimeManager.IsNight && weight < 1)
            {
                weight += Time.deltaTime * speed;
                weight = Mathf.Clamp01(weight);
            }
            else if (!TimeManager.IsNight && weight > 0)
            {
                weight -= Time.deltaTime * speed;
                weight = Mathf.Clamp01(weight);
            }

            nightVolume.weight = weight;

        }

    }
}