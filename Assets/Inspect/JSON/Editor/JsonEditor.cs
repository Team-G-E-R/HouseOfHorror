#if UNITY_EDITOR
/**
 * Copyright (c) Pixisoft Corporations. All rights reserved.
 * 
 * Licensed under the Source EULA. See COPYING in the asset root for license informtaion.
 */
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEditor;

namespace Inspect.Json
{
    /// <summary>
    /// Adds a nice editor to edit JSON files as well as a simple text editor incase
    /// the editor doesn't support the types you need. It works with strings, floats
    /// ints and bools at the moment.
    /// 
    /// * Requires the latest version of JSON.net compatible with Unity
    /// 
    /// 
    /// If you want to edit a JSON file in the "StreammingAssets" Folder change this to DefaultAsset.
    /// Hacky solution to a weird problem :/
    /// </summary>
    [CustomEditor(typeof(TextAsset), true)]
    public class JSONEditor : Editor
    {
        /* Variables */

        private string Path => AssetDatabase.GetAssetPath(target);
        private bool isCompatible => Path.EndsWith(".json");
        private bool unableToParse => !JsonUtil.IsValidJson(rawText);

        private bool isTextMode;

        private string rawText;
        private JObject jsonObject;
        private JProperty propertyToRename;
        private string propertyRename;

        private Dictionary<JProperty, bool> foldouts = new Dictionary<JProperty, bool>();
        private const bool DEFAULT_FOLD = false;

        /* Setter & Getters */

        /* Functions */

        private void OnEnable()
        {
            if (isCompatible)
                LoadFromJson();
        }

        private void OnDisable()
        {
            if (isCompatible)
                WriteToJson();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (isCompatible)
                JsonInspectorGUI();
        }

        private void JsonInspectorGUI()
        {
            GUI.enabled = true;

            const string info = "You edit raw text if the JSON editor isn't enough by clicking the button to the right";

            Rect subHeaderRect = EditorGUILayout.GetControlRect(false, EditorGUIUtility.singleLineHeight * 1.8f);
            Rect helpBoxRect = new Rect(subHeaderRect.x, subHeaderRect.y, subHeaderRect.width - subHeaderRect.width / 6 - 5f, subHeaderRect.height);
            Rect rawTextModeButtonRect = new Rect(subHeaderRect.x + subHeaderRect.width / 6 * 5, subHeaderRect.y, subHeaderRect.width / 6, subHeaderRect.height);
            EditorGUI.HelpBox(helpBoxRect, info, MessageType.Info);

            GUIStyle wrappedButton = new GUIStyle("Button");
            wrappedButton.wordWrap = true;
            EditorGUI.BeginChangeCheck();
            isTextMode = GUI.Toggle(rawTextModeButtonRect, isTextMode || unableToParse, "Edit Text", wrappedButton);

            if (EditorGUI.EndChangeCheck() && !unableToParse)
            {
                WriteToJson(!isTextMode);
                GUI.FocusControl("");
                LoadFromJson();
            }

            if (isTextMode || unableToParse)
            {
                OnTextMode();
            }
            else
            {
                OnGUIMode();
            }
        }

        private void OnGUIMode()
        {
            if (jsonObject == null)
                return;

            GUILayout.Space(5);

            JsonUtil.BeginVertical(() =>
            {
                RecursiveDrawField(jsonObject);
            });

            GUILayout.Space(5);

            JsonUtil.BeginHorizontal(() =>
            {
                const float border = 20.0f;
                GUILayout.Space(border / 2);

                if (GUILayout.Button("Add New Property", GUILayout.Width(EditorGUIUtility.currentViewWidth - border)))
                    DrawNewProperty();
            });
        }

        private void OnTextMode()
        {
            rawText = JsonUtil.WithoutSelectAll(() => EditorGUILayout.TextArea(rawText));

            GUIStyle helpBoxRichText = new GUIStyle(EditorStyles.helpBox);
            Texture errorIcon = EditorGUIUtility.Load("icons/console.erroricon.png") as Texture2D;

            helpBoxRichText.richText = true;

            if (unableToParse)
            {
                var content = new GUIContent("Unable to parse text into JSON. Make sure there are no mistakes! Are you missing a <b>{</b>, <b>{</b> or <b>,</b>?", errorIcon);
                GUILayout.Label(content, helpBoxRichText);
            }
            else
            {
                WriteToJson(true);
            }
        }

        private void RecursiveDrawField(JToken container, bool renaming = false, int level = 0)
        {
            const int indentSpace = 15;
            JToken[] tokens = container.Values<JToken>().ToArray();

            for (int index = 0; index < container.Count(); ++index)
            {
                JToken token = tokens[index];

                switch (token.Type)
                {
                    case JTokenType.Property:
                        {
                            JsonUtil.BeginHorizontal(() =>
                            {
                                GUILayout.Space(indentSpace * level);

                                JProperty property = token.Value<JProperty>();

                                if (propertyToRename != property)
                                {
                                    string txt = "Σ";
                                    const float border = 4;
                                    float width = JsonUtil.CalcSize(txt).x + border;
                                    if (GUILayout.Button(txt, GUILayout.Width(width)))
                                        DrawPropertyMenu(property, token);
                                }
                                else
                                {
                                    DrawRenameField(property);

                                    renaming = true;
                                }
                                RecursiveDrawField(token, renaming, level);
                            });
                        }
                        break;
                    case JTokenType.Object:
                        {
                            ++level;

                            JProperty parentProperty = token.Parent.Value<JProperty>();
                            string name = (renaming) ? "" : parentProperty.Name;

                            if (!foldouts.ContainsKey(parentProperty))
                                foldouts.Add(parentProperty, DEFAULT_FOLD);

                            GUILayout.Space(13);  // shift foldout symbol

                            foldouts[parentProperty] = EditorGUILayout.Foldout(foldouts[parentProperty], name);

                            GUILayout.EndHorizontal();

                            if (foldouts[parentProperty])
                            {
                                GUILayout.BeginVertical();
                                GUILayout.Space(5);

                                var children = token.Children();
                                if (children.Count() == 0)
                                {
                                    JsonUtil.BeginHorizontal(() =>
                                    {
                                        GUILayout.Space(indentSpace * level);
                                        GUILayout.Label("-- No properties --");
                                    });
                                }
                                else
                                {
                                    renaming = false;
                                    RecursiveDrawField(token, renaming, level);
                                }
                                GUILayout.EndVertical();
                            }

                            GUILayout.Space(3);  // add spaces so it aligns

                            GUILayout.BeginHorizontal();  // close for next horizontal section

                            --level;
                        }
                        break;
                    default:
                        {
                            JProperty parentProperty = token.Parent.Value<JProperty>();
                            DrawInputField(parentProperty, token, renaming);
                        }
                        break;
                }

                renaming = false;
            }
        }

        private void LoadFromJson()
        {
            if (string.IsNullOrWhiteSpace(Path) || !File.Exists(Path))
                return;

            if (jsonObject != null && rawText == jsonObject.ToString())
                return;

            rawText = File.ReadAllText(Path);
            jsonObject = JsonConvert.DeserializeObject<JObject>(rawText);
        }

        private void WriteToJson(bool useRaw = false)
        {
            if (!File.Exists(Path))
                return;

            if (jsonObject != null)
            {
                if (!useRaw)
                    rawText = jsonObject.ToString();
            }

            File.WriteAllText(Path, rawText);
        }

        private JProperty NewProperty<T>(JObject jObject)
        {
            string typeName = typeof(T).Name.ToLower();
            object value = default(T);

            switch (Type.GetTypeCode(typeof(T)))
            {
                case TypeCode.Boolean:
                    break;
                case TypeCode.Int32:
                    typeName = "integer";
                    break;
                case TypeCode.Single:
                    break;
                case TypeCode.String:
                    value = "";
                    break;
                default:
                    {
                        if (typeof(T) == typeof(JObject))
                            typeName = "empty object";
                        value = new JObject();
                    }
                    break;
            }

            string name = GetUniqueName(jObject, string.Format("new {0}", typeName));
            return new JProperty(name, value);
        }

        private void AddNewProperty<T>(JObject jObject)
        {
            JProperty property = NewProperty<T>(jObject);
            jObject.Add(property);
        }

        private void DuplicateProperty(JObject parent, JProperty current)
        {
            string name = GetUniqueName(parent, string.Format("{0} (Duplicate)", current.Name));
            parent.Add(new JProperty(name, current.Value));
        }

        private string GetUniqueName(JObject jObject, string orignalName)
        {
            string uniqueName = orignalName;
            int suffix = 0;
            while (jObject[uniqueName] != null && suffix < 100)
            {
                ++suffix;
                if (suffix >= 100)
                {
                    Debug.LogError("Stop calling all your fields the same thing! Isn't it confusing?");
                }
                uniqueName = string.Format("{0} {1}", orignalName, suffix.ToString());
            }
            return uniqueName;
        }

        [MenuItem("Assets/Create/JSON File", priority = 81)]
        public static void CreateNewJsonFile()
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (path == "")
                path = "Assets";
            else if (System.IO.Path.GetExtension(path) != "")
                path = path.Replace(System.IO.Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");

            path = System.IO.Path.Combine(path, "New JSON File.json");

            JObject jObject = new JObject();
            File.WriteAllText(path, jObject.ToString());
            AssetDatabase.Refresh();
        }

        private void DrawNewProperty()
        {
            GenericMenu menu = new GenericMenu();
            JsonUtil.AddItem(menu, "Empty Object", () => { AddNewProperty<JObject>(jsonObject); });
            menu.AddSeparator("");
            JsonUtil.AddItem(menu, "String", () => { AddNewProperty<string>(jsonObject); });
            JsonUtil.AddItem(menu, "Single", () => { AddNewProperty<float>(jsonObject); });
            JsonUtil.AddItem(menu, "Integer", () => { AddNewProperty<int>(jsonObject); });
            JsonUtil.AddItem(menu, "Boolean", () => { AddNewProperty<bool>(jsonObject); });
            menu.ShowAsContext();
        }

        private void DrawRenameField(JProperty property)
        {
            var style = new GUIStyle(GUI.skin.button);
            style.normal.textColor = new Color(0, 1, 0);

            if (GUILayout.Button("✔", style, GUILayout.Width(24)))
            {
                var token = property.Value;

                JToken newToken = JsonUtil.Rename(token, propertyRename);
                GUI.FocusControl("");

                if (token != null && foldouts.ContainsKey(property))
                {
                    foldouts.Add(newToken.Value<JProperty>(), foldouts[property]);
                }
            }

            GUI.SetNextControlName("RENAME_PROPERTY");
            float space = JsonUtil.CalcSize("        ").x;
            float width = JsonUtil.CalcSize(propertyRename).x + space;
            propertyRename = EditorGUILayout.TextField(propertyRename, GUILayout.Width(width));
        }

        private void DrawPropertyMenu(JProperty property, JToken token)
        {
            GenericMenu menu = new GenericMenu();
            if (property.Value.Type == JTokenType.Object)
            {
                JObject jObject = property.Value.Value<JObject>();
                JsonUtil.AddItem(menu, "Add/Empty Object", () => { AddNewProperty<JObject>(jObject); });
                menu.AddSeparator("Add/");
                JsonUtil.AddItem(menu, "Add/String", () => { AddNewProperty<string>(jObject); });
                JsonUtil.AddItem(menu, "Add/Single", () => { AddNewProperty<float>(jObject); });
                JsonUtil.AddItem(menu, "Add/Integer", () => { AddNewProperty<int>(jObject); });
                JsonUtil.AddItem(menu, "Add/Boolean", () => { AddNewProperty<bool>(jObject); });
            }

            JsonUtil.AddItem(menu, "Duplicate", () => 
            {
                var parent = property.Parent.Value<JObject>();
                DuplicateProperty(parent, property);
            });
            JsonUtil.AddItem(menu, "Rename", () =>
            {
                propertyToRename = property;
                propertyRename = propertyToRename.Name;
            });

            menu.AddSeparator("");

            JsonUtil.AddItem(menu, "Move Up", () =>
            {
                JsonUtil.MoveJToken(token, true);
            }, JsonUtil.CanMoveJToken(token, true));

            JsonUtil.AddItem(menu, "Move Down", () =>
            {
                JsonUtil.MoveJToken(token, false);
            }, JsonUtil.CanMoveJToken(token, false));

            menu.AddSeparator("");

            JsonUtil.AddItem(menu, "Remove", () => { token.Remove(); });
            
            menu.ShowAsContext();
        }

        private void DrawInputField(JProperty parentProperty, JToken token, bool renaming)
        {
            string name = (renaming) ? "" : parentProperty.Name;
            float width = JsonUtil.CalcSize(name).x + 3;

            GUILayout.Label(name, GUILayout.Width(width));

            switch (token.Type)
            {
                case JTokenType.String:
                    string stringValue = token.Value<string>();
                    stringValue = GUILayout.TextField(stringValue);
                    parentProperty.Value = stringValue;
                    break;
                case JTokenType.Float:
                    float floatValue = token.Value<float>();
                    floatValue = EditorGUILayout.FloatField(floatValue);
                    parentProperty.Value = floatValue;
                    break;
                case JTokenType.Integer:
                    int intValue = token.Value<int>();
                    intValue = EditorGUILayout.IntField(intValue);
                    parentProperty.Value = intValue;
                    break;
                case JTokenType.Boolean:
                    bool boolValue = token.Value<bool>();
                    boolValue = EditorGUILayout.Toggle(boolValue);
                    parentProperty.Value = boolValue;
                    break;
                case JTokenType.Null:
                    GUILayout.Label("Null", EditorStyles.helpBox);
                    break;
                default:
                    string info = string.Format("Type '{0}' is not supported. Use text editor instead", token.Type.ToString());
                    GUILayout.Label(info, EditorStyles.helpBox);
                    break;
            }
        }
    }
}
#endif
