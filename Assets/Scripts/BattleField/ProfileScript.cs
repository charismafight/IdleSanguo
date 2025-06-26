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
            L($"profileSpritePath����Ϊ�գ�����ͨ��Ĭ�Ϲ������:{profileSpritePath}");
        }

        var profileSprite = ResourceManager.GetSprite(profileSpritePath);
        if (profileSprite == null)
        {
            L($"δ����·����{CharacterManager.SelectedCharacterConfig.ProfileSpritePath}���ҵ���Ӧsprite��Դ");
            return;
        }

        transform.GetComponent<SpriteRenderer>().sprite = profileSprite;
        transform.GetComponent<SpriteRenderer>().size = new Vector2(1, 1);
    }
}
