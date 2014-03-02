using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class PlayerFootstepsAudio : MonoBehaviour {
	RoundRobinAudioCollection footstepSounds;
	// Use this for initialization
	void Start () {
		footstepSounds = new RoundRobinAudioCollection(GetComponents<AudioSource>());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void PlayFootstep()
	{
		footstepSounds.PlayRandomSound();
	}
}
