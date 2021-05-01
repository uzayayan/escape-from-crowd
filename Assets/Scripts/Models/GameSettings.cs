using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "SPACE/GameSettings", order = 1)]
public class GameSettings : ScriptableObject
{
    [Header("Generic")]
    public float PlayerSpeed;
    public float EnemySpeed;
    public float DistanceThreshold;
    
    [Header("Prefabs")]
    public GameObject BoxPrefab;
    public GameObject GemPrefab;
    public EnemyController EnemyPrefab;
    public BossController BossPrefab;
}
