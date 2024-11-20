using UnityEngine;
using HarmonyLib;
using System.Security.Policy;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Reflection;
using System.Collections;
using Randomizer.Config;
using Randomizer;

namespace EasySave.Patches
{
    public class Patch_Save
    {
        [HarmonyPatch]
        static class Patch_SaveGameScreen_LoadGame
        {
            [HarmonyTargetMethod]
            static public MethodBase TargetMethod()
            {
                List<MethodInfo> methods = AccessTools.GetDeclaredMethods(typeof(SaveGameScreen));
                foreach (MethodInfo method in methods)
                {
                    if (method.Name.Equals("LoadGame"))
                    {
                        if (method.ReturnType == typeof(IEnumerator))
                        {
                            if (method.GetParameters().Length == 1)
                            {
                                return method;
                            }
                        }
                    }
                }
                return AccessTools.Method(typeof(SaveGameScreen), "LoadGame");
            }

            [HarmonyPostfix]
            static void Postfix(int slot)
            {
                Plugin.Instance.Load(slot);
            }
        }
        [HarmonyPatch]
        static class Patch_SaveGameScreen_ConfirmSave
        {
            [HarmonyTargetMethod]
            static public MethodBase TargetMethod()
            {
                List<MethodInfo> methods = AccessTools.GetDeclaredMethods(typeof(SaveGameScreen));
                foreach (MethodInfo method in methods)
                {
                    if (method.Name.Equals("ConfirmSave"))
                    {
                        if (method.ReturnType == typeof(IEnumerator))
                        {
                            if (method.GetParameters().Length == 1)
                            {
                                return method;
                            }
                        }
                    }
                }
                return AccessTools.Method(typeof(SaveGameScreen), "ConfirmSave");
            }

            [HarmonyPostfix]
            static void Postfix(int slot)
            {
                Plugin.Instance.Save(slot);
            }
        }


    }

}
