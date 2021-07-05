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
    /// A List of all the points instantiated to create the graph.
    /// </summary>
    private readonly List<GameObject> _points = new List<GameObject>();
    
    #endregion
    
    #region --Unity Specific Methods--
    
    private void Start()
    {
        Init();
    }

    private void Update()
    {
        AnimateGraph();
    }
    
    #endregion
    
    #region --Custom Methods--

    private void Init()
    {
        Graph2D();
    }
 
    /// <summary>
    /// Creates a two dimensional representation of the selected _graphFunctionType.
    /// </summary>
    private void Graph2D()
    {
        GraphFunction _function = GraphFunctionLibrary.GetGraphFunction(_graphFunctionType);
        
        //Scale is changed to make sure all points fit within our domain of -1 to 1 based on how many points are set for the resolution.
        float _step = 2f / _resolution;
        Vector3 _scale = Vector3.one * _step;
        
        Vector3 _position = Vector3.zero;
        for (int i = 0; i < _resolution; i++)
        {
            GameObject _point = Instantiate(_pointPrefab, transform, true);
            _points.Add(_point);
            
            //Position each point side by side starting from the left shifted right .5 units (radius) so they aren't overlapping.
            //Factor in the point's adjusted scale.
            _position.x = (i + .5f) * _step - 1f;
            
            _position.y = _function(_position.x, Time.time);
            _point.transform.position = _position;
            _point.transform.localScale = _scale;
        }
    }

    /// <summary>
    /// Animates the points of the graph based on the currently selected _graphFunctionType.
    /// </summary>
    private void AnimateGraph()
    {
        for (int i = 0; i < _resolution; i++)
        {
            GraphFunction _function = GraphFunctionLibrary.GetGraphFunction(_graphFunctionType);
            GameObject _point = _points[i];
            Vector3 _position = _point.transform.position;
            _position.y = _function(_position.x, Time.time);
            _point.transform.position = _position;
        }
    }

    #endregion
    
}
