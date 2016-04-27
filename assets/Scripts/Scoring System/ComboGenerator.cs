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


	// Use this for initialization
	void Start () {
        instance = this;
    }
	
    // "ACTION" FUNCTIONS //
    // These are accessed by other objects in the scene
    // to UPDATE the COMBO STATS script with new information
	// and TRIGGER and relevant SCORING PLAY CHECKS

	public static void ActionDartTag (string playerHit)
	{
		instance.lastTagTime = ComboStats.instance.lastTagTime;
        ComboStats.instance.AddDartTag(playerHit);
		instance.CheckTag (playerHit);		
	}

	public static void ActionSodaHit (string playerHit)
	{
		ComboStats.instance.AddSodaHit(playerHit);
		instance.CheckThisIsAStickUp (playerHit);
	}

	public static void ActionRespawn ()
	{
		ComboStats.instance.RespawnClear ();
	}


    // SCORING PLAY CHECK FUNCTIONS //
    // These are used to TEST for conditions in COMBO STATS and
	// UPDATE the COMBO COUNTER when
    // a Scoring Play has been achieved.

   
    private void CheckTag(string playerHit)
    {
        
        if (tripleTagEnabled && ComboStats.instance.lastTagTime < (lastTagTime + tripleTagGap)) // DOUBLE TAG
        {
            ComboCounter.addCombo(ComboList.Combos.ThreesCompany, playerHit);
            float value = ComboList.getComboValue(ComboList.Combos.ThreesCompany);
            instance.scoringPlayHolder.DisplayScoringPlay("Three's Company! " + value + "Pts");
            doubleTagEnabled = false;
            tripleTagEnabled = false;
        }
        else if (doubleTagEnabled && ComboStats.instance.lastTagTime < (lastTagTime + doubleTagGap)) // DOUBLE TAG
        {
            ComboCounter.addCombo(ComboList.Combos.DoubleTap, playerHit);
            float value = ComboList.getComboValue(ComboList.Combos.DoubleTap);
            instance.scoringPlayHolder.DisplayScoringPlay("Double Tap! " + value + "Pts");
            doubleTagEnabled = false;
            tripleTagEnabled = true;    
        }
        else // SINGLE TAG
        {
            ComboCounter.addCombo(ComboList.Combos.SingleTag, playerHit);
            float value = ComboList.getComboValue(ComboList.Combos.SingleTag);
            instance.scoringPlayHolder.DisplayScoringPlay("Single Tag! " + value + "Pts");
            doubleTagEnabled = true;
            tripleTagEnabled = false;
        }
        ComboCounter.addComboToScore();
        
    }

    private void CheckThisIsAStickUp(string playerHit)
    {        
        ComboCounter.addCombo(ComboList.Combos.ThisIsAStickUp, playerHit);
        float value = ComboList.getComboValue(ComboList.Combos.ThisIsAStickUp);
        instance.scoringPlayHolder.DisplayScoringPlay("This is a Stick Up! " + value + "Pts");
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

    private void comeAndGetMe()
    {
        ComboCounter.addCombo(ComboList.Combos.ComeAndGetMe, "");
    }

    private void starSixtyNine(string playerHit)
    {
        ComboCounter.addCombo(ComboList.Combos.StarSixtyNine, playerHit);
    }

    private void phoneTag(string playerHit)
    {
        ComboCounter.addCombo(ComboList.Combos.PhoneTag, playerHit);
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

    private void theCommuter(string playerHit)
    {
        ComboCounter.addCombo(ComboList.Combos.TheCommuter, playerHit);
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

    private void nearMiss(string shooterName)
    {
        //passUpProwessTimer = passUpProwessTimeWindow;
       // ComboCounter.addCombo(ComboList.Combos.CloseCall, shooterName);
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
