/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using System.Collections.Generic;
using System.Linq;
using InfinityCode.UltimateEditorEnhancer.UnityTypes;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace InfinityCode.UltimateEditorEnhancer.HierarchyTools
{
    [InitializeOnLoad]
    public static class BestIconDrawer
    {
        private const double CacheLifeTimeSec = 5;

        private static Texture _prefabIcon;
        private static Texture _unityLogoTexture;
        private static readonly HashSet<int> hierarchyWindows;
        private static bool inited = false;
#if UNITY_2021_4_OR_NEWER
        private static readonly Dictionary<int, CachedTexture> cachedTextures = new();
#else
        private static readonly Dictionary<int, CachedTexture> cachedTextures = new Dictionary<int, CachedTexture>();
#endif
        private static readonly double lastUpdateTime;

        private static Texture prefabIcon
        {
            get {
                if (_prefabIcon == null) _prefabIcon = EditorIconContents.prefab.image;
                return _prefabIcon;
            }
        }

        private static Texture unityLogoTexture
        {
            get {
                if (_unityLogoTexture == null) _unityLogoTexture = EditorIconContents.unityLogo.image;
                return _unityLogoTexture;
            }
        }

        static BestIconDrawer()
        {
            hierarchyWindows = new HashSet<int>();
            HierarchyItemDrawer.Register("BestIconDrawer", DrawItem, HierarchyToolOrder.BestIcon);

            //EditorApplication.update += DelayedInit;
            lastUpdateTime = EditorApplication.timeSinceStartup;
        }

        private static void DelayedInit()
        {
            if ((EditorApplication.timeSinceStartup - lastUpdateTime) < 1) return;
            EditorApplication.update -= DelayedInit;

            Init();
        }

        private static void DrawItem(HierarchyItem item)
        {
            if (!Prefs.hierarchyOverrideMainIcon) return;
            if (!inited) Init();

            Event e = Event.current;

            if (e.type == EventType.Layout)
            {
                EditorWindow lastHierarchyWindow = SceneHierarchyWindowRef.GetLastInteractedHierarchy();
                int wid = lastHierarchyWindow.GetInstanceID();
                if (!hierarchyWindows.Contains(wid)) InitWindow(lastHierarchyWindow, wid);
                return;
            }

            if (e.type != EventType.Repaint) return;

            if (!GetTexture(item, out Texture texture)) return;
            if (texture == null) return;

            const int iconSize = 16;

            Rect rect = item.rect;
#if UNITY_2021_4_OR_NEWER
            Rect iconRect = new(rect) {width = iconSize, height = iconSize};
#else
            Rect iconRect = new Rect(rect.x, rect.y, iconSize, iconSize);
#endif
            iconRect.y += (rect.height - iconSize) / 2;
            GUI.DrawTexture(iconRect, texture, ScaleMode.ScaleToFit);
        }

        private static void FirstInit(int id, Rect rect)
        {
            EditorApplication.hierarchyWindowItemOnGUI -= FirstInit;
            Init();
        }

        private static Component GetBestComponent(GameObject go)
        {
            Component[] components = go.GetComponents<Component>();
            if (components.Length == 1) return components[0];


            Component best = components[1];

            // Controlla se "best" è uno script mancante
            if (best == null ||
                best.GetType() == typeof(MonoBehaviour) &&
                best.GetType().Name == "MissingMonoScript")
            {
                bool foundValidComponent = false;
                for (int i = 2; i < components.Length; i++)
                {
                    if (components[i] != null && !(components[i].GetType() == typeof(MonoBehaviour) && components[i].GetType().Name == "MissingMonoScript"))
                    {
                        best = components[i];
                        foundValidComponent = true;
                        break;
                    }
                }
                if (!foundValidComponent)
                {
                    best = components[0];
                }
            }


            if (components.Length == 2) return best;

#if UNITY_2021_4_OR_NEWER
            if (best is not CanvasRenderer) return best;
            best = components[2];
            if (components.Length == 3 || (best is not UnityEngine.UI.Image)) return best;
#else
            if (best.GetType() != typeof(CanvasRenderer)) return best;
            best = components[2];
            if (components.Length == 3 || (best.GetType() != typeof(UnityEngine.UI.Image))) return best;
#endif

            Component c = components[3];
            Texture texture = AssetPreview.GetMiniThumbnail(c);
            if (texture == null) return best;

            string textureName = texture.name;
#if UNITY_2021_4_OR_NEWER
            return textureName is "cs Script Icon" or "d_cs Script Icon" ? best : c;
#else
            return ((textureName is "cs Script Icon") || (textureName is "d_cs Script Icon")) ? best : c;
#endif
        }

        public static Texture GetGameObjectIcon(GameObject go)
        {
            if (go.CompareTag("Collection"))
            {
                return Icons.collection;
            }

            Texture texture = AssetPreview.GetMiniThumbnail(go);
            string textureName = texture.name;

#if UNITY_2021_4_OR_NEWER
            if (textureName is "d_Prefab Icon" or "Prefab Icon")
            {
                if (PrefabUtility.IsAnyPrefabInstanceRoot(go)) return prefabIcon;
            }
            else if (textureName is not "d_GameObject Icon" and not "GameObject Icon")
            {
                return texture;
            }
#else
            if ((textureName is "d_Prefab Icon") || (textureName is "Prefab Icon"))
            {
                if (PrefabUtility.IsAnyPrefabInstanceRoot(go)) return prefabIcon;
            }
            else if ((textureName != "d_GameObject Icon") && (textureName != "GameObject Icon"))
            {
                return texture;
            }
#endif

            Component best = GetBestComponent(go);
            texture = AssetPreview.GetMiniThumbnail(best);

            if (texture == null) return EditorIconContents.gameObject.image;
            return texture;
        }

        private static bool GetTexture(HierarchyItem item, out Texture texture)
        {
            texture = null;
            if (cachedTextures.TryGetValue(item.id, out CachedTexture cachedTexture))
            {
                if (EditorApplication.timeSinceStartup - cachedTexture.time < CacheLifeTimeSec)
                {
                    texture = cachedTexture.texture;
                    return true;
                }
                cachedTextures.Remove(item.id);
            }

            if (item.gameObject != null)
            {
                texture = GetGameObjectIcon(item.gameObject);
                cachedTextures.Add(item.id, new CachedTexture
                {
                    texture = texture,
                    time = EditorApplication.timeSinceStartup
                });
            }
            else if (item.target == null) texture = unityLogoTexture;
            else return false;

            return true;
        }

        private static void Init()
        {
            inited = true;
            Object[] windows = UnityEngine.Resources.FindObjectsOfTypeAll(SceneHierarchyWindowRef.type);
            foreach (EditorWindow window in windows.Cast<EditorWindow>())
            {
                int wid = window.GetInstanceID();
                if (!hierarchyWindows.Contains(wid))
                {
                    InitWindow(window, wid);
                }
            }
        }

        private static void InitWindow(EditorWindow window, int wid)
        {
            try
            {
                IMGUIContainer container = window.rootVisualElement.parent.Query<IMGUIContainer>().First();
                container.onGUIHandler = (() => OnGUIBefore(wid)) + container.onGUIHandler;
                HierarchyHelper.SetDefaultIconsSize(window);
                hierarchyWindows.Add(wid);
            }
            catch
            {

            }
        }

        private static void OnGUIBefore(int wid)
        {
            if (!Prefs.hierarchyOverrideMainIcon) return;
            if (Event.current.type != EventType.Layout) return;

            EditorWindow w = EditorUtility.InstanceIDToObject(wid) as EditorWindow;
            if (w != null) HierarchyHelper.SetDefaultIconsSize(w);
        }

        internal class CachedTexture
        {
            public Texture texture;
            public double time;
        }
    }
}