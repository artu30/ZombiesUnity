using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ZombieFactory {

    [MenuItem("Assets/Create/Zombie Assets/Zombie Tank")]
    public static void CreateZombieTank()
    {
        TankZombie z = ScriptableObject.CreateInstance<TankZombie>();
		if (!AssetDatabase.IsValidFolder("Assets/Resources/Zombies"))
			AssetDatabase.CreateFolder ("Assets/Resources", "Zombies");
        AssetDatabase.CreateAsset(z, "Assets/Resources/Zombies/tank_zombie.asset");
        AssetDatabase.SaveAssets();
    }

    [MenuItem("Assets/Create/Zombie Assets/Zombie Slow")]
    public static void CreateZombieBase()
    {
		SlowZombie z = ScriptableObject.CreateInstance<SlowZombie>();
        if (!AssetDatabase.IsValidFolder("Assets/Resources/Zombies"))
            AssetDatabase.CreateFolder("Assets/Resources", "Zombies");
        AssetDatabase.CreateAsset(z, "Assets/Resources/Zombies/slow_zombie.asset");
        AssetDatabase.SaveAssets();
    }

}
