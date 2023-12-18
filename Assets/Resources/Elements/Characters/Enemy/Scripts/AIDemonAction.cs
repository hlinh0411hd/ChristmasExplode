using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDemonAction : AIEnemyAction
{
    Enemy enemy;
    float timeUpdate = 1;
    public Vector2 desPos;

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
        target = PlayerController.instance.GetPlayer();
    }

    void UpdateState()
    {
        if (!target){
            UpdateTarget();
        }
        if (GameController.instance.IsBossLevel()){
            if (CheckCanAttack())
            {
                enemy.ChangeState(EnemyState.ATTACK, 1f);
            }
        } else {
            
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
