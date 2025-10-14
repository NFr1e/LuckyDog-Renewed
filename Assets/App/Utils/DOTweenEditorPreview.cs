#if UNITY_EDITOR
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace DG.DOTweenEditor
{
    internal static class UnityEditorVersion
    {
        //
        // 摘要:
        //     Full major version + first minor version (ex: 2018.1f)
        public static readonly float Version;

        //
        // 摘要:
        //     Major version
        public static readonly int MajorVersion;

        //
        // 摘要:
        //     First minor version (ex: in 2018.1 it would be 1)
        public static readonly int MinorVersion;

        static UnityEditorVersion()
        {
            string unityVersion = Application.unityVersion;
            int num = unityVersion.IndexOf('.');
            if (num == -1)
            {
                MajorVersion = int.Parse(unityVersion);
                Version = MajorVersion;
                return;
            }

            string text = unityVersion.Substring(0, num);
            MajorVersion = int.Parse(text);
            string text2 = unityVersion.Substring(num + 1);
            num = text2.IndexOf('.');
            if (num != -1)
            {
                text2 = text2.Substring(0, num);
            }

            MinorVersion = int.Parse(text2);
            if (!float.TryParse(text + "." + text2, NumberStyles.Float, CultureInfo.InvariantCulture, out Version))
            {
                Debug.LogWarning($"UnityEditorVersion ► Error when detecting Unity Version from \"{text}.{text2}\"");
                Version = 2018.3f;
            }
        }
    }
    //
    // 摘要:
    //     Contains compatibility methods taken from DemiEditor (for when DOTween is without
    //     it)
    internal static class EditorCompatibilityUtils
    {
        private static MethodInfo _miFindObjectOfTypeGeneric;

        private static MethodInfo _miFindObjectOfType;

        private static MethodInfo _miFindObjectsOfTypeGeneric;

        private static MethodInfo _miFindObjectsOfType;

        private static bool _findObjectOfType_hasIncludeInactiveParam;

        private static bool _findObjectOfTypeGeneric_hasIncludeInactiveParam;

        private static bool _findObjectsOfType_hasIncludeInactiveParam;

        private static bool _findObjectsOfTypeGeneric_hasIncludeInactiveParam;

        private static Type _findObjectsInactiveType;

        private static Type _findObjectsSortModeType;

        //
        // 摘要:
        //     Warning: some versions of this method don't have the includeInactive parameter
        //     so it won't be taken into account
        public static T FindObjectOfType<T>(bool includeInactive = false)
        {
            if ((object)_miFindObjectOfTypeGeneric == null)
            {
                if (UnityEditorVersion.MajorVersion < 2023)
                {
                    _miFindObjectOfTypeGeneric = typeof(UnityEngine.Object).GetMethod("FindObjectOfType", BindingFlags.Public | BindingFlags.Static, Type.DefaultBinder, new Type[1] { typeof(bool) }, null);
                    _findObjectOfTypeGeneric_hasIncludeInactiveParam = true;
                    if ((object)_miFindObjectOfTypeGeneric == null)
                    {
                        MethodInfo[] methods = typeof(UnityEngine.Object).GetMethods(BindingFlags.Public | BindingFlags.Static);
                        foreach (MethodInfo methodInfo in methods)
                        {
                            if (!(methodInfo.Name != "FindObjectOfType") && methodInfo.IsGenericMethod)
                            {
                                _miFindObjectOfTypeGeneric = methodInfo;
                                break;
                            }
                        }

                        _findObjectOfTypeGeneric_hasIncludeInactiveParam = false;
                    }

                    _miFindObjectOfTypeGeneric = _miFindObjectOfTypeGeneric.MakeGenericMethod(typeof(T));
                }
                else
                {
                    if ((object)_findObjectsInactiveType == null)
                    {
                        _findObjectsInactiveType = typeof(GameObject).Assembly.GetType("UnityEngine.FindObjectsInactive");
                    }

                    _miFindObjectOfTypeGeneric = typeof(UnityEngine.Object).GetMethod("FindAnyObjectByType", BindingFlags.Public | BindingFlags.Static, Type.DefaultBinder, new Type[1] { _findObjectsInactiveType }, null).MakeGenericMethod(typeof(T));
                }
            }

            if (UnityEditorVersion.MajorVersion < 2023)
            {
                if (_findObjectOfTypeGeneric_hasIncludeInactiveParam)
                {
                    return (T)_miFindObjectOfTypeGeneric.Invoke(null, new object[1] { includeInactive });
                }

                return (T)_miFindObjectOfTypeGeneric.Invoke(null, null);
            }

            return (T)_miFindObjectOfTypeGeneric.Invoke(null, new object[1] { (!includeInactive) ? 1 : 0 });
        }

        //
        // 摘要:
        //     Warning: some versions of this method don't have the includeInactive parameter
        //     so it won't be taken into account
        public static UnityEngine.Object FindObjectOfType(Type type, bool includeInactive = false)
        {
            if ((object)_miFindObjectOfType == null)
            {
                if (UnityEditorVersion.MajorVersion < 2023)
                {
                    _miFindObjectOfType = typeof(UnityEngine.Object).GetMethod("FindObjectOfType", BindingFlags.Public | BindingFlags.Static, Type.DefaultBinder, new Type[2]
                    {
                    typeof(Type),
                    typeof(bool)
                    }, null);
                    _findObjectOfType_hasIncludeInactiveParam = true;
                    if ((object)_miFindObjectOfType == null)
                    {
                        _miFindObjectOfType = typeof(UnityEngine.Object).GetMethod("FindObjectOfType", BindingFlags.Public | BindingFlags.Static, Type.DefaultBinder, new Type[1] { typeof(Type) }, null);
                        _findObjectOfType_hasIncludeInactiveParam = false;
                    }
                }
                else
                {
                    if ((object)_findObjectsInactiveType == null)
                    {
                        _findObjectsInactiveType = typeof(GameObject).Assembly.GetType("UnityEngine.FindObjectsInactive");
                    }

                    _miFindObjectOfType = typeof(UnityEngine.Object).GetMethod("FindAnyObjectByType", BindingFlags.Public | BindingFlags.Static, Type.DefaultBinder, new Type[2]
                    {
                    typeof(Type),
                    _findObjectsInactiveType
                    }, null);
                }
            }

            if (UnityEditorVersion.MajorVersion < 2023)
            {
                if (_findObjectOfType_hasIncludeInactiveParam)
                {
                    return (UnityEngine.Object)_miFindObjectOfType.Invoke(null, new object[2] { type, includeInactive });
                }

                return (UnityEngine.Object)_miFindObjectOfType.Invoke(null, new object[1] { type });
            }

            return (UnityEngine.Object)_miFindObjectOfType.Invoke(null, new object[2]
            {
            type,
            (!includeInactive) ? 1 : 0
            });
        }

        //
        // 摘要:
        //     Warning: some versions of this method don't have the includeInactive parameter
        //     so it won't be taken into account
        public static T[] FindObjectsOfType<T>(bool includeInactive = false)
        {
            if ((object)_miFindObjectsOfTypeGeneric == null)
            {
                if (UnityEditorVersion.MajorVersion < 2023)
                {
                    _miFindObjectsOfTypeGeneric = typeof(UnityEngine.Object).GetMethod("FindObjectsOfType", BindingFlags.Public | BindingFlags.Static, Type.DefaultBinder, new Type[1] { typeof(bool) }, null);
                    _findObjectsOfTypeGeneric_hasIncludeInactiveParam = true;
                    if ((object)_miFindObjectsOfTypeGeneric == null)
                    {
                        MethodInfo[] methods = typeof(UnityEngine.Object).GetMethods(BindingFlags.Public | BindingFlags.Static);
                        foreach (MethodInfo methodInfo in methods)
                        {
                            if (!(methodInfo.Name != "FindObjectsOfType") && methodInfo.IsGenericMethod)
                            {
                                _miFindObjectsOfTypeGeneric = methodInfo;
                                break;
                            }
                        }

                        _findObjectsOfTypeGeneric_hasIncludeInactiveParam = false;
                    }

                    _miFindObjectsOfTypeGeneric = _miFindObjectsOfTypeGeneric.MakeGenericMethod(typeof(T));
                }
                else
                {
                    if ((object)_findObjectsInactiveType == null)
                    {
                        _findObjectsInactiveType = typeof(GameObject).Assembly.GetType("UnityEngine.FindObjectsInactive");
                    }

                    if ((object)_findObjectsSortModeType == null)
                    {
                        _findObjectsSortModeType = typeof(GameObject).Assembly.GetType("UnityEngine.FindObjectsSortMode");
                    }

                    _miFindObjectsOfTypeGeneric = typeof(UnityEngine.Object).GetMethod("FindObjectsByType", BindingFlags.Public | BindingFlags.Static, Type.DefaultBinder, new Type[2] { _findObjectsInactiveType, _findObjectsSortModeType }, null).MakeGenericMethod(typeof(T));
                }
            }

            if (UnityEditorVersion.MajorVersion < 2023)
            {
                if (_findObjectsOfTypeGeneric_hasIncludeInactiveParam)
                {
                    return (T[])_miFindObjectsOfTypeGeneric.Invoke(null, new object[1] { includeInactive });
                }

                return (T[])_miFindObjectsOfTypeGeneric.Invoke(null, null);
            }

            return (T[])_miFindObjectsOfTypeGeneric.Invoke(null, new object[2]
            {
            (!includeInactive) ? 1 : 0,
            0
            });
        }

        //
        // 摘要:
        //     Warning: some versions of this method don't have the includeInactive parameter
        //     so it won't be taken into account
        public static UnityEngine.Object[] FindObjectsOfType(Type type, bool includeInactive = false)
        {
            if ((object)_miFindObjectsOfType == null)
            {
                if (UnityEditorVersion.MajorVersion < 2023)
                {
                    _miFindObjectsOfType = typeof(UnityEngine.Object).GetMethod("FindObjectsOfType", BindingFlags.Public | BindingFlags.Static, Type.DefaultBinder, new Type[2]
                    {
                    typeof(Type),
                    typeof(bool)
                    }, null);
                    _findObjectsOfType_hasIncludeInactiveParam = true;
                    if ((object)_miFindObjectOfType == null)
                    {
                        _miFindObjectsOfType = typeof(UnityEngine.Object).GetMethod("FindObjectsOfType", BindingFlags.Public | BindingFlags.Static, Type.DefaultBinder, new Type[1] { typeof(Type) }, null);
                        _findObjectsOfType_hasIncludeInactiveParam = false;
                    }
                }
                else
                {
                    if ((object)_findObjectsInactiveType == null)
                    {
                        _findObjectsInactiveType = typeof(GameObject).Assembly.GetType("UnityEngine.FindObjectsInactive");
                    }

                    if ((object)_findObjectsSortModeType == null)
                    {
                        _findObjectsSortModeType = typeof(GameObject).Assembly.GetType("UnityEngine.FindObjectsSortMode");
                    }

                    _miFindObjectsOfType = typeof(UnityEngine.Object).GetMethod("FindObjectsByType", BindingFlags.Public | BindingFlags.Static, Type.DefaultBinder, new Type[3]
                    {
                    typeof(Type),
                    _findObjectsInactiveType,
                    _findObjectsSortModeType
                    }, null);
                }
            }

            if (UnityEditorVersion.MajorVersion < 2023)
            {
                if (_findObjectsOfType_hasIncludeInactiveParam)
                {
                    return (UnityEngine.Object[])_miFindObjectsOfType.Invoke(null, new object[2] { type, includeInactive });
                }

                return (UnityEngine.Object[])_miFindObjectsOfType.Invoke(null, new object[1] { type });
            }

            return (UnityEngine.Object[])_miFindObjectsOfType.Invoke(null, new object[3]
            {
            type,
            (!includeInactive) ? 1 : 0,
            0
            });
        }
    }
    public static class DOTweenEditorPreview
    {
        private static double _previewTime;

        private static Action _onPreviewUpdated;

        private static UnityEngine.Object[] _uiGraphics;

        private static readonly Type _TGraphic;

        private static readonly List<Tween> _Tweens;

        public static bool isPreviewing { get; private set; }

        static DOTweenEditorPreview()
        {
            _Tweens = new List<Tween>();
            Assembly assembly2 = AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault((Assembly assembly) => assembly.GetName().Name == "UnityEngine.UI");
            if ((object)assembly2 != null)
            {
                _TGraphic = assembly2.GetType("UnityEngine.UI.Graphic");
            }
        }

        //
        // 摘要:
        //     Starts the update loop of tween in the editor. Has no effect during playMode.
        //
        //
        // 参数:
        //   onPreviewUpdated:
        //     Eventual callback to call after every update
        public static void Start(Action onPreviewUpdated = null)
        {
            if (!isPreviewing && !EditorApplication.isPlayingOrWillChangePlaymode)
            {
                isPreviewing = true;
                _onPreviewUpdated = onPreviewUpdated;
                _previewTime = EditorApplication.timeSinceStartup;
                if ((object)_TGraphic != null)
                {
                    _uiGraphics = EditorCompatibilityUtils.FindObjectsOfType(_TGraphic);
                }
                else
                {
                    _uiGraphics = null;
                }

                EditorApplication.update = (EditorApplication.CallbackFunction)Delegate.Combine(EditorApplication.update, new EditorApplication.CallbackFunction(PreviewUpdate));
            }
        }

        //
        // 摘要:
        //     Stops the update loop and clears the onPreviewUpdated callback.
        //
        // 参数:
        //   resetTweenTargets:
        //     If TRUE also resets the tweened objects to their original state. Note that this
        //     works by calling Rewind on all tweens, so it will work correctly only if you
        //     have a single tween type per object and it wasn't killed
        //
        //   clearTweens:
        //     If TRUE also kills any cached tween
        public static void Stop(bool resetTweenTargets = false, bool clearTweens = true)
        {
            isPreviewing = false;
            _uiGraphics = null;
            EditorApplication.update = (EditorApplication.CallbackFunction)Delegate.Remove(EditorApplication.update, new EditorApplication.CallbackFunction(PreviewUpdate));
            _onPreviewUpdated = null;
            if (resetTweenTargets)
            {
                foreach (Tween tween in _Tweens)
                {
                    try
                    {
                        if (tween.isBackwards)
                        {
                            tween.Complete();
                        }
                        else
                        {
                            tween.Rewind();
                        }
                    }
                    catch
                    {
                    }
                }
            }

            if (clearTweens)
            {
                _Tweens.Clear();
            }
            else
            {
                ValidateTweens();
            }
        }

        //
        // 摘要:
        //     Readies the tween for editor preview by setting its UpdateType to Manual plus
        //     eventual extra settings.
        //
        // 参数:
        //   t:
        //     The tween to ready
        //
        //   clearCallbacks:
        //     If TRUE (recommended) removes all callbacks (OnComplete/Rewind/etc)
        //
        //   preventAutoKill:
        //     If TRUE prevents the tween from being auto-killed at completion
        //
        //   andPlay:
        //     If TRUE starts playing the tween immediately
        public static void PrepareTweenForPreview(Tween t, bool clearCallbacks = true, bool preventAutoKill = true, bool andPlay = true)
        {
            _Tweens.Add(t);
            t.SetUpdate(UpdateType.Manual);
            if (preventAutoKill)
            {
                t.SetAutoKill(autoKillOnCompletion: false);
            }

            if (clearCallbacks)
            {
                t.OnComplete(null).OnStart(null).OnPlay(null)
                    .OnPause(null)
                    .OnUpdate(null)
                    .OnWaypointChange(null)
                    .OnStepComplete(null)
                    .OnRewind(null)
                    .OnKill(null);
            }

            if (andPlay)
            {
                t.Play();
            }
        }

        private static void PreviewUpdate()
        {
            double previewTime = _previewTime;
            _previewTime = EditorApplication.timeSinceStartup;
            float num = (float)(_previewTime - previewTime);
            DOTween.ManualUpdate(num, num);
            if (_uiGraphics != null)
            {
                UnityEngine.Object[] uiGraphics = _uiGraphics;
                for (int i = 0; i < uiGraphics.Length; i++)
                {
                    //EditorUtility.SetDirty(uiGraphics[i]);
                }
            }

            if (_onPreviewUpdated != null)
            {
                _onPreviewUpdated();
            }
        }

        private static void ValidateTweens()
        {
            for (int num = _Tweens.Count - 1; num > -1; num--)
            {
                if (_Tweens[num] == null || !_Tweens[num].active)
                {
                    _Tweens.RemoveAt(num);
                }
            }
        }
    }
}
#endif