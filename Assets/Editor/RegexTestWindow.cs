using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEditor;

public class RegexTestWindow : EditorWindow {

    [MenuItem("Window/Regex Test")]
    static void Open()
    {
        EditorWindow.GetWindow<RegexTestWindow>();
    }

    public string regexRule = @"(\d)(\w)";
    public string testString = "123abc";
    Vector2 scrollPos = Vector2.zero;

    private void OnGUI()
    {
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.LabelField("Reguler Expression");
        var regexRule = EditorGUILayout.TextField(this.regexRule);
        EditorGUILayout.LabelField("Test String");
        var testString = EditorGUILayout.TextArea(this.testString);
        if (EditorGUI.EndChangeCheck()) {
            Undo.RecordObject(this, "Change RegexTestWindow text");
            this.regexRule = regexRule;
            this.testString = testString;
        }

        var writer = new StringBuilder();
        try {
            var regex = new Regex(regexRule);
            var matches = regex.Matches(testString);
            if (0 < matches.Count) {
                for (int i = 0; i < matches.Count; i++)
                {
                    writer.AppendLine(string.Format("Match {0}:",i));
                    var match = matches[i];
                    for (int groupNo = 0; groupNo < match.Groups.Count; groupNo++)
                    {
                        writer.AppendLine(string.Format("  Group {0}:{1}", groupNo, match.Groups[groupNo].Value));
                    }
                }
            }
            else {
                writer.AppendLine("No matches");
            }

        }
        catch(Exception exception) {
            writer.AppendLine(exception.Message);
        }
        finally {
            EditorGUILayout.LabelField("Result");
            EditorGUILayout.TextArea(writer.ToString());
            EditorGUILayout.EndScrollView();
        }
    }
}
