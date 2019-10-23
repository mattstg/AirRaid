using System.Collections.Generic;
using UnityEngine;

//Rewind ability,  go back in time 3 seconds

public class Ab_Rewind : Ability
{

    //Rewind Variables

    private float abilityTimer = 7f;
    float savedEnergy;
    private LinkedList<RewindPositions> rewindQueue;
    private float refreshCounter = 3f;
    private bool rewindHappening;
    private Material basicSkin;
    private Material teleportFade;
    private Renderer rd;
    private Rigidbody rb;

    public Ab_Rewind(PlayerController _pc) : base(_pc)
    {
        stats = new AbilityStats(this, Abilities.Rewind, UpdateType.FixedUpdate, 7f, 35f);

        rewindQueue = new LinkedList<RewindPositions>();
        rewindHappening = false;
        rd = _pc.GetComponent<Renderer>();
        rb = _pc.GetComponent<Rigidbody>();
        basicSkin = Resources.Load<Material>("Material/basicSkin");
        teleportFade = Resources.Load<Material>("Material/teleportFade");
    }

    public override void AbilityPressed()
    {
        UseAbility();
        base.AbilityPressed();
    }

    public override void AbilityHeld()
    {

        base.AbilityHeld();
    }

    public override bool UseAbility()
    {
        
        if (base.UseAbility())
        {
            Debug.Log("in Ablilty");
            if (abilityTimer < 0 && !pc.stats.engineStalled)
            {
                //Rewind Switch bool
                rewindHappening = true;
                savedEnergy = rewindQueue.Last.Value.savedEnergy;
                rb.velocity = new Vector3(0, 0, 0);
                rd.material = teleportFade;
                pc.GetComponent<AudioSource>().Play();
                return true;
            }
        }
        return false;
    }

    public override void AbilityRelease()
    {
        base.AbilityRelease();
    }

    public override void AbilityUpdate()
    {
        //Ability Timers

        refreshCounter -= Time.deltaTime;
        abilityTimer -= Time.deltaTime;
        if (abilityTimer < 0 && !rewindHappening)
        {
            Debug.Log("Rewind READY!");
        }


        //Add data to Queue /OR/ Remove old Data then Add new
        if (!rewindHappening)
        {

            if (refreshCounter < 0)
            { 

                rewindQueue.RemoveFirst();

                rewindQueue.AddLast(new RewindPositions(pc.transform.position, rb.velocity, rb.rotation, pc.stats.currentEnegy, pc.stats.hp));
            }
            else
            {
                rewindQueue.AddLast(new RewindPositions(pc.transform.position, rb.velocity, rb.rotation, pc.stats.currentEnegy, pc.stats.hp));
            }
        }

        if (rewindHappening)
        {
            

            Debug.Log("Size of queue in routine = " + rewindQueue.Count);
            if (rewindQueue.Count >= 2)
            {
                rewindQueue.RemoveLast();
                pc.transform.position = rewindQueue.Last.Value.savedPos;
                rb.velocity = rewindQueue.Last.Value.savedVelo;
                rb.rotation = rewindQueue.Last.Value.savedRot;
               
            }
            else
            {
                pc.stats.currentEnegy = savedEnergy;
                rewindQueue.Clear();
                rewindHappening = false;
                rd.material = basicSkin;
                refreshCounter = 3f;
                abilityTimer = 7f;
                Debug.Log("Rewind DONE!");
            }

        }
        base.AbilityUpdate();
    }


    public class RewindPositions
    {
        public Vector3 savedPos;
        public Quaternion savedRot;
        public Vector3 savedVelo;
        public float savedHp;
        public float savedEnergy;


        public RewindPositions(Vector3 pos, Vector3 velo, Quaternion rotation, float savedEnergy, float savedHp)
        {
            this.savedPos = pos;
            this.savedVelo = velo;
            this.savedRot = rotation;
            this.savedHp = savedHp;
            this.savedEnergy = savedEnergy;
        }
    }
}
