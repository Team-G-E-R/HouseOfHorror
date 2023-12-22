#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;
using static UnityEditor.GenericMenu;

namespace Inspect.Json
{
    public delegate void EmptyFunction();

    public static class JsonUtil
    {
        /* Variables */

        private const string NAME = "Inspect_Json";

        /* Setter & Getters */

        /* Functions */

        public static bool IsValidJson(string json)
        {
            json = json.Trim();
            if ((json.StartsWith("{") && json.EndsWith("}")) ||  // For object
                (json.StartsWith("[") && json.EndsWith("]")))  // For array
            {
                try
                {
                    JToken.Parse(json);
                    return true;
                }
                catch (JsonReaderException)  // Exception in parsing json
                {
                    return false;
                }
                catch (Exception ex)  // some other exception
                {
                    Debug.LogError(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static JToken Rename(JToken token, string newName)
        {
            var parent = token.Parent;
            if (parent == null)
                throw new InvalidOperationException("The parent is missing.");
            var newToken = new JProperty(newName, token);
            parent.Replace(newToken);
            return newToken;
        }

        public static void CreateGroup(EmptyFunction func, bool flexibleSpace = false)
        {
            BeginHorizontal(() =>
            {
                GUILayout.Space(5);

                BeginVertical(() =>
                {
                    Indent(func);
                });
            },
            flexibleSpace);
        }

        public static void BeginHorizontal(EmptyFunction func, bool flexibleSpace = false)
        {
            GUILayout.BeginHorizontal();
            if (flexibleSpace) GUILayout.FlexibleSpace();
            func.Invoke();
            GUILayout.EndHorizontal();
        }

        public static void BeginVertical(EmptyFunction func, string style = "box")
        {
            if (style == "")
                GUILayout.BeginVertical();
            else
                GUILayout.BeginVertical("box");
            func.Invoke();
            GUILayout.EndVertical();
        }

        public static void Indent(EmptyFunction func)
        {
            EditorGUI.indentLevel++;
            func.Invoke();
            EditorGUI.indentLevel--;
        }

        public static void CreateInfo(string desc)
        {
            if (desc == "")
                return;

            GUILayout.BeginHorizontal(EditorStyles.helpBox);
            {
                GUILayout.Space(5);
                GUILayout.Label(desc, EditorStyles.wordWrappedLabel);
                GUILayout.Space(5);
            }
            GUILayout.EndHorizontal();
        }

        #region Default UI

        public static bool Button(string text)
        {
            return GUILayout.Button(text, GUILayout.Width(60));
        }

        public static bool Button(string text, EmptyFunction func)
        {
            if (Button(text))
            {
                func.Invoke();
                return true;
            }
            return false;
        }

        public static void LabelField(string text)
        {
            EditorGUILayout.LabelField(text, EditorStyles.boldLabel);
        }

        public static void LabelField(string text, string tooltip)
        {
            EditorGUILayout.LabelField(new GUIContent(text, tooltip), EditorStyles.boldLabel);
        }

        #endregion

        #region Tooltip

        private static GUIContent CreateGUIContent(string name, string tooltip = "")
        {
            var gc = new GUIContent(name);
            if (tooltip != "") gc.tooltip = tooltip;
            return gc;
        }

        public static bool Toggle(string name, bool val, string tooltip = "", float width = -1.0f)
        {
            float originalValue = EditorGUIUtility.labelWidth;
            if (width != -1.0f)
                EditorGUIUtility.labelWidth = width;
            bool result = EditorGUILayout.Toggle(CreateGUIContent(name, tooltip), val);
            EditorGUIUtility.labelWidth = originalValue;
            return result;
        }

        public static float Slider(string name, float val, float leftValue, float rightValue, string tooltip = "")
        {
            return EditorGUILayout.Slider(CreateGUIContent(name, tooltip), val, leftValue, rightValue);
        }

        public static Enum EnumPopup(string name, Enum val, string tooltip = "")
        {
            return EditorGUILayout.EnumPopup(CreateGUIContent(name, tooltip), val);
        }

        public static string TextField(string name, string val, string tooltip = "")
        {
            return EditorGUILayout.TextField(CreateGUIContent(name, tooltip), val);
        }

        public static Color ColorField(string name, Color val, string tooltip = "")
        {
            return EditorGUILayout.ColorField(CreateGUIContent(name, tooltip), val);
        }

        public static int Popup(int selectedIndex, string[] displayedOptions)
        {
            return EditorGUILayout.Popup(selectedIndex, displayedOptions, GUILayout.Width(175));
        }

        #endregion

        public static Vector2 CalcSize(string text)
        {
            return GUI.skin.label.CalcSize(new GUIContent(text));
        }

        public static T WithoutSelectAll<T>(Func<T> guiCall)
        {
            bool preventSelection = (Event.current.type == EventType.MouseDown);

            Color oldCursorColor = GUI.skin.settings.cursorColor;

            if (preventSelection)
                GUI.skin.settings.cursorColor = new Color(0, 0, 0, 0);

            T value = guiCall();

            if (preventSelection)
                GUI.skin.settings.cursorColor = oldCursorColor;

            return value;
        }

        private static IEnumerable<JToken> JTokensByDirection(JToken token, bool up)
        {
            return (up) ? token.BeforeSelf() : token.AfterSelf();
        }

        public static void MoveJToken(JToken token, bool up)
        {
            IEnumerable<JToken> tokens = JTokensByDirection(token, up);

            int len = tokens.Count();
            if (len == 0)
                return;

            if (up)
            {
                JToken lastElement = tokens.ElementAt(len - 1);
                lastElement.Remove();

                token.AddAfterSelf(lastElement);
            }
            else
            {
                JToken firstElement = tokens.ElementAt(0);
                firstElement.Remove();

                token.AddBeforeSelf(firstElement);
            }
        }

        public static bool CanMoveJToken(JToken token, bool up)
        {
            IEnumerable<JToken> tokens = JTokensByDirection(token, up);

            int len = tokens.Count();
            if (len == 0) 
                return false;

            return true;
        }

        public static void AddItem(GenericMenu menu, string name, MenuFunction fnc, bool cond = true)
        {
            var content = new GUIContent(name);
            if (cond)
                menu.AddItem(content, false, fnc);
            else
                menu.AddDisabledItem(content, false);
        }
    }
}
#endif
