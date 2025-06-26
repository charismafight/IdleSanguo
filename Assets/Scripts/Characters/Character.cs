using System;
using System.Collections;
using System.Linq;
using Assets.Common;
using Assets.Common.Cache;
using Assets.Common.Extensions;
using Assets.Scripts.Characters;
using Assets.Scripts.Common;
using Assets.Scripts.Config.GameConfigs;
using Assets.Scripts.Config.GameConfigs.Stat;
using Assets.Scripts.Enums;
using Assets.Scripts.Sqlite;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class Character : BaseMonoBehaviour
{
    public GameObject actionBar;
    public PropertiesBoard board;

    private Animator animator;
    private CharacterScript characterScript;
    private Growth growth;
    public Stat stat;
    private CharacterConfig cconfig;
    public int CconfigId = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = InitUniqueComponentIncludeChirldren<Animator>();
        characterScript = InitUniqueComponentIncludeChirldren<CharacterScript>();
        if (cconfig == null)
        {
            Debug.LogError("缺少对CharacterConfig的初始化，无法构造角色");

            Debug.Log($"进入测试模式，默认加载CconfigId为4的黄巾贼");
            cconfig = CacheManager.Instance.Get<CharacterConfig>(cc => cc.Id == 4);
        }

        //根据条件，对character里面的信息进行初始化
        characterScript.InitCharacterInfo(cconfig);
        LoadName();
        growth = CacheManager.Instance.Get<Growth>(o => o.CharacterId == cconfig.Id && o.StatType == (int)StatTypes.Growth);

        ShowAttributes();
    }

    /// <summary>
    /// 行动，暂时为攻击enemy
    /// </summary>
    /// <param name="enemy"></param>
    /// <returns>对手是否已经死亡</returns>
    public bool Act(GameObject enemy)
    {
        var oppKilled = false;
        if (enemy == null)
        {
            return true;
        }

        actionBar.GetComponentInChildren<Slider>().value += 1f * (stat.Speed / 1000f);

        if (actionBar.GetComponentInChildren<Slider>().value >= 1)
        {
            actionBar.GetComponentInChildren<Slider>().value = 0;
            //攻击行动不可继续，则暂停等待重启
            //这里attack函数是enemy调用自己的被攻击，名称容易导致误解
            if (Attack(enemy))
            {
                EarnEXP(enemy.GetComponent<Character>().stat.LV, enemy.GetComponent<Character>().stat.SuppliedEXP);
                //刷新当前属性
                ShowAttributes();
                oppKilled = true;
            }

            //攻击者行为
            if (animator != null)
            {
                animator.Play("Attack");
                L($"{transform.name}Attack 播放");
            }
        }

        return oppKilled;
    }

    /// <summary>
    /// 攻击只针对对象，所以演变成了enemy调用自己的被攻击
    /// todo 是否还有问题，比如伤害计算？技能攻击？特殊攻击？
    /// </summary>
    /// <param name="enemy"></param>
    /// <returns>是否被击杀</returns>
    public bool Attack(GameObject enemy)
    {
        //todo underattack函数传入的应优化为计算攻、防后的攻击数值，交由被攻击方进行显示

        var dmg = 0;
        //伤害计算
        //物理 or 法术
        if (AttackManager.UseSkill(stat, enemy.GetComponent<Character>().stat))
        {

        }
        else
        {
            dmg = AttackManager.CalculateAttackDamage(stat, enemy.GetComponent<Character>().stat);
        }

        return enemy.GetComponent<Character>().UnderAttack(dmg);
    }

    public void ShowAttributes()
    {
        board?.Refresh();
    }

    void LoadName()
    {
        var nameControl = transform.GetChildObjects("Name").FirstOrDefault();
        if (nameControl != null)
        {
            nameControl.GetComponentInChildren<TMP_Text>().text = cconfig.Name;
        }
    }

    /// <summary>
    /// 被攻击
    /// </summary>
    /// <param name="damage">受伤害值</param>
    /// <returns>是否被击杀</returns>
    public bool UnderAttack(float damage)
    {
        stat.HP -= damage;

        if (stat.HP <= 0)
        {
            stat.HP = 0;
            animator?.Play("Dead");
            characterScript.PlayingDead();
            //gameObject.SetActive(false);
            //game over
            //SceneManager.LoadScene("GameOver");
        }
        else
        {
            animator?.Play("UnderAttack");
        }

        characterScript.ShowHPChange(-damage);
        ShowAttributes();

        var killed = stat.HP == 0;
        if (killed)
        {
            Debug.Log($"{transform.name}  was killed");
        }

        return killed;
    }

    public void Regen(int regenCount)
    {
        if (regenCount <= 0)
        {
            return;
        }

        if (!stat.Injured)
        {
            stat.MaxHP++;
            stat.HP++;
        }
        else
        {
            stat.HP = stat.HP + regenCount > stat.MaxHP ? stat.MaxHP : stat.HP + regenCount;
        }

        ShowAttributes();
    }

    //public bool IsDeadPlayed()
    //{
    //    return characterScript != null && characterScript.DeadAnimPlayed;

    //    //var animatorInfo = animator.GetCurrentAnimatorStateInfo(1);
    //    //if (animatorInfo.IsName("Dead"))
    //    //{
    //    //    return true;
    //    //}

    //    //return false;
    //}

    public bool IsDeadPlaying()
    {
        return characterScript != null && characterScript.DeadAnimPlaying;
    }

    public void EarnEXP(int enemyLevel, int expCount)
    {
        //根据规则，处理经验获取

        characterScript.ShowText(expCount.ToString(), Color.yellow);
        stat.Exp += expCount;
        if (stat.Exp >= stat.MaxExp)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        stat.Exp -= stat.MaxExp;
        stat.LV++;
        stat.GrowUp(growth);
        //补满红蓝
        stat.HP = stat.MaxHP;
        stat.MP = stat.MaxMP;
    }

    public void Resurrection()
    {
        stat.HP = stat.MaxHP;
        stat.MP = stat.MaxMP;
        ShowAttributes();
        actionBar.GetComponentInChildren<Slider>().value = 0;
    }

    #region Profile处理
    void SetCurrentCharacterSprite()
    {

    }
    #endregion

    public void InitCConfig(CharacterConfig config)
    {
        cconfig = config;
        //stat涉及到被修改，并非只读数据，从数据库中取
        stat = SqlHelper.Instance.Get<Stat>(o => o.CharacterId == cconfig.Id && o.StatType == (int)StatTypes.Stat);
        if (stat == null)
        {
            throw new Exception($"未找到characterid:{cconfig.Id}的stat表配置");
        }

        //初始化board
        board.InitStat(stat);
    }
}
