using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Common;
using Assets.Scripts.Common;
using Assets.Scripts.Config.GameConfigs;
using Assets.Scripts.Enums;
using UnityEngine;

public class CharacterScript : BaseMonoBehaviour
{
    public bool DeadAnimPlaying;

    private HUDText hudText;

    // Start is called before the first frame update
    void Start()
    {
        hudText = InitComponent<HUDText>();
    }

    public void ShowText(string txt, Color color)
    {
        hudText.Add(txt, transform.position, color);
    }

    public void ShowHPChange(float regenCount)
    {
        var txt = regenCount < 0 ? regenCount.ToString() : "+" + regenCount;
        var color = regenCount < 0 ? Color.red : Color.green;
        if (regenCount != 0)
        {
            ShowText(txt, color);
        }
    }

    public void InitCharacterInfo(CharacterConfig cconfig)
    {
        if (cconfig == null)
        {
            L("CharacterScript初始化异常，CharacterConfig为空");
            return;
        }

        var spritePath = string.Empty;

        //处理sprite
        if (string.IsNullOrWhiteSpace(cconfig.SpritePath))
        {
            var ct = (CharacterTypes)cconfig.CharacterType;
            spritePath = $"{IdleConstants.CharacterSpriteFolder}{ct}/{cconfig.Code}/{cconfig.Code}";
            L($"cconfig.SpritePath为空，计算默认路径为：{spritePath}");
        }
        else
        {
            spritePath = cconfig.SpritePath;
        }

        var sprites = ResourceManager.Getsprites(spritePath);
        if (!sprites.Any())
        {
            E("CharacterScript初始化异常，默认路径未能找到sprite资源");
            return;
        }

        GetComponent<SpriteRenderer>().sprite = sprites[8];
        GetComponent<SpriteRenderer>().size = new Vector2(3, 3);

        //处理动画
        var animationPath = string.Empty;
        if (string.IsNullOrWhiteSpace(cconfig.AnimationPath))
        {
            var ct = (CharacterTypes)cconfig.CharacterType;
            animationPath =
                $"{IdleConstants.ProtagonistCharacterAnimatorFolder}{ct}/{cconfig.Code}/{cconfig.Code}";
            L($"cconfig.AnimationPath为空，计算默认路径为：{animationPath}");
        }
        else
        {
            animationPath = cconfig.AnimationPath;
        }

        GetComponent<Animator>().runtimeAnimatorController =
            ResourceManager.GetRuntimeAnimatorController(animationPath);
    }

    public void Killed()
    {
        DeadAnimPlaying = false;
    }

    public void PlayingDead()
    {
        DeadAnimPlaying = true;
    }

    public void ReLoad()
    {
        DeadAnimPlaying = false;
    }
}
