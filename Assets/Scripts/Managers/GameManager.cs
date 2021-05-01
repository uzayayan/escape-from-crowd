using System;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class GameManager : Singleton<GameManager>
{
    #region Public Fields

    public Action GameStarted;
    public Action GameCompleted;
    public Action GameOver;

    #endregion
    #region Serializable Fields

    [SerializeField] private GameSettings m_gameSettings;
    [SerializeField] private NavMeshSurface m_navMeshSurface;
    [SerializeField] private PlayerController m_playerController;
    [SerializeField] private List<EnemyController> m_enemyControllers;
    [SerializeField] private FloorController m_playerFloorController;
    [SerializeField] private FloorController m_enemyFloorController;
    [SerializeField] private FinishController m_finishController;

    #endregion
    #region Private Fields

    private bool isGameStarted;
    private bool isGameCompleted;
    private bool isGameOver;
    
    public Level currentLevel;

    #endregion

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        currentLevel = MainManager.Instance.GetLevel();
        RenderSettings.skybox = currentLevel.Skybox;
    }

    /// <summary>
    /// This function helper for start the game.
    /// </summary>
    public void StartGame()
    {
        if(isGameStarted)
            return;

        isGameStarted = true;

        FloorView playerFloorView = (FloorView) m_playerFloorController.GetView();
        FloorView enemyFloorView = (FloorView) m_enemyFloorController.GetView();
        
        playerFloorView.InitializeScale();
        enemyFloorView.InitializeScale();
        
        BuildNavigationMesh();

        m_playerFloorController.Initialize(currentLevel.PlayArea);
        m_enemyFloorController.Initialize(currentLevel.EnemyArea);
        m_playerController.Initialize();

        BuildNavigationMesh();

        GameStarted?.Invoke();
        Debug.Log("Game Is Started.");
    } 

    /// <summary>
    /// This function return related 'Game Settings'.
    /// </summary>
    /// <returns></returns>
    public GameSettings GetGameSettings()
    {
        return m_gameSettings;
    }

    /// <summary>
    /// This function return related 'Player Controller'.
    /// </summary>
    /// <returns></returns>
    public PlayerController GetPlayerController()
    {
        return m_playerController;
    }
    
    /// <summary>
    /// This function return related 'Finish Controller'.
    /// </summary>
    /// <returns></returns>
    public FinishController GetFinishController()
    {
        return m_finishController;
    }
    
    /// <summary>
    /// This function return related 'Enemy Controllers'.
    /// </summary>
    /// <returns></returns>
    public List<EnemyController> GetAllEnemyControllers()
    {
        return m_enemyControllers.Where(x=> !x.IsKilled()).ToList();
    }

    /// <summary>
    /// This function helper for add 'Enemy Controller' to pool.
    /// </summary>
    /// <param name="enemyController"></param>
    public void AddEnemyControllerToPool(EnemyController enemyController)
    {
        m_enemyControllers.Add(enemyController);
    }

    /// <summary>
    /// This function helper for over the game.
    /// </summary>
    public void CompleteLevel()
    {
        isGameCompleted = true;
        m_enemyControllers.ForEach(x=> Destroy(x.gameObject));

        GameCompleted?.Invoke();
        
        InterfaceManager.Instance.OpenEndGamePanel();
        Debug.Log("Level Completed.");
    }
    
    /// <summary>
    /// This function helper for over the game.
    /// </summary>
    public void OverGame()
    {
        isGameOver = true;
        m_enemyControllers.ForEach(x=> x.Stop());
        
        GameOver?.Invoke();
        
        InterfaceManager.Instance.OpenEndGamePanel();
        Debug.Log("Game Over.");
    }

    /// <summary>
    /// This function helper for build navigation mesh.
    /// </summary>
    public void BuildNavigationMesh()
    {
        m_navMeshSurface.BuildNavMesh();
        Debug.Log("Navigation Mesh Builded.");
    }

    /// <summary>
    /// This function return related 'Current Level'.
    /// </summary>
    /// <returns></returns>
    public Level GetCurrentLevel()
    {
        return currentLevel;
    }

    /// <summary>
    /// This function return true if game is started.
    /// </summary>
    /// <returns></returns>
    public bool IsGameStarted()
    {
        return isGameStarted;
    }
    
    /// <summary>
    /// This function return true if game is completed.
    /// </summary>
    /// <returns></returns>
    public bool IsGameCompleted()
    {
        return isGameCompleted;
    }
    
    /// <summary>
    /// This function return true if game is over.
    /// </summary>
    /// <returns></returns>
    public bool IsGameOver()
    {
        return isGameOver;
    }
}
