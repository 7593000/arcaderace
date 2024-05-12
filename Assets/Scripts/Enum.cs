
public enum Axes : byte
{
    Front,
    Rear
}

public enum SaveDataType
{
    SettingsPlayer,
    SettingsEnemy,
    Score
}

public enum DataSettings
{/// <summary>
/// Цвет авто
/// </summary>
    Texture,
    /// <summary>
    /// Развал колеса ( макс. угол поворота ) 
    /// </summary>
    Collapse,
    /// <summary>
    /// Клиренс
    /// </summary>
    Clearance,
    /// <summary>
    /// Жесткость пружины
    /// </summary>
    Stiffness,
    /// <summary>
    ///  жесткость амортизатора
    /// </summary>
    Damper


}

public enum TriggerFinish
{
    Finish,
    AntiFinish,
    ResetAntiFinish
}