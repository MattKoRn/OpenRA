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
using OpenRA.Graphics;
using OpenRA.Mods.Common.Traits;
using OpenRA.Orders;

namespace OpenRA.Mods.Common.Orders
{
	public class IdleOrderGenerator : IOrderGenerator
	{
		public IEnumerable<Order> Order(World world, CPos cell, int2 worldPixel, MouseInput mi)
		{
			// Handle clicks for the idle game
			if (mi.Button == MouseButton.Left && mi.Event == MouseInputEvent.Down)
			{
				var idleLogic = world.WorldActor.TraitOrDefault<IdleGameLogic>();
				if (idleLogic != null)
				{
					idleLogic.PerformClick();
				}
			}

			return [];
		}

		public void Tick(World world) { }

		public IEnumerable<IRenderable> Render(WorldRenderer wr, World world)
		{
			return [];
		}

		public IEnumerable<IRenderable> RenderAboveShroud(WorldRenderer wr, World world)
		{
			return [];
		}

		public IEnumerable<IRenderable> RenderAnnotations(WorldRenderer wr, World world)
		{
			return [];
		}

		public string GetCursor(World world, CPos cell, int2 worldPixel, MouseInput mi)
		{
			return "default";
		}

		public void Deactivate() { }

		public bool HandleKeyPress(KeyInput e)
		{
			return false;
		}

		public void SelectionChanged(World world, IEnumerable<Actor> selected) { }
	}
}