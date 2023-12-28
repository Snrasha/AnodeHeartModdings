using HarmonyLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Extrama.Patches
{
   // public override object ReadJson(JsonReader reader, Type objectType, [Nullable(2)] object existingValue, JsonSerializer serializer)

    [HarmonyPatch(typeof(StringEnumConverter), nameof(StringEnumConverter.ReadJson), new[] { typeof(JsonReader), typeof(Type), typeof(object), typeof(JsonSerializer) })]
    static class Patch_StringEnumConverter_ReadJson
    {
        [HarmonyPrefix]
        static bool Prefix(ref object __result, JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return true;
            }
            try
            {
                if (reader.TokenType == JsonToken.String)
                {
                    string value = reader.Value?.ToString();
                    if (value.Trim().Length==0)
                    {
                        return true;
                    }
                    if (objectType==typeof(Species))
                    {
                        int numb = ExtramaFramework.ExtramaSpeciesPoint;
                       // Debug.Log("Patch_StringEnumConverter_ReadJson " + value.Trim());
                        foreach (KeyValuePair<int, string> entry in ExtramaFramework.TamasSpecies)
                        {
                            if (entry.Value.Equals(value.Trim()))
                            {
                               
                                numb = entry.Key;
                                __result = numb;
                                return false;
                            }
                        }
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
            }
            return true;

        }

    }

}
