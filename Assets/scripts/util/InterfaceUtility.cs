using System.Collections.Generic;
using UnityEngine;

namespace util
{
    public class InterfaceUtility
    {
        public static void GetInterfaces<T>(out List<T> resultList, GameObject objectToSearch) where T: class {
            MonoBehaviour[] list = objectToSearch.GetComponents<MonoBehaviour>();
            resultList = new List<T>();
            foreach(MonoBehaviour mb in list){
                if(mb is T){
                    resultList.Add((T)((System.Object)mb));
                }
            }
        }
    }
}