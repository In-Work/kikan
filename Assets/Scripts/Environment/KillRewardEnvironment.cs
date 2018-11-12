﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public static class KillRewardEnvironment {
    public static KillReward GetRewardFrom(IBattle target, int teamSize) {
      double rewardRatio = 1;

      if (teamSize == 1)
        rewardRatio = 0.7;
      if (teamSize == 2)
        rewardRatio = 0.6;

      var killReward = new KillReward();

      killReward.MainExp  = (int)(target.KillExp  * rewardRatio);
      killReward.MainGold = (int)(target.KillGold * rewardRatio);

      if (teamSize > 0) {
        killReward.SubExp  = (int)(target.KillExp  * ((1.0 - rewardRatio) / teamSize));
        killReward.SubGold = (int)(target.KillGold * ((1.0 - rewardRatio) / teamSize));
      }

      return killReward;
    }

    public static void GiveRewardTo(IPlayer player, KillReward killReward) {
      player.Exp.Add(killReward.MainExp);
      player.Gold.Add(killReward.MainGold);

      foreach (var teammate in player.Teammates) {
        teammate.Exp.Add(killReward.SubExp);
        teammate.Gold.Add(killReward.SubGold);
      }
    }
  }

  public class KillReward {
    public int MainExp  = 0;
    public int MainGold = 0;
    public int SubExp   = 0;
    public int SubGold  = 0;
  }
}

