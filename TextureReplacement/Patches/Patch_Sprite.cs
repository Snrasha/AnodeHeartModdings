using UnityEngine;
using HarmonyLib;
using System.Security.Policy;

namespace TextureReplacement.Patches
{
    [HarmonyPatch(typeof(SpriteLoader), nameof(SpriteLoader.LoadMonsterFront), new[] { typeof(Species), typeof(bool) })]
    static class Patch_SpriteLoader_LoadMonsterFront
    {
        [HarmonyPrefix]
        static bool Prefix(ref Sprite __result, Species id, bool altColor = false)
        {
            if (altColor)
            {
                string text = $"Monsters/AltFronts/{id}";
                __result = TextureReplacement.GetSprite(TextureReplacement.SpritesAltFronts, text);
            }
            else
            {
                string text = $"Monsters/Fronts/{id}";
                __result = TextureReplacement.GetSprite(TextureReplacement.SpritesFronts, text);
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
            if (altColor)
            {
                string text = $"Monsters/AltIcons/{id}";
                __result = TextureReplacement.GetSprite(TextureReplacement.SpritesAltIcons, text);
            }
            else
            {
                string text = $"Monsters/Icons/{id}";
                __result = TextureReplacement.GetSprite(TextureReplacement.SpritesIcons, text);
            }
            return __result == null;

        }
    }

    [HarmonyPatch(typeof(SpriteLoader), nameof(SpriteLoader.LoadMonsterFrontGlitched), new[] { typeof(string) })]
    static class Patch_SpriteLoader_LoadMonsterFrontGlitched
    {
        [HarmonyPrefix]
        static bool Prefix(ref Sprite __result, string id)
        {

            string text = $"Monsters/FrontsGlitch/{id}";
            __result = TextureReplacement.GetSprite(TextureReplacement.SpritesFrontsGlitch, text);
            if (__result == null)
            {
                text = $"Monsters/Fronts/{id}";
                __result = TextureReplacement.GetSprite(TextureReplacement.SpritesFronts, text);
            }

            return __result == null;
        }
    }
    [HarmonyPatch(typeof(SpriteLoader), nameof(SpriteLoader.LoadMonsterOverworld), new[] { typeof(Species), typeof(GameCharacterAnimationType) })]
    static class Patch_SpriteLoader_LoadMonsterOverworld
    {
        [HarmonyPrefix]
        static bool Prefix(ref Texture2D __result, Species id, GameCharacterAnimationType animation)
        {
            string text;
            
            switch (animation)
            {
                case GameCharacterAnimationType.Idle:
                    text = $"Monsters/Overworlds/{id}_Idle";
                    __result = TextureReplacement.GetSprite(TextureReplacement.SpritesOverworldsIdle, text).texture;
                    return __result == null;
                    
                case GameCharacterAnimationType.Walk:
                    text = $"Monsters/Overworlds/{id}_Walk";
                    __result = TextureReplacement.GetSprite(TextureReplacement.SpritesOverworldsWalk, text).texture;
                    return __result == null;
                    
            }
            return true;
        }
    }
    [HarmonyPatch(typeof(SpriteLoader), nameof(SpriteLoader.LoadGridMonsterOverworld), new[] { typeof(Species) })]
    static class Patch_SpriteLoader_LoadGridMonsterOverworld
    {
        [HarmonyPrefix]
        static bool Prefix(ref Texture2D __result, Species id)
        {
            string text = $"Monsters/Grid/{id}";
            __result = TextureReplacement.GetSprite(TextureReplacement.SpritesGrid, text).texture;
            return __result == null;
        }
    }


    //public static Sprite LoadCharacterIcon(string id, string suffix = "")
    //{
    //    suffix = ((suffix == "" || suffix == null) ? "" : ("_" + suffix));
    //    return LoadAny("Characters/Icons/" + id + suffix, "Monsters/Icons/" + id + suffix);
    //}
}
