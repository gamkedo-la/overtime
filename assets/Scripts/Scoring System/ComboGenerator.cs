using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ComboGenerator : MonoBehaviour {

    // This holds all of the functions for acknowledging a Scoring Play
    // has occured and triggering a Combo Counter update.

    public static ComboGenerator Instance;


    public ComboCounter comboCounter;
    public ComboStats comboStats;


    

	// Use this for initialization
	void Start () {
        Instance = this;
    }
	
	
    // STAT UPDATE FUNCTIONS //
    // These are accessed by other objects in the scene
    // to update the Combo Stats script with new information

    public void AddStatSodaHit ()
    {   
        comboStats.AddSodaHit();
    }   



    // SCORING PLAY RECOGNITION FUNCTIONS //
    // These are used to update the Combo Counter when
    // a Scoring Play has been achieved.

    private void simpleSingleTagTest(string playerHit)
    {
        comboCounter.addCombo(ComboList.Combos.SimpleSingleTagTest, playerHit);
    }

    private void unorthodonculous(string playerHit)
    {
        comboCounter.addCombo(ComboList.Combos.Unorthodonculous, playerHit);
    }

    private void aStapleMove(string playerHit)
    {
        comboCounter.addCombo(ComboList.Combos.AStapleMove, playerHit);
    }

    private void humanCorkboard(string playerHit)
    {
        comboCounter.addCombo(ComboList.Combos.HumanCorkboard, playerHit);
    }

    private void memoRies(string playerHit)
    {
        comboCounter.addCombo(ComboList.Combos.MemoRies, playerHit);
    }

    private void ifYouCantDodgeAWrench(string playerHit)
    {
        comboCounter.addCombo(ComboList.Combos.IfYouCantDodgeAWrench, playerHit);
    }

    private void canIBounceSomethingAtYou(string playerHit)
    {
        comboCounter.addCombo(ComboList.Combos.CanIBounceSomethingAtYou, playerHit);
    }

    private void doubleYouToo(string playerHit)
    {
        comboCounter.addCombo(ComboList.Combos.DoubleYouToo, playerHit);
    }

    private void comeAndGetMe()
    {
        comboCounter.addCombo(ComboList.Combos.ComeAndGetMe, "");
    }

    private void starSixtyNine(string playerHit)
    {
        comboCounter.addCombo(ComboList.Combos.StarSixtyNine, playerHit);
    }

    private void phoneTag(string playerHit)
    {
        comboCounter.addCombo(ComboList.Combos.PhoneTag, playerHit);
    }

    private void thisIsAStickUp(string playerHit)
    {
        comboCounter.addCombo(ComboList.Combos.ThisIsAStickUp, playerHit);
    }

    private void coolRefreshingPushy(string playerHit)
    {
        comboCounter.addCombo(ComboList.Combos.CoolRefreshingPushy, playerHit);
    }

    private void soaked(string playerHit)
    {
        comboCounter.addCombo(ComboList.Combos.Soaked, playerHit);
    }

    private void humanResourceless(string playerHit)
    {
        comboCounter.addCombo(ComboList.Combos.HumanResourceless, playerHit);
    }

    private void csOutsourced(string playerHit)
    {
        comboCounter.addCombo(ComboList.Combos.CsOutsourced, playerHit);
    }

    private void cubiKill(string playerHit)
    {
        comboCounter.addCombo(ComboList.Combos.CubiKill, playerHit);
    }

    private void outlookOut(string playerHit)
    {
        comboCounter.addCombo(ComboList.Combos.OutlookOut, playerHit);
    }

    private void theCommuter(string playerHit)
    {
        comboCounter.addCombo(ComboList.Combos.TheCommuter, playerHit);
    }

    private void topDownShooter(string playerHit)
    {
        comboCounter.addCombo(ComboList.Combos.TopDownShooter, playerHit);
    }

    private void theWhitesOfTheirEyes(string playerHit)
    {
        comboCounter.addCombo(ComboList.Combos.TheWhitesOfTheirEyes, playerHit);
    }

    private void noHandsNoProblem(string playerHit)
    {
        comboCounter.addCombo(ComboList.Combos.NoHandsNoProblem, playerHit);
    }

    private void doubleTap(string playerHit)
    {
        comboCounter.addCombo(ComboList.Combos.DoubleTap, playerHit);
    }

    private void aroundTheWorld()
    {
        comboCounter.addCombo(ComboList.Combos.AroundTheWorld, "");
    }

    private void lunchThief(string playerHit)
    {
        comboCounter.addCombo(ComboList.Combos.LunchThief, playerHit);
    }

   
    private void backAttackStapler(string playerHitName)
    {
        comboCounter.addCombo(ComboList.Combos.AStapleMove, playerHitName);
    }

    private void nearMiss(string shooterName)
    {
        //passUpProwessTimer = passUpProwessTimeWindow;
       // comboCounter.addCombo(ComboList.Combos.CloseCall, shooterName);
    }

    private void setOffSprinklers()
    {
        comboCounter.addCombo(ComboList.Combos.WhenItRains, "");
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
                comboCounter.addCombo(ComboList.Combos.DoubleTime, playerTaggedName);
            }

            if (playersTagged[i].time >= Time.time - 10.0f)
            {
                if (playerTaggedWithinTenSeconds)
                {
                    //TRIPPLE TAG!!!
                    comboCounter.addCombo(ComboList.Combos.ThreesCompany, playerTaggedName);
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
            comboCounter.addCombo(ComboList.Combos.AroundTheWorld, playerTaggedName);
        }

        //TODO change this to get the other player's last near miss time
            //Player taggedPlayer = getPlayerByName(taggedPlayerName);
            //playerTaggedComboGenerator = taggedPlayer.gameObject.getComponent<ComboGenerator>();
        ComboGenerator playerTaggedComboGenerator = new ComboGenerator(); //Monobehaviour is gonna be mad

        if(playerTaggedComboGenerator.getPassUpProwessTimer() > 0)
        {
            Debug.Log("Pass Up Prowess\nWARNING: THIS NEEDS TO BE CODED TO ACTUALLY CHECK THE OTHER PLAYER");
            comboCounter.addCombo(ComboList.Combos.PassUpProwess, playerTaggedName);
            //Alternative method: just have the near miss script keep a timer, then on destruction if the timer
            //is greater than zero, send a pass up prowess event
        }

        if (playerTaggedName == targetName)
        {
            comboCounter.addComboToScore();
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
                comboCounter.addCombo(ComboList.Combos.StarSixtyNine, otherPlayerName);
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
