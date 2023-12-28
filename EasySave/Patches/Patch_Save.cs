using UnityEngine;
using HarmonyLib;
using System.Security.Policy;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Reflection;
using System.Collections;

namespace EasySave.Patches
{
    public class SaveGameScreen_ConfirmSave
    {
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
            static void Postfix(string ___anchor, int slot)
            {
                if (___anchor != null && ___anchor.Equals("QuickSave"))
                {

                    GameObject gameObject = GameObject.FindGameObjectWithTag("Player");

                    if (gameObject != null)
                    {
                        QuickSave quickSave = new QuickSave();
                        quickSave.xPosPlayer = gameObject.transform.position.x;
                        quickSave.yPosPlayer = gameObject.transform.position.y;
                        quickSave.zPosPlayer = gameObject.transform.position.z;
                        FileUtil.Save(quickSave, $"quicksave_{slot}.bin");
                    }
                }
            }
        }


    }


    public class LevelLoader_getAnchorPosition
    {
        [HarmonyPatch]
        static class Patch_LevelLoader_getAnchorPosition
        {
            [HarmonyTargetMethod]
            static public MethodBase TargetMethod()
            {
                List<MethodInfo> methods = AccessTools.GetDeclaredMethods(typeof(LevelLoader));
                foreach (MethodInfo method in methods)
                {
                    if (method.Name.Equals("getAnchorPosition"))
                    {
                        if (method.ReturnType == typeof(Vector3))
                        {
                            if (method.GetParameters().Length == 1)
                            {
                                return method;
                            }
                        }
                    }
                }
                return AccessTools.Method(typeof(LevelLoader), "getAnchorPosition");
            }

            [HarmonyPrefix]
            static bool Prefix(ref Vector3 __result, string anchorId)
            {
                Debug.Log("Patch_LevelLoader_getAnchorPosition " + anchorId);

                if (anchorId != null && anchorId.Equals("QuickSave"))
                {
                    QuickSave quickSave = FileUtil.Load<QuickSave>($"quicksave_{GameState.Instance().Data.Slot}.bin", null);

                    if (quickSave != null)
                    {


                      ///  Debug.Log("Patch_LevelLoader_getAnchorPosition " + quickSave.positionPlayer.ToString());

                        __result = new Vector3(quickSave.xPosPlayer,quickSave.yPosPlayer,quickSave.zPosPlayer); 
                        return false;
                    }
                }
                return true;
            }
        }


    }
}
