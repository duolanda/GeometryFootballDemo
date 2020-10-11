using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Tooltip("队伍的球门")]
    public Goal teamGoal;
    private int teamID = -1;
    protected string teamName = "煤";
    Color teamColor = Color.black;
    Color teamHatColor = Color.gray;
    MeshRenderer mr;
    MeshRenderer hatMR;
    public int TeamID { get => teamID; private set => teamID = value; }
    public string TeamName { get => teamName; private set => teamName = value; }


    Vector3 initPos = Vector3.zero;
    Quaternion iniRotation = Quaternion.identity;
    Rigidbody rd;

    public float force = 50f;
    float maxSpeed = 12f;
    public float rotateSpeed = 160;
    public float barSpeed = 0.5f;
    float step = 0;
    bool isBarRotate = false;

    List<Transform> barList = new List<Transform>();
    List<Vector3> barPositionList = new List<Vector3>();

    void Start()
    {
        #region 队伍信息
        TeamID = teamGoal.teamID;
        TeamName = GlobalManager.instance.GetTeamName(TeamID);
        teamColor = GlobalManager.instance.GetTeamColor(TeamID);
        teamHatColor = GlobalManager.instance.GetTeamColor(TeamID, true);
        mr = GetComponent<MeshRenderer>();
        hatMR = transform.GetChild(0).GetComponent<MeshRenderer>();
        mr.material.color = teamColor;
        hatMR.material.color = teamHatColor;
        AddTeam();
        #endregion

        rd = GetComponent<Rigidbody>();

        //读取孩子里的 Bar，如果以后给 Bar 写了脚本，能简化判断
        foreach (Transform child in transform.GetComponentInChildren<Transform>())
        {
            if (child.gameObject.layer == 8) //8 Bar
            {
                barList.Add(child);
                barPositionList.Add(child.localPosition);
            } 
        }
    }

    void Update()
    {
        //移动
        float h = Input.GetAxis("Horizontal"); 
        float accelerator = Input.GetAxis("Vertical"); 
        rd.AddForce(accelerator * transform.forward * force);
        if(rd.velocity.magnitude >= maxSpeed)
        {
            rd.velocity = rd.velocity.normalized * maxSpeed;
        }

        transform.Rotate(0, h * rotateSpeed * Time.deltaTime, 0, Space.Self);

        if (!isBarRotate && Input.GetKey(KeyCode.J))
        {
            isBarRotate = true;
        }

        //让 Bar 转
        if (isBarRotate)
        {
            for (int i = 0; i < barList.Count; i++)
            {
                step += barSpeed * Time.deltaTime;
                //barList[i].position = Vector3.MoveTowards(barList[i].position, barPositionList[i], step);
                barList[i].localPosition = Vector3.Lerp(barPositionList[i], barPositionList[(i + 1) % barList.Count], step);
            }
            if (step > 1)
            {
                step = 0;
                Vector3 barPos0 = barPositionList[0];
                for (int i = 0; i < barList.Count - 1; i++)
                {
                    barPositionList[i] = barPositionList[(i + 1) % (barList.Count)];
                }
                barPositionList[barList.Count - 1] = barPos0;
                isBarRotate = false;
            }

        }

    }

    public void InitPlayer()
    {
        rd.velocity = Vector3.zero;
        transform.localPosition = initPos;
        transform.localRotation = iniRotation;
    }

    public void AddTeam()
    {
        if (StageManager.Instance.teams.TryGetValue(TeamName, out List<Player> teamList))
        {
            teamList.Add(this);
        }
        else
        {
            teamList = new List<Player>();
            teamList.Add(this);
            StageManager.Instance.teams.Add(TeamName, teamList);
        }
    }

    //private void barMove()
}
