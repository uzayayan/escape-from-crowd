using UnityEngine;

[ExecuteAlways]
public class BoundingBoxView : MonoBehaviour
{
    #region Serializable Fields

    [SerializeField]
    private Color Color = Color.white;
    
    [SerializeField]
    private Bounds bounds;

    #endregion
    
    /// <summary>
    /// Awake
    /// </summary>
    private void Awake()
    {
        bounds.center = transform.position;
    }

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        bounds.center = transform.position;
    }

    /// <summary>
    /// This function return random point in bounding area.
    /// </summary>
    /// <returns></returns>
    public Vector3 GetRandomPoint()
    {
        bounds.center = transform.position;

        Vector3 target = bounds.center + new Vector3(
                             Random.Range(-bounds.extents.x, bounds.extents.x),
                             -0.5F,
                             Random.Range(-bounds.extents.z, bounds.extents.z)
                         );
        
        return target;
    }

    public Vector3 GetScale()
    {
        return bounds.size;
    }

    public float GetLength()
    {
        return GetScale().z;
    }

    /// <summary>
    /// On Draw Gizmos
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color;
        Gizmos.DrawWireCube(bounds.center, bounds.size);
    }
}
