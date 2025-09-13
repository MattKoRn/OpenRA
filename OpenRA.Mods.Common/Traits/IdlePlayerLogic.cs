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

using OpenRA.Traits;

namespace OpenRA.Mods.Common.Traits
{
	[Desc("Player-specific idle game logic.")]
	[TraitLocation(SystemActors.Player | SystemActors.EditorPlayer)]
	public class IdlePlayerLogicInfo : TraitInfo
	{
		public override object Create(ActorInitializer init) { return new IdlePlayerLogic(this); }
	}

	public class IdlePlayerLogic : INotifyCreated
	{
		readonly IdlePlayerLogicInfo info;

		public IdlePlayerLogic(IdlePlayerLogicInfo info)
		{
			this.info = info;
		}

		void INotifyCreated.Created(Actor self)
		{
			// Player-specific initialization if needed
		}
	}
}