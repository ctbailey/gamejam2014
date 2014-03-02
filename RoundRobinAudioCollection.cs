using System;
using UnityEngine;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public class RoundRobinAudioCollection
	{
		private int count = 0;
		private AudioSource[] _audioSources;
		List<int> unusedIndices;
		public RoundRobinAudioCollection (AudioSource[] audioSources)
		{
			unusedIndices = new List<int>();
			_audioSources = audioSources;
		}
		public void PlayRandomSound()
		{
			Debug.Log(unusedIndices.Count);
			if(unusedIndices.Count == 0)
			{
				count++;
				for(int i = 0; i < _audioSources.Length; i++)
				{
					unusedIndices.Add (i);	
				}
			}
			int randomUnusedSoundIndex = UnityEngine.Random.Range(0, unusedIndices.Count - 1);
			int randomIndex = unusedIndices[randomUnusedSoundIndex];
			_audioSources[randomIndex].PlayOneShot(_audioSources[randomIndex].clip);
			unusedIndices.RemoveAt(randomUnusedSoundIndex);
		}
	}
}

