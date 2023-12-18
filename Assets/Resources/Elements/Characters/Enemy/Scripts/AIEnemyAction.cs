using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemyAction : AIAction
{
    Enemy enemy;
    protected float timeUpdate = 1;

    new void Awake()
    {
        base.Awake();
        enemy = GetComponent<Enemy>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    public virtual void StartPlay()
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
        if (Vector2.Distance(transform.position, target.transform.position) > 1f){
            ChooseTypeTarget(TypeFindTarget.PRIORITY);
            FindTarget();
            return;
        }
    }

    void UpdateState()
    {
        if (CheckCanAttack())
        {
            enemy.ChangeState(EnemyState.ATTACK, 1f);
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
    public override float GetRangeAction(string tag)
    {
        return 1f;
    }

}
