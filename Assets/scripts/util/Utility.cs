using System.Collections.Generic;
using UnityEngine;

namespace util
{
    public class Utility
    {
        public static void getInterfaces<T>(out List<T> resultList, GameObject objectToSearch) where T : class
        {
            MonoBehaviour[] list = objectToSearch.GetComponents<MonoBehaviour>();
            resultList = new List<T>();
            foreach (MonoBehaviour mb in list)
            {
                if (mb is T)
                {
                    resultList.Add((T) ((System.Object) mb));
                }
            }
        }


        public static GameObject findComponentInChildWithTag(GameObject parent, string tag)
        {
            Transform t = parent.transform;
            foreach (Transform tr in t)
            {
                if (tr.CompareTag(tag))
                {
                    return tr.GetComponent<GameObject>();
                }
            }

            return null;
        }
    }
}