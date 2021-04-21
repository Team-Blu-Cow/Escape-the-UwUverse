using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JaysUnityUtils
{
    public static class JUtils
    {
        // courtesy of http://answers.unity.com/answers/893984/view.html
        public static T FindComponentInChildWithTag<T>(this GameObject parent, string tag) where T : Component
        {
            Transform t = parent.transform;
            foreach (Transform tr in t)
            {
                if (tr.tag == tag)
                {
                    return tr.GetComponent<T>();
                }
            }
            return null;
        }

        public static Vector3 SmoothStepInterpolate(Vector3 min, Vector3 max, float value)
        {
            return Vector3.zero;
        }

        public static float SmoothStep(float min, float max, float value)
        {
            var p = (value - min) / (max - min);
            p = (p > 1)? 1 : p;
            p = (p < 0)? 0 : p;
            return p * p * (3 - 2 * p);
        }
    }
}
