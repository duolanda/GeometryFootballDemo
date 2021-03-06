﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;


public class StageManager : Singleton<StageManager>
{
    public List<Ball> balls = new List<Ball>(); //场地中的球
    public Dictionary<string, List<Player>> teams = new Dictionary<string, List<Player>>(); //场地中的各个队伍
    public Dictionary<string, Goal> teamGoals = new Dictionary<string, Goal>(); //各个队伍的球门

    [Tooltip("场地对角线长度")]
    public float maxStageLength = 20;
    float stageDiagonalFactor = 0;

    [HideInInspector]
    public float timer = 0; //计时器


    public void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
    }
    /// <summary>
    /// 将所有球初始化/重置
    /// </summary>
    public void InitBalls()
    {
        foreach (Ball b in balls)
        {
            b.InitBall();
        }
    }
    /// <summary>
    /// 将所有玩家初始化/重置
    /// </summary>
    public void InitPlayers()
    {
        foreach (List<Player> pal in teams.Values)
        {
            foreach (Player pa in pal)
            {
                pa.InitPlayer();
            }
        }
    }

    public Vector3 NormalizePos(Vector3 pos)
    {
        return pos * stageDiagonalFactor;
    }

    #region 以下代码来自 MLBall 的 Utils.cs
    //public static Dictionary<string, StageManager> stages = new Dictionary<string, StageManager>();

    //public static void SetStage(StageManager sm)
    //{
    //    string stageName = GetStageName(sm.transform);
    //    stages.Add(stageName, sm);
    //    Debug.Log($"{stageName}加入了stages");
    //}

    //public static StageManager GetStage(Transform t)
    //{
    //    return stages[GetStageName(t)];
    //}

    public static string GetStageName(Transform t)
    {
        string name = t.name;
        while (!name.StartsWith("Stage"))
        {
            t = t.parent;
            name = t.name;
        }
        return name;
    }

    public static bool EndWithTag(Collider collider, string tag)
    {
        return collider.tag.EndsWith(tag);
    }
    #endregion
}
