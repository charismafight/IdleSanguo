using UnityEngine;

namespace Assets.Scripts.Common
{
    public static class ResourceManager
    {
        public static Sprite GetSprite(string path)
        {
            return Resources.Load<Sprite>(path);
        }

        public static Sprite[] Getsprites(string path)
        {
            return Resources.LoadAll<Sprite>(path);
        }

        public static RuntimeAnimatorController GetRuntimeAnimatorController(string path)
        {
            return Resources.Load<RuntimeAnimatorController>(path);
        }

        public static Sprite GetProfileSprite(string spriteName)
        {
            return GetSprite(IdleConstants.ProfileSpritesFolder + spriteName);
        }

        public static T GetSprite<T>(string path) where T : Object
        {
            return Resources.Load<T>(path);
        }
    }
}
