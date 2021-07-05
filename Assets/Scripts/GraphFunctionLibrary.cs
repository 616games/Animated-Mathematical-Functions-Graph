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
        }

        return null;
    }

    /// <summary>
    /// Graphs a line.
    /// f(x) = x.
    /// </summary>
    private static float Line(float _xInput, float _time)
    {
        return _xInput;
    }

    /// <summary>
    /// Graphs a parabola.
    /// f(x) = x * x.
    /// </summary>
    private static float Squared(float _xInput, float _time)
    {
        return _xInput * _xInput;
    }

    /// <summary>
    /// Graphs a cubed function.
    /// f(x) = x * x * x.
    /// </summary>
    private static float Cubed(float _xInput, float _time)
    {
        return _xInput * _xInput * _xInput;
    }
    
    /// <summary>
    /// Graphs a single sine wave with its full period of 2PI and animates over time.
    /// f(x, t) = sin(PI(x + t)). 
    /// </summary>
    private static float Wave(float _xInput, float _time)
    {
        return Mathf.Sin(Mathf.PI * (_xInput + _time));
    }

    /// <summary>
    /// Graphs two sine waves added together with different frequencies and animates over time.
    /// f1(x, t) = sin(PI(x + t))
    /// f2(x, t) = .5sin(2PI(x + t))
    /// divisor = 1.5 (to make sure the result fits within our -1 to 1 domain)
    /// (f1 + f2) / 1.5 => (f1 + f2) / (3/2) => (2/3)(f1 + f2)
    /// </summary>
    private static float MultiWave(float _xInput, float _time)
    {
        float _f1 = Mathf.Sin(Mathf.PI * (_xInput + _time));
        float _f2 = .5f * Mathf.Sin(2 * Mathf.PI * (_xInput + _time));

        //We multiple the output by 1.5 to make sure it fits within our -1 to 1 domain.
        return (2f / 3f) * (_f1 + _f2);
    }
    
    /// <summary>
    /// Graphs three sine waves added together with different frequencies and animates over time.
    /// f1(x, t) = sin(PI(x + .5t))
    /// f2(x, t) = .5sin(2PI(x + t))
    /// f3(x, t) = .5sin(PI(x + .25t))
    /// divisor = 1.5 (to make sure the result fits within our -1 to 1 domain)
    /// (f1 + f2 + f3) / 1.5 => (f1 + f2 + f3) / (3/2) => (2/3)(f1 + f2 + f3)
    /// </summary>
    private static float MultiWave2(float _xInput, float _time)
    {
        float _f1 = Mathf.Sin(Mathf.PI * (_xInput + .5f *_time));
        float _f2 = .5f * Mathf.Sin(2 * Mathf.PI * (_xInput + _time));
        float _f3 = .5f * Mathf.Sin(Mathf.PI * (_xInput + .25f * _time));

        return (2f / 3f) * (_f1 + _f2 + _f3);
    }

    /// <summary>
    /// Graphs a sine wave oscillating away from the center of the graph and decreasing its amplitude over time.
    /// d = distance from center (absolute value of x input).
    /// f(x, t) = sin(PI(4d - t))
    /// divisor = 1 + 10d (to decrease amplitude over time)
    /// f / (1 + 10d)
    /// </summary>
    private static float Ripple(float _xInput, float _time)
    {
        float _distance = Mathf.Abs(_xInput);
        float _f = Mathf.Sin(Mathf.PI * (4f * _distance - _time));

        return _f / (1f + 10f * _distance);
    }
    
    #endregion
    
}
