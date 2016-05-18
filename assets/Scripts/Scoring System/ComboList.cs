using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// This holds the listing of all possible Scoring Plays
// and their point values.

public static class ComboList
{
    private static int numberOfCombos;
    private static Dictionary<Combos, int> comboValues = new Dictionary<Combos, int>();
    private static Dictionary<Combos, string> comboNames = new Dictionary<Combos, string>();

    public enum Combos{
        AroundTheWorld,
        AStapleMove,
        CanIBounceSomethingAtYou,
        CloseCall,
        ComeAndGetMe,
        CoolRefreshingPushy,
        CsOutsourced,
        CubiKill,
        DoubleTap,
        DoubleTime,
        DoubleYouToo,
        HumanCorkboard,
        HumanResourceless,
        IfYouCantDodgeAWrench,
        LunchThief,
        MemoRies,
        NoHandsNoProblem,
        OutlookOut,
        PassUpProwess,
        PhoneTag,
        Soaked,
        StarSixtyNine,
        TheCommuter,
        TheWhitesOfTheirEyes,
        ThisIsAStickUp,
        ThreesCompany,
        TopDownShooter,
        Unorthodonculous,
        WhenItRains,
        SingleTag,
		NearMiss,
    }

    static ComboList()
    {
        comboValues.Add(Combos.AroundTheWorld, 5);
        comboValues.Add(Combos.AStapleMove, 5);
        comboValues.Add(Combos.CanIBounceSomethingAtYou, 5);
        comboValues.Add(Combos.CloseCall, 5);
        comboValues.Add(Combos.ComeAndGetMe, 5);
        comboValues.Add(Combos.CoolRefreshingPushy, 5);
        comboValues.Add(Combos.CsOutsourced, 5);
        comboValues.Add(Combos.CubiKill, 5);
        comboValues.Add(Combos.DoubleTap, 5);
        comboValues.Add(Combos.DoubleTime, 5);
        comboValues.Add(Combos.DoubleYouToo, 5);
        comboValues.Add(Combos.HumanCorkboard, 5);
        comboValues.Add(Combos.HumanResourceless, 5);
        comboValues.Add(Combos.IfYouCantDodgeAWrench, 5);
        comboValues.Add(Combos.LunchThief, 5);
        comboValues.Add(Combos.MemoRies, 5);
        comboValues.Add(Combos.NoHandsNoProblem, 5);
        comboValues.Add(Combos.OutlookOut, 5);
        comboValues.Add(Combos.PassUpProwess, 5);
        comboValues.Add(Combos.PhoneTag, 5);
        comboValues.Add(Combos.Soaked, 5);
        comboValues.Add(Combos.StarSixtyNine, 5);
        comboValues.Add(Combos.TheCommuter, 5);
        comboValues.Add(Combos.TheWhitesOfTheirEyes, 5);
        comboValues.Add(Combos.ThisIsAStickUp, 5);
        comboValues.Add(Combos.ThreesCompany, 5);
        comboValues.Add(Combos.TopDownShooter, 5);
        comboValues.Add(Combos.Unorthodonculous, 5);
        comboValues.Add(Combos.WhenItRains, 5);
		comboValues.Add(Combos.SingleTag, 5);
		comboValues.Add(Combos.NearMiss, 5);

        comboNames.Add(Combos.AroundTheWorld, "Around the World!");
        comboNames.Add(Combos.AStapleMove, "A Staple Move");
        comboNames.Add(Combos.CanIBounceSomethingAtYou, "Can I Bounce Something At You?");
        comboNames.Add(Combos.CloseCall, "Close Call");
        comboNames.Add(Combos.ComeAndGetMe, "Come and Get Me");
        comboNames.Add(Combos.CoolRefreshingPushy, "Cool! Refreshing! Pushy!");
        comboNames.Add(Combos.CsOutsourced, "CS OutSourced");
        comboNames.Add(Combos.CubiKill, "Cubi-kill");
        comboNames.Add(Combos.DoubleTap, "Double Tap");
        comboNames.Add(Combos.DoubleTime, "Double Time!");
        comboNames.Add(Combos.DoubleYouToo, "Double You, Too");
        comboNames.Add(Combos.HumanCorkboard, "Human Corkboard");
        comboNames.Add(Combos.HumanResourceless, "Human Resourceless");
        comboNames.Add(Combos.IfYouCantDodgeAWrench, "If you can't dodge a wrench...");
        comboNames.Add(Combos.LunchThief, "Lunch Thief");
        comboNames.Add(Combos.MemoRies, "Memo-ries");
        comboNames.Add(Combos.NoHandsNoProblem, "No Hands, No Problem");
        comboNames.Add(Combos.OutlookOut, "Outlook Out!");
        comboNames.Add(Combos.PassUpProwess, "Pass Up Prowess");
        comboNames.Add(Combos.PhoneTag, "Phone Tag");
        comboNames.Add(Combos.Soaked, "Soaked");
        comboNames.Add(Combos.StarSixtyNine, "*69");
        comboNames.Add(Combos.TheCommuter, "The Commuter");
        comboNames.Add(Combos.TheWhitesOfTheirEyes, "The Whites Of Their Eyes...");
        comboNames.Add(Combos.ThisIsAStickUp, "This is a Stick-Up");
        comboNames.Add(Combos.ThreesCompany, "3's Company");
        comboNames.Add(Combos.TopDownShooter, "Top-Down Shooter");
        comboNames.Add(Combos.Unorthodonculous, "Unorthodonculous");
        comboNames.Add(Combos.WhenItRains, "When it rains");
		comboNames.Add(Combos.SingleTag, "Single Tag");
		comboNames.Add(Combos.NearMiss, "Near Miss");

        if(comboValues.Count != comboNames.Count)
        {
            Debug.Log("WARNING: ComboList FAILED TO PROPPERLY INITIALIZE");
        }
        else
        {
            numberOfCombos = comboValues.Count;
        }
    }

    public static int getComboValue(Combos combo)
    {
        return comboValues[combo];
    }

    public static string getComboName(Combos combo)
    {
        return comboNames[combo];
    }

    public static int count()
    {
        return numberOfCombos;
    }
}
