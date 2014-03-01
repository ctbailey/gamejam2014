using UnityEngine;
using System.Collections;

public class BlockColorChanger : MonoBehaviour {
	public float fadeCycleDuration;
	public float brightnessFluctuationScale = 0.1f;
	public float colorFluctuationScale = 0.3f;
	public float colorScale = 0.5f;
	private float baseLightIntensity;
	private Color baseColor;
	private float currentCycleStartTime;
	
	// Use this for initialization
	void Start () {
		baseColor = new Color(light.color.r + colorScale, light.color.g + colorScale, light.color.b + colorScale);
		renderer.material.color = baseColor;
		currentCycleStartTime = Time.time;
		baseLightIntensity = light.intensity;
	}
	
	// Update is called once per frame
	void Update () {
		float progressThroughCurrentCycle = Update_CycleProgress();
		
		float variation = 
				((Mathf.Sin(progressThroughCurrentCycle*2.0f*Mathf.PI) 
					* brightnessFluctuationScale));
		Color newColor = new Color(
			baseColor.r + (variation * colorFluctuationScale), 
			baseColor.g + (variation * colorFluctuationScale), 
			baseColor.b + (variation * colorFluctuationScale));
		light.intensity = baseLightIntensity + (variation * brightnessFluctuationScale);
		renderer.material.color = newColor;
	}
	float Update_CycleProgress()
	{
		float timeElapsedInCurrentCycle = Time.time - currentCycleStartTime;
		if(timeElapsedInCurrentCycle > fadeCycleDuration)
		{
			currentCycleStartTime = Time.time;
			timeElapsedInCurrentCycle = 0;
		}
		return timeElapsedInCurrentCycle/fadeCycleDuration;
	}
}
