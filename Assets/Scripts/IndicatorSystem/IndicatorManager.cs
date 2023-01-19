using UnityEngine;
using System;
using TheSwordOfSpring.Misc;


namespace TheSwordOfSpring.IndicatorSystem
{
    public class IndicatorManager : MonoBehaviour
    {


        public static void CreateCircleIndicator(Vector3 position, float radius, float displayTime, Action onTimeUp = null)
        {
            GameObject go = Instantiate(IndicatorStaticHolder.Instance.circle, position, Quaternion.identity);
            go.transform.localScale = Vector3.zero;
            Vector3 newScale = new Vector3(radius * 2, radius * 2, radius * 2);
            go.transform.LeanScale(newScale, .2f).setEaseOutSine();

            ActionAfterTime.AddToObject(go, displayTime - .1f, onTimeUp);
        }
        public static void CreateCircleIndicatorWithParent(Vector3 position, float radius, float displayTime, Transform parent, Action onTimeUp = null)
        {
            GameObject go = Instantiate(IndicatorStaticHolder.Instance.circle, position, Quaternion.identity, parent);
            go.transform.localScale = Vector3.zero;
            Vector3 newScale = new Vector3(radius * 2, radius * 2, radius * 2);
            go.transform.LeanScale(newScale, displayTime - .1f).setEaseOutSine();
            ActionAfterTime.AddToObject(go, displayTime, onTimeUp);
        }
        public static void CreateSquareIndicator(Vector3[] positions, float thickness, float displayTime, Action onTimeUp = null)
        {
            GameObject go = Instantiate(IndicatorStaticHolder.Instance.square, positions[0], Quaternion.identity);
            LineRenderer renderer = go.GetComponent<LineRenderer>();

            renderer.positionCount = positions.Length;
            renderer.SetPositions(positions);
            renderer.startWidth = thickness;
            renderer.endWidth = thickness;

            ActionAfterTime.AddToObject(go, displayTime, onTimeUp);
        }
    }

    public enum IndicatorType
    {
        CIRCLE,
        SQUARE,
    }
}
