using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "SPACE/Weapon", order = 3)]
public class Weapon : ScriptableObject
{
    [Header("Generic")]
    public int Id;
    public int Damage;
    public int Capacity;
    public float ReloadTime;
    public float TriggerDelay;
    
    [Header("Prefabs")]
    public WeaponController Prefab;
    
    [Header("Misc")]
    public Sprite Icon;
    public AudioClip FireClip;
    public AudioClip ReloadClip;
}