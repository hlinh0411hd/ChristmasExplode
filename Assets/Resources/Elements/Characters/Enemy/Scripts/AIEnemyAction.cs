using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AIPathFinding;

public class AIEnemyAction : AIAction
{
    Enemy enemy;
    float timeUpdate = 1;

    new void Awake()
    {
        base.Awake();
        enemy = GetComponent<Enemy>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    public void StartPlay()
    {
        ChooseTypeTarget(TypeFindTarget.PRIORITY);
        enemy.ChangeState(EnemyState.IDLE);
        timeUpdate = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemy.IsStart() || enemy.GetState() == EnemyState.DIE) return;
        UpdateState();
    }

    void UpdateTarget(){
        if (target == null) {
            ChooseTypeTarget(TypeFindTarget.PRIORITY);
            FindTarget();
            return;
        }
        if (Vector2.Distance(transform.position, target.transform.position) > enemy.data.rangeFollow){
            ChooseTypeTarget(TypeFindTarget.PRIORITY);
            FindTarget();
            return;
        }
    }

    void UpdateState()
    {
        Vector2Int crrPos = MapController.instance.currentMap.ConvertToTilePosition(transform.position);
        UpdateTarget();
        if (target == null)
        {
            enemy.ChangeState(EnemyState.IDLE);
            return;
        }

        if (targetPos != MapController.instance.currentMap.ConvertToTilePosition(target.transform.position)){
            targetPos = MapController.instance.currentMap.ConvertToTilePosition(target.transform.position);
            aIPathFinding.FindPathToTile(targetPos, new Vector2Int(1, 1));
        }

        int indexDir = aIPathFinding.mapPath[crrPos.x][crrPos.y];
        if (indexDir < 0 || indexDir >= GameConfig.DIR_X.Length)
        {
            if (!MapController.instance.currentMap.CheckEmpty(MapController.instance.currentMap.ConvertToTilePosition(transform.position), new Vector2Int(1, 1)))
            {
                indexDir = MapController.instance.currentMap.GetDirNearFreeTile(MapController.instance.currentMap.ConvertToTilePosition(transform.position));
                crrTargetPos = crrPos + new Vector2Int(GameConfig.DIR_X[indexDir], GameConfig.DIR_Y[indexDir]);
            } else
            {
                crrTargetPos = crrPos;
                ChooseTypeTarget(TypeFindTarget.NEAREST);
            }
        } else
        {
            crrTargetPos = crrPos + new Vector2Int(GameConfig.DIR_X[indexDir], GameConfig.DIR_Y[indexDir]);
        }
        if (CheckCanAttack())
        {
            enemy.ChangeState(EnemyState.ATTACK, enemy.data.timeAttackDuration);
        }
        if (crrPos == crrTargetPos){
            enemy.ChangeState(EnemyState.IDLE);
        } else {
            enemy.ChangeState(EnemyState.MOVE);
        }
    }

    bool CheckCanAttack()
    {
        //return false;
        if (!target) { return false; }
        if (GetEnemiesByTag().IndexOf(target.tag) < 0)
        {
            return false;
        }
        return false;
        //Vector2Int tileTarget = MapController.instance.ConvertToTilePosition(target.transform.position);
        //Vector2Int tileGameObject = MapController.instance.ConvertToTilePosition(transform.position);
        //float distance = Vector2.Distance(target.transform.position, transform.position);
        //return (GameUtil.GetRawDistance(tileTarget, tileGameObject) <= GetRangeAction(target.tag)|| distance <= GetRangeAction(target.tag)) && enemy.GetState() != EnemyState.ATTACK && enemy.data.timeCountDownAttack < 0;
    }


    protected override List<string> GetTargetsByTag()
    {
        List<string> list = new List<string>();
        list.Add(GameConfig.TAG_BASE);
        list.Add(GameConfig.TAG_PLAYER);
        list.Add(GameConfig.TAG_ALLY);
        list.Add(GameConfig.TAG_TOWER);
        return list;
    }

    protected override List<string> GetEnemiesByTag()
    {
        List<string> list = new List<string>();
        list.Add(GameConfig.TAG_BASE);
        list.Add(GameConfig.TAG_PLAYER);
        list.Add(GameConfig.TAG_ALLY);
        list.Add(GameConfig.TAG_TOWER);
        return list;
    }
    public override float GetRangeAction(string tag)
    {
        return enemy.data.rangeAttack;
    }

}
