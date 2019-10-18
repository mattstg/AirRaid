using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader : MonoBehaviour {

    private List<Defender> myGroup = new List<Defender>();
    private Transform target;
    private bool readyToFight;

    public void Refresh() {
        if (this.readyToFight)
            if (this.target == null) {
                FindNewTarget();
                GiveTargetToDefenders();
            }
        for (int i = this.myGroup.Count - 1; i >= 0; i--) {
            this.myGroup[i].Refresh();
        }
    }

    public void PhysicRefresh() {
        for (int i = this.myGroup.Count - 1; i >= 0; i--) {
            this.myGroup[i].PhysicRefresh();
        }
    }

    private void FindNewTarget() {
        //Logic to find new Target
        this.target = GameObject.FindGameObjectWithTag("Enemy_Egg").transform;
    }

    private Transform GetDefenderToHeal() {
        Defender lowestHealthDefender = this.myGroup[0];
        for (int i = this.myGroup.Count - 1; i >= 0; i--) {
            if (this.myGroup[i].defenderInfos.maxHp - this.myGroup[i].defenderInfos.hp < lowestHealthDefender.defenderInfos.maxHp - lowestHealthDefender.defenderInfos.hp)
                lowestHealthDefender = this.myGroup[i];
        }
        return lowestHealthDefender.transform;
    }

    private void GiveTargetToDefenders() {
        for (int i = this.myGroup.Count - 1; i >= 0; i--) {
            switch (this.myGroup[i].defenderInfos.type) {
                case TypeDefender.MELEE:
                case TypeDefender.RANGE:
                    this.myGroup[i].SetTarget(this.target);
                    break;
                case TypeDefender.SUPPORT:
                    this.myGroup[i].SetTarget(GetDefenderToHeal());
                    break;
            }
        }
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
