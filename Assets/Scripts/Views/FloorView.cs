using UnityEngine;
using System.Collections.Generic;

public class FloorView : BaseView
{
    #region Serializable Fields

    [SerializeField] private bool m_isEnemyArea;
    [SerializeField] private Transform m_base;
    [SerializeField] private Transform m_datasSlot;

    #endregion
    #region Private Fields

    private FloorController floorController => (FloorController) m_baseController;

    #endregion

    /// <summary>
    /// This function called when related controller initialized.
    /// </summary>
    protected override void OnControllerInitialized()
    {
        InitializeDatas();
        base.OnControllerInitialized();
    }

    /// <summary>
    /// This function helper for initialize level data to world.
    /// </summary>
    private void InitializeDatas()
    {
        LevelContainer levelContainer = floorController.GetDatas();
        
        CreateDatas(levelContainer.Left, EDataPosition.Left);
        CreateDatas(levelContainer.Middle, EDataPosition.Middle);
        CreateDatas(levelContainer.Right, EDataPosition.Right);
    }

    /// <summary>
    /// This function helper for initialize scale of this component.
    /// </summary>
    public void InitializeScale()
    {
        Level currentLevel = GameManager.Instance.GetCurrentLevel();
        m_base.transform.localScale = new Vector3(m_base.localScale.x, m_base.localScale.y, m_isEnemyArea ? currentLevel.GetEnemyAreaLength() : currentLevel.GetPlayAreaLength());
    }

    /// <summary>
    /// This function create level data by 'E Data Position'
    /// </summary>
    /// <param name="targetList"></param>
    /// <param name="dataPosition"></param>
    private void CreateDatas(List<EDataType> targetList, EDataPosition dataPosition)
    {
        Level currentLevel = GameManager.Instance.GetCurrentLevel();
        GameSettings gameSettings = GameManager.Instance.GetGameSettings();
        
        float length = m_isEnemyArea ? currentLevel.GetEnemyAreaLength() : currentLevel.GetPlayAreaLength();
        float step = length / targetList.Count;
        float currentStep = step / 2;
        
        foreach (EDataType dataType in targetList)
        {
            switch (dataType)
            {
                case EDataType.Empty:
                    break;
                case EDataType.Wall:
                    GameObject boxObject = Instantiate(gameSettings.BoxPrefab, m_datasSlot);
                    boxObject.transform.localPosition = GetPoisitonByDataPosition(currentStep, dataPosition);
                    break;
                case EDataType.Gem:
                    GameObject gemObject = Instantiate(gameSettings.GemPrefab, m_datasSlot);
                    gemObject.transform.localPosition = GetPoisitonByDataPosition(currentStep, dataPosition);
                    break;
                case EDataType.Enemy:
                    EnemyController enemyController = Instantiate(gameSettings.EnemyPrefab, m_datasSlot);
                    enemyController.transform.localPosition = GetPoisitonByDataPosition(currentStep, dataPosition);
                    
                    GameManager.Instance.AddEnemyControllerToPool(enemyController);
                        
                    enemyController.Initialize();
                    break;
                case EDataType.Boss:
                    BossController bossController = Instantiate(gameSettings.BossPrefab, m_datasSlot);
                    bossController.transform.localPosition = GetPoisitonByDataPosition(currentStep, dataPosition);
                    
                    GameManager.Instance.AddEnemyControllerToPool(bossController);
                    
                    bossController.Initialize();
                    break;
            }
            
            currentStep += step;
        }
    }

    /// <summary>
    /// This function return position by data position.
    /// </summary>
    /// <param name="zPosition"></param>
    /// <param name="dataPosition"></param>
    /// <returns></returns>
    private Vector3 GetPoisitonByDataPosition(float zPosition, EDataPosition dataPosition)
    {
        switch (dataPosition)
        {
            case EDataPosition.Left:
                return new Vector3(-1, 0, zPosition);
            case EDataPosition.Middle:
                return new Vector3(0, 0, zPosition);
            case EDataPosition.Right:
                return new Vector3(1, 0, zPosition);
        }
        
        return Vector3.zero;
    }
}
