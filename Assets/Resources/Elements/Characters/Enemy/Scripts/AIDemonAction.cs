using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDemonAction : AIEnemyAction
{
    Demon demon;
    public Vector2 desPos;

    new void Awake()
    {
        base.Awake();
        demon = GetComponent<Demon>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    public override void StartPlay()
    {
        ChooseTypeTarget(TypeFindTarget.PRIORITY);
        demon.ChangeState(DemonState.MAKE_LEVEL);
        timeUpdate = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!demon.IsStart() || demon.GetState() == DemonState.DIE) return;
        timeUpdate -= Time.deltaTime;
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
                demon.ChangeState(DemonState.ATTACK, 1f);
            }
        } else {
            if (timeUpdate <= 0)
            {
                timeUpdate = Random.Range(1f, 3f);
                int randState = Random.Range(0f, 1f) < 0.5f ? DemonState.ATTACK : DemonState.SPAWN;
                demon.ForceState(randState, 0.5f);
            } else
            {
                demon.ChangeState(DemonState.IDLE);
            }
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
        //return (GameUtil.GetRawDistance(tileTarget, tileGameObject) <= GetRangeAction(target.tag)|| distance <= GetRangeAction(target.tag)) && demon.GetState() != DemonState.ATTACK && demon.data.timeCountDownAttack < 0;
    }
    public override float GetRangeAction(string tag)
    {
        return 1f;
    }

}
