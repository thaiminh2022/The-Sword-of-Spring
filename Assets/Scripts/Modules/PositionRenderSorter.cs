using UnityEngine;
using System.Collections.Generic;

namespace TheSwordOfSpring.Modules
{
    public class PositionRenderSorter : MonoBehaviour
    {
        [SerializeField]
        private int sortingOrderBase = 5000;
        [SerializeField]
        private int offset;

        [SerializeField]
        private bool onlyOnce = false;

        [SerializeField]
        private float timerMax = .1f;
        private float timer;

        private Renderer myRenderer;

        private void Awake()
        {
            myRenderer = gameObject.GetComponent<Renderer>();
        }

        private void LateUpdate()
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = timerMax;
                myRenderer.sortingOrder = (int)(sortingOrderBase - transform.position.y - offset);

                if (onlyOnce)
                {
                    Destroy(this);
                }
            }


        }
    }
}