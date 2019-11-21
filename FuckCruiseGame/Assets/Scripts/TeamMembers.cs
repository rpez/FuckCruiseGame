using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamMembers : MonoBehaviour
{
    GameObject teamMemberFieldPrefab;
    List<GameObject> teamMemberFields;

    [Header("Drag in editor")]
    public GameObject content;

    public List<string> GetMembers()
    {
        List<string> teamMembers = new List<string>();

        for (int i = 0; i < content.transform.childCount; i++)
        {
            teamMembers.Add(content.transform.GetChild(i).GetComponent<Text>().text);
        }

        return teamMembers;
    }

    public void AddTeamMember()
    {
        GameObject prefab = GameObject.Instantiate(teamMemberFieldPrefab);
        prefab.transform.parent = content.transform;
    }
}
