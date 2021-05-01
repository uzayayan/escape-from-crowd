public class FloorController : BaseController
{
    #region Private Fields

    private LevelContainer datas;

    #endregion

    /// <summary>
    /// This function helper for initialize this component.
    /// </summary>
    /// 0 = LevelContainer
    /// <param name="parameters"></param>
    public override void Initialize(params object[] parameters)
    {
        datas = (LevelContainer) parameters[0];
        
        base.Initialize(parameters);
    }
    
    public void UpdateScale(){
    
    }
    /// <summary>
    /// This function return related 'Level Data'.
    /// </summary>
    /// <returns></returns>
    public LevelContainer GetDatas()
    {
        return datas;
    }
}
