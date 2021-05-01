using System.Linq;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

public class MainManager : Singleton<MainManager>
{
    #region Serialziable Fields

    [SerializeField] private Level[] m_levels;
    [SerializeField] private Weapon[] m_weapons;
    
    #endregion
    #region Private Fields

    private Player player;
    private Level level;

    #endregion

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        Application.targetFrameRate = 60;
        
        InitializePlayer();
        InitializeLevel();
    }

    /// <summary>
    /// This function helper for initialize Player.
    /// </summary>
    private void InitializePlayer()
    {
        player = DataService.LoadObjectWithKey<Player>(CommonTypes.PLAYER_DATA_KEY);

        if (player == null)
        {
            player = new Player();
        }
        
        Debug.Log("Player Initialized.");
    }

    /// <summary>
    /// This function helper for initialize Level.
    /// </summary>
    private void InitializeLevel()
    {
        int savedLevelId = player.Level;
        level = m_levels.SingleOrDefault(x => x.Id == savedLevelId);

        if (level == null)
        {
            player.Level = 0;
            InitializeLevel();
            
            return;
        }

        SceneManager.LoadScene("Game");
        Debug.Log($"Level Initialized. ID : {savedLevelId}");
    }

    /// <summary>
    /// This function helper for load next level.
    /// </summary>
    public void NextLevel()
    {
        int nextLevelId = level.Id + 1;
        player.Level = nextLevelId;
        
        InitializeLevel();
    }
    
    /// <summary>
    /// This function helper for load next level.
    /// </summary>
    public void RetryLevel()
    {
        InitializeLevel();
    }

    /// <summary>
    /// This function helper for save 'Player' data.
    /// </summary>
    public void SavePlayerData()
    {
        player.SaveData();
        Debug.Log("Player Data is Saved.");
    }

    /// <summary>
    /// This function return current level.
    /// </summary>
    /// <returns></returns>
    public Level GetLevel()
    {
        return level;
    }
    
    /// <summary>
    /// This function return related 'Player' component.
    /// </summary>
    /// <returns></returns>
    public Player GetPlayer()
    {
        return player;
    }

    /// <summary>
    /// This function return all related weapons.
    /// </summary>
    /// <returns></returns>
    public Weapon[] GetAllWeapons()
    {
        return m_weapons;
    }

    /// <summary>
    /// This function called when game is paused.
    /// </summary>
    /// <param name="pauseStatus"></param>
    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SavePlayerData();
        }
    }

    /// <summary>
    /// This function called when this component destroyed.
    /// </summary>
    protected override void OnDestroy()
    {
        SavePlayerData();
        
        base.OnDestroy();
    }
}
