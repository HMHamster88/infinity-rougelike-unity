using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(MapBehaviour))]
public class MapGenerationBehavor : InjectComponentBehaviour
{
    [SerializeField]
    private MapBehaviour mapBehaviour;
    [GetComponent]
    private Minimap minimap;

    private MapGenerator[] mapGenerators;

    public int CurrentLevel = 1;

    protected override void OnEnable()
    {
        base.OnEnable();
        mapGenerators = Resources.LoadAll<MapGenerator>("ScriptableObjects/MapGenerators");
    }

    // Start is called before the first frame update
    void Start()
    {
        var rule = mapGenerators.Where(rule => rule.MinimalLevel <= CurrentLevel)
            .ToList()
            .GetRandomElement();
        rule.GenerateMap(mapBehaviour, CurrentLevel);
        minimap.InitMinimap(rule.Width, rule.Height);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
