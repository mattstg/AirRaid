using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader : MonoBehaviour {

    private List<Defender> myGroup = new List<Defender>();
    private Transform target;
    private bool readyToFight;
    private float delayToCheckTarget = 5f, lastTimeTargetCheked;
    private float delayToGiveTargetToHealer = 3f, lastTimeHealerTarget;

    public void Refresh() {
        //Every 5 sec, check if enemy closer than actual target
        if (this.readyToFight && Time.time - this.lastTimeTargetCheked >= this.delayToCheckTarget) {
            if (this.myGroup.Count == 0)
                SelfDestroy();
            else {
                FindNewTarget();
                GiveTargetToDefenders();
            }
            this.lastTimeTargetCheked = Time.time;
        }
        //Every 3 sec, give new target to healers
        if (Time.time - this.lastTimeHealerTarget > this.delayToGiveTargetToHealer) {
            GiveTargetToHealer();
            this.lastTimeHealerTarget = Time.time;
        }
        if (this.readyToFight)
            for (int i = this.myGroup.Count - 1; i >= 0; i--) {
                this.myGroup[i].Refresh();
            }
    }

    public void PhysicRefresh() {
        if (this.readyToFight) {
            //Move toward target
        }
        for (int i = this.myGroup.Count - 1; i >= 0; i--) {
            this.myGroup[i].PhysicRefresh();
        }
    }

    private void FindNewTarget() {
        //Find smallest distance between leader and an enemy
        float smallestDistance = float.MaxValue, distance;
        foreach (Enemy enemy in EnemyManager.Instance.enemies) {
            distance = Vector3.Distance(transform.position, enemy.transform.position);
            Debug.Log(distance.ToString());
            if (distance < smallestDistance) {
                smallestDistance = distance;
                this.target = enemy.transform;
            }
        }
    }

    private void GiveTargetToDefenders() {
        for (int i = this.myGroup.Count - 1; i >= 0; i--) {
            switch (this.myGroup[i].defenderInfos.type) {
                case TypeDefender.MELEE:
                case TypeDefender.RANGE:
                    this.myGroup[i].SetTarget(this.target);
                    break;
            }
        }
    }

    private void GiveTargetToHealer() {
        for (int i = this.myGroup.Count - 1; i >= 0; i--) {
            switch (this.myGroup[i].defenderInfos.type) {
                case TypeDefender.SUPPORT:
                    this.myGroup[i].SetTarget(GetDefenderToHeal());
                    break;
            }
        }
    }
    private Transform GetDefenderToHeal() {
        Defender lowestHealthDefender = this.myGroup[0];
        for (int i = this.myGroup.Count - 1; i >= 0; i--) {
            if (this.myGroup[i].defenderInfos.maxHp - this.myGroup[i].defenderInfos.hp > lowestHealthDefender.defenderInfos.maxHp - lowestHealthDefender.defenderInfos.hp)
                lowestHealthDefender = this.myGroup[i];
        }
        return lowestHealthDefender.transform;
    }
    private void SelfDestroy() {
        DefenderManager.Instance.RemoveLeaderFromList(this);
        GameObject.Destroy(gameObject);
    }

    public void GoFight() {
        this.readyToFight = true;
    }
    public void AddDefenderToMyGroup(Defender defender) {
        this.myGroup.Add(defender);
    }

    public void RemoveDefenderFromMyGroup(Defender defender) {
        this.myGroup.Remove(defender);
    }


    private void OnDrawGizmos() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(new Vector3(transform.position.x, 5, transform.position.z), 0.5f);
    }
}
