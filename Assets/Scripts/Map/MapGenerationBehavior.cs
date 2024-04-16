using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MapBehaviour))]
public class MapGenerationBehavor : MonoBehaviour
{
    [SerializeField]
    private MapBehaviour mapBehaviour;

    public int CurrentLevel = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        var allGenerators = GetComponents<MapGenerator>();
        if (allGenerators.Length > 0 )
        {
            allGenerators[0].GenerateMap(mapBehaviour, CurrentLevel);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
