using UnityEngine;

public static class GraphFunctionLibrary
{
    #region --Custom Methods--
    
    /// <summary>
    /// Returns the method associated with the provided GraphFunctionType.
    /// </summary>
    public static GraphFunction GetGraphFunction(GraphFunctionType _type)
    {
        switch (_type)
        {
            case GraphFunctionType.Line:
                return Line;

            case GraphFunctionType.Squared:
                return Squared;

            case GraphFunctionType.Cubed:
                return Cubed;

            case GraphFunctionType.Wave:
                return Wave;

            case GraphFunctionType.MultiWave:
                return MultiWave;

            case GraphFunctionType.MultiWave2:
                return MultiWave2;

            case GraphFunctionType.Ripple:
                return Ripple;
            
            case GraphFunctionType.Ripple2:
                return Ripple2;
        }

        return null;
    }

    /// <summary>
    /// Graphs a line.
    /// </summary>
    public static float Line(float _xPos, float _zPos, float _time)
    {
        return _xPos;
    }

    /// <summary>
    /// Graphs a parabola.
    /// </summary>
    public static float Squared(float _xPos, float _zPos, float _time)
    {
        return _xPos * _xPos;
    }

    /// <summary>
    /// Graphs a cubed function.
    /// </summary>
    public static float Cubed(float _xPos, float _zPos, float _time)
    {
        return _xPos * _xPos * _xPos;
    }
    
    /// <summary>
    /// Graphs a sine wave.
    /// </summary>
    public static float Wave(float _xPos, float _zPos, float _time)
    {
        return Mathf.Sin(Mathf.PI * (_xPos + _zPos + _time));
    }

    /// <summary>
    /// Graphs multiple sine waves together.
    /// </summary>
    public static float MultiWave(float _xPos, float _zPos, float _time)
    {
        float _y = Mathf.Sin(Mathf.PI * (_xPos + _time));
        _y += Mathf.Sin(2 * Mathf.PI * (_zPos + _time)) * .5f;

        return _y * (2f / 3f);
    }
    
    /// <summary>
    /// Another example of multiple sine waves graphed together.
    /// </summary>
    public static float MultiWave2(float _xPos, float _zPos, float _time)
    {
        float _y = Mathf.Sin(Mathf.PI * (_xPos + .5f *_time));
        _y += Mathf.Sin(2 * Mathf.PI * (_zPos + _time)) * .5f;
        _y += Mathf.Sin(Mathf.PI * (_xPos + _zPos + .25f + _time)) * .5f;

        return _y * (1f / 2.5f);
    }

    /// <summary>
    /// Graphs what looks like a drop of water landing in a body of water using a sine wave.
    /// </summary>
    public static float Ripple(float _xPos, float _zPos, float _time)
    {
        float _distance = Mathf.Sqrt(_xPos * _xPos + _zPos * _zPos);
        float _y = Mathf.Sin(Mathf.PI * (4f * _distance - _time));

        return _y / (1f + 10f * _distance);
    }
    
    /// <summary>
    /// A more dramatic ripple effect using a sine wave.
    /// </summary>
    public static float Ripple2(float _xPos, float _zPos, float _time)
    {
        float _abs = Mathf.Abs(_xPos);
        float _y = Mathf.Sin(Mathf.PI * (4f * _abs - _time));

        return _y / (1f + 10f * _abs);
    }
    
    #endregion
    
}
