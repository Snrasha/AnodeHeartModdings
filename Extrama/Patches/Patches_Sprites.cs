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

    [HarmonyPatch(typeof(SpriteLoader), nameof(SpriteLoader.LoadMonsterFront), new[] { typeof(Species), typeof(bool) })]
    static class Patch_SpriteLoader_LoadMonsterFront
    {
        [HarmonyPrefix]
        static bool Prefix(ref Sprite __result, Species id, bool altColor = false)
        {

            if ((int)id >= ExtramaFramework.ExtramaSpeciesPoint)
            {
                string name = ExtramaFramework.TamasSpecies[(int)id];

                if (altColor)
                {
                    string text = $"Monsters/AltFronts/{name}";
                    __result = ExtramaFramework.GetSprite(ExtramaFramework.SpritesAltFronts, text);
                }
                else
                {
                    string text = $"Monsters/Fronts/{name}";
                    __result = ExtramaFramework.GetSprite(ExtramaFramework.SpritesFronts, text);
                }
            }
            else
            {
                if (altColor)
                {
                    string text = $"Monsters/AltFronts/{id}";
                    __result = ExtramaFramework.GetSprite(ExtramaFramework.SpritesAltFronts, text);
                }
                else
                {
                    string text = $"Monsters/Fronts/{id}";
                    __result = ExtramaFramework.GetSprite(ExtramaFramework.SpritesFronts, text);
                }
            }
            return __result == null;

        }

    }
    [HarmonyPatch(typeof(SpriteLoader), nameof(SpriteLoader.LoadMonsterIcon), new[] { typeof(Species), typeof(bool) })]
    static class Patch_SpriteLoader_LoadMonsterIcon
    {
        [HarmonyPrefix]
        static bool Prefix(ref Sprite __result, Species id, bool altColor = false)
        {
            Debug.Log("Patch_SpriteLoader_LoadMonsterIcon " + id+" "+(int)id);
            if ((int)id >= ExtramaFramework.ExtramaSpeciesPoint && ExtramaFramework.TamasSpecies.ContainsKey((int)id))
            {
                

                string name = ExtramaFramework.TamasSpecies[(int)id];
                Debug.Log("Patch_SpriteLoader_LoadMonsterIcon " + id + " " + name);

                if (altColor)
                {
                    string text = $"Monsters/AltIcons/{name}";
                    __result = ExtramaFramework.GetSprite(ExtramaFramework.SpritesAltIcons, text);
                }
                else
                {
                    string text = $"Monsters/Icons/{name}";
                    __result = ExtramaFramework.GetSprite(ExtramaFramework.SpritesIcons, text);
                }
            }
            else
            {
                if (altColor)
                {
                    string text = $"Monsters/AltIcons/{id}";
                    __result = ExtramaFramework.GetSprite(ExtramaFramework.SpritesAltIcons, text);
                }
                else
                {
                    string text = $"Monsters/Icons/{id}";
                    __result = ExtramaFramework.GetSprite(ExtramaFramework.SpritesIcons, text);
                }
            }
            return __result == null;

        }
    }
}
