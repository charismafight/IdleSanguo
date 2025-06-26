using System.Collections;
using System.Collections.Generic;
using Assets.Common;
using Assets.Scripts.Characters;
using Assets.Scripts.Common;
using UnityEngine;

public class ProfileScript : BaseMonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var profileSpritePath = CharacterManager.SelectedCharacterConfig.ProfileSpritePath;

        if (string.IsNullOrWhiteSpace(profileSpritePath))
        {
            profileSpritePath = $"{IdleConstants.ProfileSpritesFolder}{CharacterManager.SelectedCharacterConfig.Code}";
            L($"profileSpritePath配置为空，尝试通过默认规则加载:{profileSpritePath}");
        }

        var profileSprite = ResourceManager.GetSprite(profileSpritePath);
        if (profileSprite == null)
        {
            L($"未能在路径：{CharacterManager.SelectedCharacterConfig.ProfileSpritePath}下找到相应sprite资源");
            return;
        }

        transform.GetComponent<SpriteRenderer>().sprite = profileSprite;
        transform.GetComponent<SpriteRenderer>().size = new Vector2(1, 1);
    }
}
