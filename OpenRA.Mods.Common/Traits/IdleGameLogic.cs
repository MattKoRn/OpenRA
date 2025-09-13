#region Copyright & License Information
/*
 * Copyright (c) The OpenRA Developers and Contributors
 * This file is part of OpenRA, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */
#endregion

using System.Collections.Generic;
using OpenRA.Traits;

namespace OpenRA.Mods.Common.Traits
{
	[Desc("Manages idle game mechanics including resource generation and upgrades.")]
	public class IdleGameLogicInfo : TraitInfo
	{
		[Desc("Initial amount of resources the player starts with.")]
		public readonly int InitialResources = 100;

		[Desc("Base resources generated per second.")]
		public readonly int BaseResourcesPerSecond = 1;

		[Desc("Ticks between resource generation updates.")]
		public readonly int UpdateInterval = 40; // 1 second at 40 FPS

		public override object Create(ActorInitializer init) { return new IdleGameLogic(this); }
	}

	public class IdleGameLogic : ITick, INotifyCreated
	{
		readonly IdleGameLogicInfo info;
		
		public int Resources { get; private set; }
		public int ResourcesPerSecond { get; private set; }
		
		// Generator data
		public int BasicGenerators { get; private set; }
		public int BasicGeneratorCost { get; private set; } = 100;
		
		// Click booster data  
		public int ClickValue { get; private set; } = 50;
		public int ClickBoosterLevel { get; private set; }
		public int ClickBoosterCost { get; private set; } = 500;
		
		private int tickCounter;

		public IdleGameLogic(IdleGameLogicInfo info)
		{
			this.info = info;
			Resources = info.InitialResources;
			ResourcesPerSecond = info.BaseResourcesPerSecond;
		}

		void INotifyCreated.Created(Actor self)
		{
			// Initialize if needed
		}

		void ITick.Tick(Actor self)
		{
			if (++tickCounter >= info.UpdateInterval)
			{
				tickCounter = 0;
				
				// Generate resources based on generators
				var generatedResources = ResourcesPerSecond + (BasicGenerators * 10);
				Resources += generatedResources;
			}
		}

		public bool TryBuyGenerator()
		{
			if (Resources >= BasicGeneratorCost)
			{
				Resources -= BasicGeneratorCost;
				BasicGenerators++;
				BasicGeneratorCost = (int)(BasicGeneratorCost * 1.5f); // Increase cost
				return true;
			}
			return false;
		}

		public bool TryBuyClickBooster()
		{
			if (Resources >= ClickBoosterCost)
			{
				Resources -= ClickBoosterCost;
				ClickBoosterLevel++;
				ClickValue = (int)(ClickValue * 1.5f); // Increase click value
				ClickBoosterCost = (int)(ClickBoosterCost * 2.0f); // Increase cost
				return true;
			}
			return false;
		}

		public void PerformClick()
		{
			Resources += ClickValue;
		}
	}
}