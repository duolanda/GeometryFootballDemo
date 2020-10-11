using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [Tooltip("队伍id")]
    public int teamID;
    string teamName;

    [Tooltip("在进球时结束Episode")]
    public bool EndAtGoal = true;

    StageManager sm;
    MeshRenderer mr;

    private void Start()
    {
        sm = StageManager.Instance;
        sm.teamGoals.Add(GlobalManager.instance.GetTeamName(teamID), this);
        mr = GetComponent<MeshRenderer>();
        Debug.Log(mr);
        ChangeTeam();
    }

    public void ChangeTeam()
    {
        mr.material.color = GlobalManager.instance.GetTeamColor(teamID);
        teamName = GlobalManager.instance.GetTeamName(teamID);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Debug.Log("进球啦！");
        }
 
    }
}
