using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    #region --Fields / Properties--
    
    /// <summary>
    /// Prefab of the point used in the graph.
    /// </summary>
    [SerializeField]
    private GameObject _pointPrefab;

    /// <summary>
    /// Controls how many points are rendered.
    /// </summary>
    [SerializeField, Range(10, 100)]
    private int _resolution;

    /// <summary>
    /// The type of graph to be created.
    /// </summary>
    [SerializeField]
    private GraphFunctionType _graphFunctionType;

    /// <summary>
    /// The dimension of the graph.
    /// </summary>
    [SerializeField]
    private Dimension _activeDimension;

    /// <summary>
    /// A List of all the points instantiated to create the graph.
    /// </summary>
    private List<GameObject> _points = new List<GameObject>();
    
    /// <summary>
    /// Identifies when the user has chosen to switch the graph to a different dimension.
    /// </summary>
    private bool _switchGraph;
    
    /// <summary>
    /// Stores the previous dimension the graph was in before it was switched to a new one.
    /// </summary>
    private Dimension _previousActiveDimension;
    
    #endregion
    
    #region --Unity Specific Methods--
    
    private void Start()
    {
        Init();
    }

    private void Update()
    {
        //Checks if the user has requested a different dimension for the graph.
        if(_activeDimension != _previousActiveDimension) SwitchDimension();
        AnimateGraph();
    }
    
    #endregion
    
    #region --Custom Methods--

    private void Init()
    {
        SwitchDimension();
        switch (_activeDimension)
        {
            case Dimension.Two:
                Graph2D();
                break;

            case Dimension.Three:
                Graph3D();
                break;
        }
    }

    /// <summary>
    /// Destroys the existing points of the current dimension to prepare the Scene to graph the new points for the selected dimension.
    /// </summary>
    private void SwitchDimension()
    {
        _switchGraph = true;
        _previousActiveDimension = _activeDimension;
        for (int i = 0; i < _points.Count; i++)
        {
            Destroy(_points[i]);
        }
        
        _points.Clear();
    }
    
    /// <summary>
    /// Creates a two dimensional representation of the selected _graphFunctionType.
    /// </summary>
    private void Graph2D()
    {
        if (!_switchGraph) return;
        
        GraphFunction _function = GraphFunctionLibrary.GetGraphFunction(_graphFunctionType);
        float _step = 2f / _resolution;
        Vector3 _position = Vector3.zero;
        Vector3 _scale = Vector3.one * _step;
        for (int i = 0; i < _resolution; i++)
        {
            GameObject _point = Instantiate(_pointPrefab, transform, true);
            _points.Add(_point);
            _position.x = (i + .5f) * _step - 1f;
            _position.y = _function(_position.x, 0, Time.time);
            _point.transform.position = _position;
            _point.transform.localScale = _scale;
        }

        _switchGraph = false;
    }

    /// <summary>
    /// /// Creates a three dimensional representation of the selected _graphFunctionType.
    /// </summary>
    private void Graph3D()
    {
        if (!_switchGraph) return;
        GraphFunction _function = GraphFunctionLibrary.GetGraphFunction(_graphFunctionType);
        float _step = 2f / _resolution;
        Vector3 _position = Vector3.zero;
        Vector3 _scale = Vector3.one * _step;
        for (int i = 0, x = 0, z = 0; i < _resolution * _resolution; i++, x++)
        {
            if (x == _resolution)
            {
                x = 0;
                z++;
            }
            
            GameObject _point = Instantiate(_pointPrefab, transform, true);
            _points.Add(_point);
            _position.x = (x + .5f) * _step - 1f;
            _position.z = (z + .5f) * _step - 1f;
            _position.y = _function(_position.x, _position.z, Time.time);
            _point.transform.position = _position;
            _point.transform.localScale = _scale;
        }
        
        _switchGraph = false;
    }

    /// <summary>
    /// Animates the points of the graph.
    /// </summary>
    private void AnimateGraph()
    {
        switch (_activeDimension)
        {
            case Dimension.Two:
            {
                if(_switchGraph) Graph2D();
                
                for (int i = 0; i < _resolution; i++)
                {
                    GraphFunction _function = GraphFunctionLibrary.GetGraphFunction(_graphFunctionType);
                    GameObject _point = _points[i];
                    Vector3 _position = _point.transform.position;
                    _position.y = _function(_position.x, _position.z, Time.time);
                    _point.transform.position = _position;
                }
                break;
            }

            case Dimension.Three:
                
                if(_switchGraph) Graph3D();
                
                for (int i = 0; i < _resolution * _resolution; i++)
                {
                    GraphFunction _function = GraphFunctionLibrary.GetGraphFunction(_graphFunctionType);
                    GameObject _point = _points[i];
                    Vector3 _position = _point.transform.position;
                    _position.y = _function(_position.x, _position.z, Time.time);
                    _point.transform.position = _position;
                }
                break;
        }
    }

    #endregion
    
}
