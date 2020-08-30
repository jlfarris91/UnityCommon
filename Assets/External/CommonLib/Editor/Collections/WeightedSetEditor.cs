using System.Collections.Generic;
using CommonLib;
using UnityEditor;
using UnityEngine;

namespace CommonLib.Editor.Collections
{
    using CommonLib;
    using CommonLib.Collections;
    using GUI = UnityEngine.GUI;

    public class WeightedSetEditor<TWeightedSet, TWeightedSetItem> : UnityEditor.Editor
        where TWeightedSet : WeightedSet<TWeightedSetItem>
        where TWeightedSetItem : Object
    {
        protected bool IsItemsExpanded = true;

        public override void OnInspectorGUI()
        {
            string weightedSetTypeName = typeof(TWeightedSet).FullName;
            string itemTypeName = typeof(TWeightedSetItem).FullName;

            var setContainer = this.target as IWeightedSetContainer<TWeightedSet, TWeightedSetItem>;

            ThrowIf.Null(
                setContainer,
                "WeightedSetEditor<{0}, {1}> target does not implement IWeightedSetContainer<{0}, {1}>.",
                weightedSetTypeName,
                itemTypeName);

            List<TWeightedSetItem> items = setContainer.WeightedSet.Items;
            List<float> weights = setContainer.WeightedSet.Weights;

            ThrowIf.True(items.Count != weights.Count, "Mismatched number of items and wieghts.");

            GUILayout.BeginVertical();

            this.IsItemsExpanded = EditorGUILayout.Foldout(this.IsItemsExpanded, new GUIContent("Items"));
            if (this.IsItemsExpanded)
            {
                EditorGUI.indentLevel++;

                GUILayout.BeginHorizontal();
                GUI.skin.label.normal.textColor = Color.grey;
                GUILayout.Space(92);
                GUILayout.Label(itemTypeName);
                GUILayout.Label("Weight", GUI.skin.label, GUILayout.Width(72));
                GUI.skin.label.normal.textColor = Color.black;
                GUILayout.EndHorizontal();

                for (var i = 0; i < setContainer.WeightedSet.Count; ++i)
                {
                    var rowStyle = new GUIStyle(GUI.skin.label);

                    string itemControlName = string.Format("WeightedLayoutSetItem{0}", i);
                    string weightControlName = string.Format("WeightedLayoutSetItemWeight{0}", i);

                    string currentFocusedControlName = GUI.GetNameOfFocusedControl();
                    if (currentFocusedControlName == itemControlName || currentFocusedControlName == weightControlName)
                    {
                        rowStyle.normal.background = Texture2D.whiteTexture;
                    }

                    GUILayout.BeginHorizontal(rowStyle);

                    GUILayout.Label(string.Format("Element {0}:", i), GUI.skin.label, GUILayout.Width(72));

                    EditorGUI.BeginChangeCheck();
                    GUI.SetNextControlName(itemControlName);
                    var newItem = (TWeightedSetItem)EditorGUILayout.ObjectField(items[i], typeof(TWeightedSetItem), false);
                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(this.target, string.Format("Change weighted set item {0}", i));
                        items[i] = newItem;
                    }

                    EditorGUI.BeginChangeCheck();
                    GUI.SetNextControlName(weightControlName);
                    weights[i] = EditorGUILayout.FloatField(weights[i], GUILayout.Width(60));
                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(this.target, string.Format("Change weighted set weight {0}", i));
                        items[i] = newItem;
                    }

                    if (GUILayout.Button("X", GUI.skin.button, GUILayout.Width(20)))
                    {
                        Undo.RecordObject(this.target, string.Format("Remove item from weighted set", i));
                        setContainer.WeightedSet.Remove(items[i]);
                    }

                    GUILayout.EndHorizontal();

                    if (GUIEx.WasClicked(GUILayoutUtility.GetLastRect()))
                    {
                        if (Event.current.button == 0)
                        {
                            GUI.FocusControl(itemControlName);
                            this.Repaint();
                        }

                        if (Event.current.button == 1)
                        {
                            // Now create the menu, add items and show it
                            GenericMenu menu = new GenericMenu();

                            menu.AddItem(
                                new GUIContent("Delete Element"),
                                false,
                                index =>
                                {
                                    setContainer.WeightedSet.Remove(items[(int)index]);
                                },
                                i);

                            menu.AddItem(
                                new GUIContent("Duplicate Element"),
                                false,
                                index =>
                                {
                                    setContainer.WeightedSet.Insert((int)index + 1, items[(int)index], weights[(int)index]);
                                },
                                i);

                            Event.current.Use();
                            menu.ShowAsContext();
                        }
                    }
                }

                GUIEx.PushTooltip("Add new item to the weighted set.");
                if (GUILayout.Button("+"))
                {
                    Undo.RecordObject(this.target, "Add new item to the weighted set");
                    setContainer.WeightedSet.Add(null, 1.0f);
                }
                GUIEx.PopTooltip();

                EditorGUI.indentLevel--;
            }

            GUILayout.EndVertical();
        }
    }
}