using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AbilitiesFactory {

	[MenuItem("Assets/Create/Zombie Assets/Abilities/Shield Ability")]
	public static void CreateShieldAbility()
	{
		ShieldAbility z = ScriptableObject.CreateInstance<ShieldAbility>();
		if (!AssetDatabase.IsValidFolder("Assets/Resources/Zombies"))
			AssetDatabase.CreateFolder ("Assets/Resources", "Zombies");
		if (!AssetDatabase.IsValidFolder("Assets/Resources/Zombies/Abilities"))
			AssetDatabase.CreateFolder ("Assets/Resources/Zombies", "Abilities");
		AssetDatabase.CreateAsset(z, "Assets/Resources/Zombies/Abilities/shield_ability.asset");
		AssetDatabase.SaveAssets();
	}
}
