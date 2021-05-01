using UnityEngine;

public static class CommonTypes
{
    //GENERIC FIELDS
    public static readonly float AREA_SIZE_MULTIPLIER = 7.5F;
    
    //CAMERA FIELDS
    public static readonly float CAMERA_SPEED = 5;
    public static readonly float CAMERA_LERP_TIME = 1;

    //PLAYER FIELDS
    public static readonly float PLAYER_SPEED_MULTIPLIER = 1;
    public static readonly float PLAYER_CLAMP_X_AXIS = 1.5F;
    
    //ENEMY FIELDS
    public static readonly float ENEMY_SPEED_MULTIPLIER = 1;
    
    //ANIMATOR FIELDS
    public static readonly string ANIMATOR_RUN = "Run";
    public static readonly string ANIMATOR_DEATH = "Death";
    public static readonly string ANIMATOR_DANCE = "Dance";

    //USER INTERFACE FIELDS
    public static readonly float FLY_GEM_TIME = 0.5F;
    public static readonly float FLOATING_TEXT_TIME = 0.5F;
    
    //KEYS
    public static readonly string PLAYER_DATA_KEY = "player_data";
    public static readonly string CAMERA_TWEEN_KEY = "camera_tween";
    public static readonly string SOUND_STATE_KEY = "sound_state";
}

public static class GameUtils
{
    public static Vector2 WorldToCanvasPosition(RectTransform canvas, Camera camera, Vector3 position) 
    {
        Vector2 temp = camera.WorldToViewportPoint(position);
        
        temp.x *= canvas.sizeDelta.x;
        temp.y *= canvas.sizeDelta.y;

        temp.x -= canvas.sizeDelta.x * canvas.pivot.x;
        temp.y -= canvas.sizeDelta.y * canvas.pivot.y;
 
        return temp;
    }
}