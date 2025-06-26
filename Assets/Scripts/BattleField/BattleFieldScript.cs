using System.Collections;
using System.Collections.Generic;
using Assets.Common;
using Assets.Common.Cache;
using Assets.Scripts.Characters;
using Assets.Scripts.Common;
using Assets.Scripts.Config.GameConfigs;
using TMPro;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;

public class BattleFieldScript : BaseMonoBehaviour
{
    public GameObject player;
    public GameObject profile;
    public GameObject prev;
    public GameObject next;
    public GameObject EnemyPrefab;

    bool autoNext = true;

    /// <summary>
    /// ����Id�����ڼ��ع����������
    /// </summary>
    public int StoryId = 1;

    //���ؿ�����Ĭ�ϵ�10��Ϊboss
    private int MaxLevel = 10;

    private int passedLevel = 0;
    int CurrentLevel = 1;
    GameObject currentEnemy;

    /// <summary>
    /// ʱ������
    /// </summary>
    public float TimeRate = 1;

    bool IsGameRunning = false;
    //private bool PlayerLocked;
    //private bool EnemyLocked;

    private float startDelay = 0.5f;

    private Character playerCharacter;
    private Character enemyCharacter;

    private List<StoryCharacter> storyCharacters;
    private List<CharacterConfig> storyEnemies;

    // Start is called before the first frame update
    void Start()
    {
        InitStoryInfo();
        //���ǲ���䣬���Ե�һ֡���أ������ڵ������ɵ�ʱ�����
        playerCharacter = player.GetComponent<Character>();
        SetLevelControllerStatus();
        playerCharacter.InitCConfig(CharacterManager.SelectedCharacterConfig);

        StartGame();
        //�ȴ�2�뿪ʼ��0.01��ִ��һ�Σ��ú�����Ϊ������Ϊ�ж�����������˫����С���������Ϊ0.01��һ��
        InvokeRepeating("PlayerAttack", startDelay, 0.01f);
        InvokeRepeating("EnemyAttack", startDelay, 0.01f);
    }

    void InitStoryInfo()
    {
        //�����ñ��ȡ��ǰ������Ϣ
        var story = CacheManager.Instance.Get<Story>(p => p.Id == StoryId);
        if (story == null)
        {
            Debug.LogError($"�½ڼ����쳣���Ҳ���{StoryId}������");
            return;
        }
        var title = GameObject.Find("Title");
        title.GetComponent<TMP_Text>().text = story.Name;
        //��ȡ���ص�������
        storyCharacters = CacheManager.Instance.GetList<StoryCharacter>(sc => sc.StoryId == StoryId);
        storyEnemies = CacheManager.Instance.GetList<CharacterConfig>(sc => storyCharacters.Exists(s => s.CharacterId == sc.Id));

        MaxLevel = storyEnemies.Count;
        //todo ���⡢���������˵ȼ���
    }

    void SetLevelControllerStatus()
    {
        prev.GetComponent<Button>().interactable = CurrentLevel != 1;
        next.GetComponent<Button>().interactable = CurrentLevel < passedLevel;
    }

    void PlayerAttack()
    {
        if (!IsGameRunning)
        {
            return;
        }

        //��������������Ϊload�����ݣ�����deadanim�Ͳ��ٲ����ˣ�Ӧ���ڲ�����Ϻ����Ϊkilled
        var oppKilled = player.GetComponent<Character>().Act(currentEnemy);

        IsGameRunning = !SomeDeadAnimPlaying();

        //����������Ĵ���
        //1 �ж���һ��
        if (oppKilled)
        {
            if (autoNext)
            {
                Next();
            }

            LoadEnemy();
            ContinueGame();
        }
    }

    void EnemyAttack()
    {
        if (!IsGameRunning)
        {
            return;
        }

        var playerKilled = currentEnemy.GetComponent<Character>().Act(player);

        IsGameRunning = !SomeDeadAnimPlaying();

        if (playerKilled)
        {
            //���¿�ʼ��ǰ�ؿ�
            RestartGameOnCurrentLevel();
        }
    }

    bool SomeDeadAnimPlaying()
    {
        return playerCharacter.IsDeadPlaying() || enemyCharacter.IsDeadPlaying();
    }

    void LoadEnemy()
    {
        if (CurrentLevel > storyEnemies.Count)
        {
            Debug.LogError("�쳣��level�����˵������ޣ��ǲ���û�����������½�?");
        }

        Destroy(currentEnemy);

        //����ĳЩ�������ص���
        currentEnemy = Instantiate(EnemyPrefab, new Vector3(5.6f, 2.75f, 0f), Quaternion.identity);

        //��ʼ����ɫ����
        enemyCharacter = currentEnemy.GetComponent<Character>();
        enemyCharacter.InitCConfig(storyEnemies[CurrentLevel - 1]);
        enemyCharacter.ShowAttributes();
    }

    void RestartGameOnCurrentLevel()
    {
        playerCharacter.Resurrection();
        LoadEnemy();
    }

    public void StartGame()
    {
        IsGameRunning = true;
        //��ʼ�����˹�����
        LoadEnemy();
    }

    public void PauseGame()
    {
        IsGameRunning = false;
    }

    public void ContinueGame()
    {
        IsGameRunning = true;
    }

    public void Next()
    {
        if (CurrentLevel < MaxLevel && storyEnemies[CurrentLevel] != null)
        {
            CurrentLevel++;
        }

        passedLevel = CurrentLevel;
        SetLevelControllerStatus();
    }

    public void Previous()
    {
        if (CurrentLevel > 1)
        {
            CurrentLevel--;
        }

        LoadEnemy();
        SetLevelControllerStatus();
    }

    public void Restart()
    {
        RestartGameOnCurrentLevel();
    }

    void Pass()
    {
        PauseGame();
        //�˳�����
    }

    /// <summary>
    /// �޸��Ƿ��Զ�ǰ������
    /// </summary>
    /// <param name="auto"></param>
    public void ChangeAutoNext(Toggle t)
    {
        autoNext = t.isOn;
        L(autoNext.ToString());
    }
}
