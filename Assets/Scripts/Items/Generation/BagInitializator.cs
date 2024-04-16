using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class BagInitializator : MonoBehaviour
{
    [SerializeField]
    private List<ItemGenerationRule> generationRules = new List<ItemGenerationRule>();
    [SerializeField]
    private int level = 1;
    [SerializeField]
    private ItemsBag bag;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (bag == null)
        {
            bag = GetComponent<ItemsBag>();
        }
        bag.LayItems(generationRules.Select(rule => rule.Generate(level)).Select(item => item.GetComponent<Item>()).ToList());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
