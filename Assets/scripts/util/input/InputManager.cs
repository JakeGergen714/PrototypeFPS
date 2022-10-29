using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using util.axis;

namespace DefaultNamespace
{
    public class InputManager : MonoBehaviour
    {
        private Dictionary<String, float> axisNameToInputValues = new Dictionary<String, float>();
        private Dictionary<String, Axis> nameToAxisMap = new Dictionary<string, Axis>();

        [SerializeField] private float sensitivity = 0f;
        [SerializeField] private float gravity = 0f;

        private bool pause = false;


        public InputManager addAxis(String axisName, String posKey, String negKey)
        {
            Axis a = new Axis(axisName, posKey, negKey);
            axisNameToInputValues.Add(axisName, 0f);
            nameToAxisMap.Add(axisName, a);

            return this;
        }

        public float getValue(String key)
        {
            return axisNameToInputValues[key];
        }

        public void doPause()
        {
            this.pause = true;
        }

        public void doUnpause()
        {
            this.pause = false;
        }

        private void FixedUpdate()
        {
            if (!pause)
            {
                foreach (KeyValuePair<String, Axis> i in nameToAxisMap)
                {
                    float curVal = axisNameToInputValues[i.Key];
                    Axis a = i.Value;

                    if (!Input.GetKey(a.getNegativeKey()) && !Input.GetKey(a.getPositiveKey()))
                    {
                        if (curVal > 0 || curVal < 0)
                        {
                            if (curVal > 0)
                            {
                                curVal -= gravity;
                                if (curVal < 0)
                                {
                                    curVal = 0;
                                }
                            }
                            else if (curVal < 0)
                            {
                                curVal += gravity;
                                if (curVal > 0)
                                {
                                    curVal = 0;
                                }
                            }

                            axisNameToInputValues[i.Key] = curVal;
                            //Debug.Log("Key not held down: " + a.getNegativeKey() + "Val after update: " + curVal);
                        }
                    }

                    if (Input.GetKey(a.getPositiveKey()) & !Input.GetKey(a.getNegativeKey()))
                    {
                        if (curVal < 0)
                        {
                            curVal = 0;
                        }

                        curVal += sensitivity;
                        if (curVal > 1)
                        {
                            curVal = 1;
                        }

                        axisNameToInputValues[i.Key] = curVal;
                        // Debug.Log("Key held down: " + a.getPositiveKey() + "Val after update: " + curVal);
                    }
                    else if (Input.GetKey(a.getNegativeKey()) & !Input.GetKey(a.getPositiveKey()))
                    {
                        if (curVal > 0)
                        {
                            curVal = 0;
                        }

                        curVal -= sensitivity;
                        if (curVal < -1)
                        {
                            curVal = -1;
                        }

                        axisNameToInputValues[i.Key] = curVal;
                        //Debug.Log("Key held down: " + a.getNegativeKey() + "Val after update: " + curVal);
                    }
                }
            }
        }

        public void setSensitivty(float s)
        {
            this.sensitivity = s;
        }

        public void setGravity(float g)
        {
            this.gravity = g;
        }
    }
}