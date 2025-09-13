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

using OpenRA.Mods.Common.Traits;
using OpenRA.Widgets;

namespace OpenRA.Mods.Common.Widgets.Logic
{
	public class IdleGameWidget : ChromeLogic
	{
		readonly Traits.IdleGameLogic idleLogic;
		readonly LabelWidget resourcesLabel;
		readonly LabelWidget generatorCountLabel;
		readonly ButtonWidget buyGeneratorButton;
		readonly ButtonWidget clickButton;

		[ObjectCreator.UseCtor]
		public IdleGameWidget(Widget widget, World world)
		{
			idleLogic = world.WorldActor.TraitOrDefault<Traits.IdleGameLogic>();
			
			resourcesLabel = widget.GetOrNull<LabelWidget>("RESOURCES_VALUE");
			generatorCountLabel = widget.GetOrNull<LabelWidget>("GEN1_COUNT");
			buyGeneratorButton = widget.GetOrNull<ButtonWidget>("GEN1_BUY");
			clickButton = widget.GetOrNull<ButtonWidget>("CLICK_BUTTON");

			if (buyGeneratorButton != null && idleLogic != null)
			{
				buyGeneratorButton.OnClick = () => {
					if (idleLogic.TryBuyGenerator())
					{
						Game.Sound.PlayNotification(world.Map.Rules, null, "Sounds", "CashTick", null);
					}
				};
			}

			if (clickButton != null && idleLogic != null)
			{
				clickButton.OnClick = () => {
					idleLogic.PerformClick();
					Game.Sound.PlayNotification(world.Map.Rules, null, "Sounds", "CashTick", null);
				};
			}
		}

		public override void Tick()
		{
			if (idleLogic == null)
				return;

			if (resourcesLabel != null)
				resourcesLabel.GetText = () => $"{idleLogic.Resources}";

			if (generatorCountLabel != null)
				generatorCountLabel.GetText = () => $"Count: {idleLogic.BasicGenerators}";

			if (buyGeneratorButton != null)
			{
				buyGeneratorButton.GetText = () => $"Buy ({idleLogic.BasicGeneratorCost})";
				buyGeneratorButton.IsDisabled = () => idleLogic.Resources < idleLogic.BasicGeneratorCost;
			}
		}
	}
}