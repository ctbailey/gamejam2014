using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class PlayerJumpAudio : MonoBehaviour {
	RoundRobinAudioCollection jumpSounds;
	// Use this for initialization
	void Start () {
		jumpSounds = new RoundRobinAudioCollection(GetComponents<AudioSource>());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void PlayJump()
	{
		jumpSounds.PlayRandomSound();
	}
}
