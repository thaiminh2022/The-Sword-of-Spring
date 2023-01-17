using UnityEngine;

namespace TheSwordOfSpring.Misc
{
    public class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    T[] result = Resources.LoadAll<T>("");

                    if (result == null || result.Length <= 0)
                    {
                        Debug.LogError("NO SINGLETON ON THIS TABLE FOR TYPE: " + typeof(T).ToString());
                    }
                    else if (result.Length > 1)
                    {
                        Debug.LogError("THERE SHOULD ONLY BE 1 SINGLETON OF THIS TYPE: " + typeof(T).ToString());
                    }
                    else
                    {
                        // We fucking good
                        _instance = result[0];
                        _instance.hideFlags = HideFlags.DontUnloadUnusedAsset;
                    }
                }

                return _instance;
            }
        }

    }
}

