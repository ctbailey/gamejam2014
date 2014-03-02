using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public static class Util
	{
		public static bool HasEnemyBehaviorScript(GameObject go)
		{
			if(go.GetComponent<EnemyBehavior>() != null)
			{
				return true;
			}
			if(go.GetComponent<ZombieBarBehavior>() != null)
			{
				return true;	
			}
			return false;
		}
		public static EnemyBehavior GetEnemyBehavior(GameObject go)
		{
			EnemyBehavior script;
			script = go.GetComponent<EnemyBehavior>();
			if(script != null)
			{
				return script;
			}
			script = go.GetComponent<ZombieBarBehavior>();
			if(script != null)
			{
				return script;
			}
			return null;
		}
	}
}

