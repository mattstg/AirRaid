using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Leader : MonoBehaviour {

    private NavMeshAgent agent;
    private Animator animator;
    private AudioSource speaker;
    private List<AudioClip> walkingSounds = new List<AudioClip>();
    private List<Defender> myGroup = new List<Defender>();
    private Transform target;
    private bool readyToFight;
    private float delayToCheckTarget = 5f, lastTimeTargetCheked;
    private float delayToGiveTargetToHealer = 3f, lastTimeHealerTarget;

    public void Initialize() {
        this.agent = transform.GetComponent<NavMeshAgent>();
        this.animator = transform.GetComponent<Animator>();
        this.speaker = transform.GetComponent<AudioSource>();
        this.walkingSounds.Add(Resources.Load<AudioClip>("Characters/Sounds/StepOnGrassSound1"));
        this.walkingSounds.Add(Resources.Load<AudioClip>("Characters/Sounds/StepOnGrassSound2"));
    }

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
            if (this.target != null) {
                this.agent.SetDestination(new Vector3(this.target.transform.position.x, 0, this.target.transform.position.z));
                this.animator.SetFloat("speedVelocity", this.agent.speed);
            }
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

    public void PlaySound(AudioClip clip) {
        this.speaker.PlayOneShot(clip);
    }

    private void Step() {
        //Called by event on the animation
        PlaySound(GetRandomWalkingSound());
    }
    private AudioClip GetRandomWalkingSound() {
        return this.walkingSounds[Random.Range(0, this.walkingSounds.Count - 1)];
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(new Vector3(transform.position.x, 5, transform.position.z), 0.5f);
    }
}
