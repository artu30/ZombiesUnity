using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

public class ZombieWaveFactory {

    [MenuItem("Assets/Create/Zombie Assets/Zombie Wave")]
    public static void CreateZombieWave()
    {
        ZombieWave zw = ScriptableObject.CreateInstance<ZombieWave>();
        AssetDatabase.CreateAsset(zw, "Assets/Resources/zombie_wave.asset");
        AssetDatabase.SaveAssets();
    }
}
