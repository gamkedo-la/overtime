using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ComboGenerator : MonoBehaviour {

    // This holds all of the functions for acknowledging a Scoring Play
    // has occured and triggering a Combo Counter update.

    public static ComboGenerator instance;
    public ScoringPlayHolder scoringPlayHolder;
    public float doubleTagGap = 3F;
    public float tripleTagGap = 5F;
    private float lastTagTime = 0;
    private bool doubleTagEnabled = false;
    private bool tripleTagEnabled = false;

	public float longRangeShotLimit = 30;
    public float shortRangeShotLimit = 3;


	// Use this for initialization
	void Start () {
        instance = this;
    }
	
    // "ACTION" FUNCTIONS //
    // These are accessed by other objects in the scene
    // to UPDATE the COMBO STATS script with new information
	// and TRIGGER and relevant SCORING PLAY CHECKS

	public static void ActionDartTag (string playerHit, float distanceTravelled, string colliderType)
	{
		instance.lastTagTime = ComboStats.instance.lastTagTime;
        ComboStats.instance.AddDartTag(playerHit, distanceTravelled);
		instance.CheckTag (playerHit, distanceTravelled, colliderType);	

	}

	public static void ActionSodaHit (string playerHit)
	{
		ComboStats.instance.AddSodaHit(playerHit);
		instance.CheckStickyHit (playerHit);
	}

	public static void ActionWireTrip (string tripOwner, string playerHit)
	{
		//instance.lastTagTime = ComboStats.instance.lastTagTime;
		if (tripOwner == PhotonNetwork.player.name)
		{
			ComboStats.instance.AddTripwireHit (playerHit);
			instance.CheckTrip (tripOwner, playerHit);
		}
	}

	public static void ActionNearMiss ()
	{
		ComboStats.instance.AddNearMiss();
		instance.CheckNearMiss ();
	}

	public static void ActionRespawn ()
	{
		ComboStats.instance.RespawnClear ();
	}


    // SCORING PLAY CHECK FUNCTIONS //
    // These are used to TEST for conditions in COMBO STATS and
	// UPDATE the COMBO COUNTER when
    // a Scoring Play has been achieved.

   
    private void CheckTag(string playerHit, float distanceTravelled, string colliderType)
    {
        
		// THE COMMUTER - Long Range Tag //
		if (distanceTravelled > longRangeShotLimit)
		{
			ComboCounter.addCombo(ComboList.Combos.TheCommuter, playerHit);
			float value = ComboList.getComboValue(ComboList.Combos.TheCommuter);
			string comboName = ComboList.getComboName (ComboList.Combos.TheCommuter);
			instance.scoringPlayHolder.DisplayScoringPlay (comboName + "! " + value + "Pts");
		}

        // THE WHITE OF THEIR EYES - Point Blank Tag //
        if (distanceTravelled < shortRangeShotLimit)
        {
            ComboCounter.addCombo(ComboList.Combos.TheWhitesOfTheirEyes, playerHit);
            float value = ComboList.getComboValue(ComboList.Combos.TheWhitesOfTheirEyes);
            string comboName = ComboList.getComboName (ComboList.Combos.TheWhitesOfTheirEyes);
            instance.scoringPlayHolder.DisplayScoringPlay (comboName + "! " + value + "Pts");
        }

		if (tripleTagEnabled && ComboStats.instance.lastTagTime < (lastTagTime + tripleTagGap)) // TRIPLE TAG //
        {
            ComboCounter.addCombo(ComboList.Combos.ThreesCompany, playerHit);
            float value = ComboList.getComboValue(ComboList.Combos.ThreesCompany);
			string comboName = ComboList.getComboName (ComboList.Combos.ThreesCompany);
			instance.scoringPlayHolder.DisplayScoringPlay (comboName + "! " + value + "Pts");
			doubleTagEnabled = false;
            tripleTagEnabled = false;
        }
        else if (doubleTagEnabled && ComboStats.instance.lastTagTime < (lastTagTime + doubleTagGap)) // DOUBLE TAG
        {
            ComboCounter.addCombo(ComboList.Combos.DoubleTime, playerHit);
            float value = ComboList.getComboValue(ComboList.Combos.DoubleTime);
			string comboName = ComboList.getComboName (ComboList.Combos.DoubleTime);
			instance.scoringPlayHolder.DisplayScoringPlay (comboName + "! " + value + "Pts");
			doubleTagEnabled = false;
            tripleTagEnabled = true;    
        }
        else // SINGLE TAG //
        {
            ComboCounter.addCombo(ComboList.Combos.SingleTag, playerHit);
            float value = ComboList.getComboValue(ComboList.Combos.SingleTag);
			string comboName = ComboList.getComboName (ComboList.Combos.SingleTag);
			instance.scoringPlayHolder.DisplayScoringPlay (comboName + "! " + value + "Pts");
			doubleTagEnabled = true;
            tripleTagEnabled = false;
        }

        // CS OUTSOURCED - Head Shot Tag //
        if (colliderType == "Head")
        {
            ComboCounter.addCombo(ComboList.Combos.CsOutsourced, playerHit);
            float value = ComboList.getComboValue(ComboList.Combos.CsOutsourced);
            string comboName = ComboList.getComboName (ComboList.Combos.CsOutsourced);
            instance.scoringPlayHolder.DisplayScoringPlay (comboName + "! " + value + "Pts");
        }

        // HUMAN RESOURCELESS - Crotch/Butt Shot Tag //
        if (colliderType == "Hips")
        {
            ComboCounter.addCombo(ComboList.Combos.HumanResourceless, playerHit);
            float value = ComboList.getComboValue(ComboList.Combos.HumanResourceless);
            string comboName = ComboList.getComboName (ComboList.Combos.HumanResourceless);
            instance.scoringPlayHolder.DisplayScoringPlay (comboName + "! " + value + "Pts");
        }

        ComboCounter.addComboToScore();
        
    }

   

    private void CheckStickyHit(string playerHit)
    {        
        ComboCounter.addCombo(ComboList.Combos.ThisIsAStickUp, playerHit);
        float value = ComboList.getComboValue(ComboList.Combos.ThisIsAStickUp);
		string comboName = ComboList.getComboName (ComboList.Combos.ThisIsAStickUp);
		instance.scoringPlayHolder.DisplayScoringPlay (comboName + "! " + value + "Pts");
	}
	
	private void CheckTrip(string owner, string playerHit)
	{
		// COME AND GET ME - Trip Self //
		if (owner == playerHit) 
		{
			ComboCounter.addCombo (ComboList.Combos.ComeAndGetMe, playerHit);
			float value = ComboList.getComboValue (ComboList.Combos.ComeAndGetMe);
			string comboName = ComboList.getComboName (ComboList.Combos.ComeAndGetMe);
			instance.scoringPlayHolder.DisplayScoringPlay (comboName + "! " + value + "Pts");
		}

		// PHONE TAG - Trip another //
		if (owner != playerHit)
		{
			ComboCounter.addCombo (ComboList.Combos.PhoneTag, playerHit);
			float value = ComboList.getComboValue (ComboList.Combos.PhoneTag);
			string comboName = ComboList.getComboName (ComboList.Combos.PhoneTag);
			instance.scoringPlayHolder.DisplayScoringPlay (comboName + "! " + value + "Pts");
		}
	}

	private void CheckNearMiss()
	{
		string playerHit = "null"; // REPLACE THIS WHEN TARGETS ARE IN PLAY
		ComboCounter.addCombo (ComboList.Combos.NearMiss, playerHit);
		float value = ComboList.getComboValue (ComboList.Combos.NearMiss);
		string comboName = ComboList.getComboName (ComboList.Combos.NearMiss);
		instance.scoringPlayHolder.DisplayScoringPlay (comboName + "! " + value + "Pts");
	}




   



   // STILL TO DO //



    private void unorthodonculous(string playerHit)
    {
        ComboCounter.addCombo(ComboList.Combos.Unorthodonculous, playerHit);
    }

    private void aStapleMove(string playerHit)
    {
        ComboCounter.addCombo(ComboList.Combos.AStapleMove, playerHit);
    }

    private void humanCorkboard(string playerHit)
    {
        ComboCounter.addCombo(ComboList.Combos.HumanCorkboard, playerHit);
    }

    private void memoRies(string playerHit)
    {
        ComboCounter.addCombo(ComboList.Combos.MemoRies, playerHit);
    }

    private void ifYouCantDodgeAWrench(string playerHit)
    {
        ComboCounter.addCombo(ComboList.Combos.IfYouCantDodgeAWrench, playerHit);
    }

    private void canIBounceSomethingAtYou(string playerHit)
    {
        ComboCounter.addCombo(ComboList.Combos.CanIBounceSomethingAtYou, playerHit);
    }

    private void doubleYouToo(string playerHit)
    {
        ComboCounter.addCombo(ComboList.Combos.DoubleYouToo, playerHit);
    }

    private void starSixtyNine(string playerHit)
    {
        ComboCounter.addCombo(ComboList.Combos.StarSixtyNine, playerHit);
    }

   

    private void coolRefreshingPushy(string playerHit)
    {
        ComboCounter.addCombo(ComboList.Combos.CoolRefreshingPushy, playerHit);
    }

    private void soaked(string playerHit)
    {
        ComboCounter.addCombo(ComboList.Combos.Soaked, playerHit);
    }

    private void humanResourceless(string playerHit)
    {
        ComboCounter.addCombo(ComboList.Combos.HumanResourceless, playerHit);
    }

    private void csOutsourced(string playerHit)
    {
        ComboCounter.addCombo(ComboList.Combos.CsOutsourced, playerHit);
    }

    private void cubiKill(string playerHit)
    {
        ComboCounter.addCombo(ComboList.Combos.CubiKill, playerHit);
    }

    private void outlookOut(string playerHit)
    {
        ComboCounter.addCombo(ComboList.Combos.OutlookOut, playerHit);
    }

    private void topDownShooter(string playerHit)
    {
        ComboCounter.addCombo(ComboList.Combos.TopDownShooter, playerHit);
    }

    private void theWhitesOfTheirEyes(string playerHit)
    {
        ComboCounter.addCombo(ComboList.Combos.TheWhitesOfTheirEyes, playerHit);
    }

    private void noHandsNoProblem(string playerHit)
    {
        ComboCounter.addCombo(ComboList.Combos.NoHandsNoProblem, playerHit);
    }

    

    private void aroundTheWorld()
    {
        ComboCounter.addCombo(ComboList.Combos.AroundTheWorld, "");
    }

    private void lunchThief(string playerHit)
    {
        ComboCounter.addCombo(ComboList.Combos.LunchThief, playerHit);
    }

   
    private void backAttackStapler(string playerHitName)
    {
        ComboCounter.addCombo(ComboList.Combos.AStapleMove, playerHitName);
    }


    private void setOffSprinklers()
    {
        ComboCounter.addCombo(ComboList.Combos.WhenItRains, "");
    }



    // LEFTOVER CODE //

    /* private void gotTagged()
    {
        lastDeathTime = Time.time;
    }

    private void taggedPlayer(string playerTaggedName)
    {
        bool playerTaggedWithinTenSeconds = false;
        List<string> playersTaggedThisLife = new List<string>();

        for (int i = 0; i < playersTagged.Count; i++)
        {
            if (playersTagged[i].time >= Time.time - 3.0f)
            {
                Debug.Log("Double tag!");
                //DOUBLE TAG
                ComboCounter.addCombo(ComboList.Combos.DoubleTime, playerTaggedName);
            }

            if (playersTagged[i].time >= Time.time - 10.0f)
            {
                if (playerTaggedWithinTenSeconds)
                {
                    //TRIPPLE TAG!!!
                    ComboCounter.addCombo(ComboList.Combos.ThreesCompany, playerTaggedName);
                }
                else
                {
                    playerTaggedWithinTenSeconds = true;
                }
            }

            if(playersTagged[i].time >= lastDeathTime && !playersTaggedThisLife.Contains(playersTagged[i].playerName))
            {
                playersTaggedThisLife.Add(playersTagged[i].playerName);
            }
        }

        //Change if players includes this player
        if(playersTaggedThisLife.Count == players.Length)
        {
            Debug.Log("Around the world!");
            ComboCounter.addCombo(ComboList.Combos.AroundTheWorld, playerTaggedName);
        }

        //TODO change this to get the other player's last near miss time
            //Player taggedPlayer = getPlayerByName(taggedPlayerName);
            //playerTaggedComboGenerator = taggedPlayer.gameObject.getComponent<ComboGenerator>();
        ComboGenerator playerTaggedComboGenerator = new ComboGenerator(); //Monobehaviour is gonna be mad

        if(playerTaggedComboGenerator.getPassUpProwessTimer() > 0)
        {
            Debug.Log("Pass Up Prowess\nWARNING: THIS NEEDS TO BE CODED TO ACTUALLY CHECK THE OTHER PLAYER");
            ComboCounter.addCombo(ComboList.Combos.PassUpProwess, playerTaggedName);
            //Alternative method: just have the near miss script keep a timer, then on destruction if the timer
            //is greater than zero, send a pass up prowess event
        }

        if (playerTaggedName == targetName)
        {
            ComboCounter.addComboToScore();
        }
        else
        {
            playersTagged.Add(new playerTag(playerTaggedName));
        }
    }

    private void activatedTripwire(Tripwire tripwireIn)
    {
        if (!activeTripwires.Contains(tripwireIn))
        {
            activeTripwires.Add(tripwireIn);
        }
    }

   /* private void deactivatedTripwire(Tripwire tripwireIn)
    {
        activeTripwires.Remove(tripwireIn);
    }

    private void shotPlayer(string otherPlayerName)
    {
        for(int i = 0; i < activeTripwires.Count; i++)
        {
// TODO make commented code funtional
//            if(activeTripwires[i].getOwner() == otherPlayerName)
//            {
                ComboCounter.addCombo(ComboList.Combos.StarSixtyNine, otherPlayerName);
//            }
//
        }
    } 

    private void switchedToDartWeapon()
    {
        mostRecentNonDartWeaponHit = new string[players.Length];
    }*/

     /*
    void OnEnable()
    {
        EventManager.StartListening(StringEventName.NearMiss, nearMiss);
        EventManager.StartListening(StringEventName.BackAttackStapler, backAttackStapler);
        EventManager.StartListening(StandardEventName.SetOffSprinklers, setOffSprinklers);
    }

    void OnDisable()
    {
        EventManager.StopListening(StringEventName.NearMiss, nearMiss);
        EventManager.StopListening(StringEventName.BackAttackStapler, backAttackStapler);
        EventManager.StopListening(StandardEventName.SetOffSprinklers, setOffSprinklers);
    }
    */


   /*public float getPassUpProwessTimer()
    {
       return passUpProwessTimer;
       
    }*/
}
