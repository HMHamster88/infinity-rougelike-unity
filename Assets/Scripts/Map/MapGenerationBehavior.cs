using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(MapBehaviour))]
public class MapGenerationBehavor : MonoBehaviour
{
    [SerializeField]
    private MapBehaviour mapBehaviour;

    private MapGenerator[] mapGenerators;

    public int CurrentLevel = 1;

    private void OnEnable()
    {
        mapGenerators = Resources.LoadAll<MapGenerator>("ScriptableObjects/MapGenerators");
    }

    // Start is called before the first frame update
    void Start()
    {
        var rule = mapGenerators.Where(rule => rule.MinimalLevel <= CurrentLevel)
            .ToList()
            .GetRandomElement();
       rule.GenerateMap(mapBehaviour, CurrentLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
