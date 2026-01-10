using UnityEngine;
using UnityEditor;

namespace Nanochip.AvatarLimbScaling
{
    public class PrefabSpawner : MonoBehaviour
    {
        // VRCF Prefab definitions
        static string prefab = "c0802794c7b9ff941873338202c5c7f7";

        static string VRCF_Path = "Packages/com.vrcfury.vrcfury";

        // MA Prefab definitions
        static string prefab_MA = "159b5232ae618f142bf758a737b55752";

        static string MA_Path = "Packages/nadena.dev.modular-avatar";


        // Toolbar Menu - VRCF
        [MenuItem("Tools/Nanochip/Spawn Avatar Limb Scaling Prefab... [VRCFury]", false, 0)]
        [MenuItem("GameObject/Nanochip/Spawn Avatar Limb Scaling Prefab... [VRCFury]", false, 0)]
        private static void SpawnPrefab()
        {
            SpawnPrefab(prefab);
        }

        // Enable or disable menu items dynamically
        [MenuItem("Tools/Nanochip/Spawn Avatar Limb Scaling Prefab... [VRCFury]", true)]
        [MenuItem("GameObject/Nanochip/Spawn Avatar Limb Scaling Prefab... [VRCFury]", true)]
        private static bool ValidateSpawnPrefab()
        {
            return AssetDatabase.IsValidFolder(VRCF_Path);
        }

        // Toolbar Menu - MA
        [MenuItem("Tools/Nanochip/Spawn Avatar Limb Scaling Prefab... [ModularAvatar]", false, 1)]
        [MenuItem("GameObject/Nanochip/Spawn Avatar Limb Scaling Prefab... [ModularAvatar]", false, 1)]
        private static void SpawnPrefab_MA()
        {
            SpawnPrefab(prefab_MA);
        }

        // Enable or disable menu items dynamically for MA
        [MenuItem("Tools/Nanochip/Spawn Avatar Limb Scaling Prefab... [ModularAvatar]", true)]
        [MenuItem("GameObject/Nanochip/Spawn Avatar Limb Scaling Prefab... [ModularAvatar]", true)]
        private static bool ValidateSpawnPrefab_MA()
        {
            return AssetDatabase.IsValidFolder(MA_Path);
        }

        // Prefab Spawner
        private static void SpawnPrefab(string guid)
        {
            string prefabPath = AssetDatabase.GUIDToAssetPath(guid);

            if (string.IsNullOrEmpty(prefabPath))
            {
                Debug.LogError("Prefab with GUID " + guid + " not found.");
                return;
            }

            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            GameObject selectedObject = Selection.activeGameObject;

            if (!prefab)
            {
                Debug.LogError("Failed to load prefab with GUID " + guid + " at path " + prefabPath);
                return;
            }

            GameObject instantiatedPrefab = (GameObject)PrefabUtility.InstantiatePrefab(prefab);

            if (selectedObject)
            {
                instantiatedPrefab.transform.parent = selectedObject.transform;
            }

            if (instantiatedPrefab)
            {
                EditorGUIUtility.PingObject(instantiatedPrefab);
            }
            else
            {
                Debug.LogError("Failed to instantiate prefab with GUID " + guid);
            }
        }
    }
}