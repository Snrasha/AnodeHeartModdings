using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Xml.Linq;
using UnityEngine;

namespace Extrama.Patches
{

    [HarmonyPatch(typeof(StringLocalizer), nameof(StringLocalizer.Load))]
    static class Patch_StringLocalizer_Load
    {
        [HarmonyPostfix]
        static void Postfix(Dictionary<string, string> ___strings)
        {
            foreach(var keyvalue in ExtramaFramework.StringDict)
            {
                ___strings.Add(keyvalue.Key, keyvalue.Value);
            }
        }

    }
}
