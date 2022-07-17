using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesUIController : MonoBehaviour
{
	public TownResourcesManager TownResources;
	public Text Food;
	public Text Army;
	public Text Population;
	public Text Confidence;

	public string Pattern = "{0}/{1}";

	public void UpdateDisplay()
	{
		Food.text = string.Format(Pattern, TownResources.Food, TownResources.Max);
		Army.text = string.Format(Pattern, TownResources.Army, TownResources.Max);
		Population.text = string.Format(Pattern, TownResources.Population, TownResources.Max);
		Confidence.text = string.Format(Pattern, TownResources.Confidence, TownResources.Max);
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
