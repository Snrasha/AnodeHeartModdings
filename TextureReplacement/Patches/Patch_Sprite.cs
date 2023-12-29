using UnityEngine;
using HarmonyLib;
using System.Security.Policy;

namespace TextureReplacement.Patches
{
    //[HarmonyPatch(typeof(SpriteLoader), nameof(SpriteLoader.LoadSprite), new[] { typeof(string) })]
    //static class Patch_SpriteLoader_LoadSprite
    //{
    //    [HarmonyPrefix]
    //    static bool Prefix(ref Sprite __result, string path)
    //    {
    //        if (path.EndsWith("Orb")) {
    //            Debug.Log("Yes this is a ORB");
    //        }
    //        return __result == null;

    //    }

    //}

    


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

                    __result = TextureReplacement.GetTexture(TextureReplacement.SpritesOverworldsIdle, text);

                    return __result == null;
                    
                case GameCharacterAnimationType.Walk:
                    text = $"Monsters/Overworlds/{id}_Walk";

                    __result = TextureReplacement.GetTexture(TextureReplacement.SpritesOverworldsWalk, text);
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
            Sprite grid = TextureReplacement.GetSprite(TextureReplacement.SpritesGrid, text);
            if (grid != null)
            {
                __result= grid.texture;
            }
            return __result == null;
        }
    }


    [HarmonyPatch(typeof(SpriteLoader), nameof(SpriteLoader.LoadCharacterIcon), new[] { typeof(string), typeof(string) })]
    static class Patch_SpriteLoader_LoadCharacterIcon
    {
        [HarmonyPrefix]
        static bool Prefix(ref Sprite __result, string id, string suffix = "")
        {
            string text = id;
            if(suffix!= null && suffix.Length > 0)
            {
                text += "_" + suffix;
            }

          //  Debug.Log("Patch_SpriteLoader_LoadCharacterIcon |" + id + "|" + suffix);

            __result = TextureReplacement.GetSprite(TextureReplacement.SpritesCharacterIcons, "Characters/Icons/" + text);
            if (__result == null)
            {
                __result = TextureReplacement.GetSprite(TextureReplacement.SpritesCharacterIcons, "Monsters/Icons/" + text);

            }

            return __result == null;
        }
    }
}
