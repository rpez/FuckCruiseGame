using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TeamMembers : MonoBehaviour
{
    [Header("Drag in editor")]
    public GameObject content;
    public GameObject teamMemberFieldPrefab;

    public List<string> GetMembers()
    {
        List<string> teamMembers = new List<string>();

        for (int i = 0; i < content.transform.childCount; i++)
        {
            teamMembers.Add(content.transform.GetChild(i).gameObject.GetComponent<TMP_InputField>().text);
        }

        return teamMembers;
    }

    public void AddTeamMember()
    {
        GameObject prefab = GameObject.Instantiate(teamMemberFieldPrefab);
        prefab.transform.SetParent(content.transform);
    }
}
