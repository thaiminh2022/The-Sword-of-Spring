using UnityEngine;

namespace TheSwordOfSpring.TimeSystem
{
    public class TimeClockUI : MonoBehaviour
    {
        [SerializeField] LeanTweenType leanType;
        [SerializeField] private bool flipped;
        [SerializeField, Tooltip("Ref: Default morning will be 00:00, -90deg offset it by +6 hour")] private float offset;

        [SerializeField] private RectTransform rectTransform;

        private void Update()
        {
            if (rectTransform == null)
                return;

            float degree = TimeClockManager.CurrentClockDegree;
            float modifier = flipped ? -1 : 1;

            rectTransform
            .LeanRotateZ((degree + offset) * modifier, .3f)
            .setEase(leanType);

        }
    }
}