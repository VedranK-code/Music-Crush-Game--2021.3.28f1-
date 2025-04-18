/*
 * Created on 2023
 *
 * Copyright (c) 2023 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */

using System.Collections;
using UnityEngine;

public class BoosterHBomb : Booster
{
	protected override void Start()
	{
		base.Start();
	}

	public override void ForceStart()
	{
		boosterType = BoosterType.HBomb;
		base.ForceStart();
	}

	public override void UseBooster(bool isTutorial = false)
	{
		if (CheckEnableBooster(isTutorial))
		{
			base.UseBooster(isTutorial);
			selectEffect.SetActive(value: true);
			guide.gameObject.SetActive(value: true);
			guide.TurnOnOnlyOneBoosterUI(uiIndex);
			guide.textGuide.text = "Touch to launch the rocket horizontally from the touch point.";
			if ((bool)guide)
			{
				guide.SetIconImage(boosterType);
			}
			if (onSelect)
			{
				CancelBooster();
				return;
			}
			SoundSFX.Play(SFXIndex.GameItemButtonClickCandyBomb);
			StartCoroutine(UseCandyPack());
		}
	}

	public override void CancelBooster()
	{
		base.CancelBooster();
		ControlAssistant.main.ReleasePressedChip();
		onSelect = false;
		selectEffect.SetActive(value: false);
		guide.gameObject.SetActive(value: false);
	}

	private IEnumerator UseCandyPack()
	{
		onSelect = true;
		yield return StartCoroutine(Utils.WaitFor(GameMain.main.CanIWait, 0.1f));
		Slot targetSlot = null;
		while (onSelect)
		{
			if (!GameMain.main.CanIWait())
			{
				CancelBooster();
			}
			if (GameMain.main.CurrentTurn == VSTurn.CPU)
			{
				CancelBooster();
			}
			if (Input.GetMouseButtonDown(0) || (UnityEngine.Input.touchCount > 0 && UnityEngine.Input.GetTouch(0).phase == TouchPhase.Ended))
			{
				targetSlot = ControlAssistant.main.GetSlotFromTouch();
			}
			if (targetSlot != null)
			{
				if (CheckCanCrush(targetSlot))
				{
					GameMain.main.isPlaying = false;
					selectEffect.SetActive(value: false);
					guide.gameObject.SetActive(value: false);
					SoundSFX.Play(SFXIndex.GameItemUseHammer);
					Chip chip = null;
					Chip chip2 = null;
					int chipID = 0;
					if (targetSlot.GetBlock() == null)
					{
						if (targetSlot.GetChip() != null)
						{
							chipID = targetSlot.GetChip().id;
							if (targetSlot.GetChip().chipType == ChipType.BringDown)
							{
								chip2 = targetSlot.GetChip();
							}
							else
							{
								targetSlot.GetChip().DestroyChip();
							}
						}
					}
					else if (targetSlot.GetBlock() != null)
					{
						BoardManager.main.BlockCrush(targetSlot.x, targetSlot.y, radius: false);
						if (targetSlot.GetChip() != null)
						{
							chip = targetSlot.GetChip();
						}
					}
					Transform transform = SpawnStringBlock.GetSpawnBlockObjectHBomb(chipID).transform;
					ColorHBomb component = transform.GetComponent<ColorHBomb>();
					targetSlot.SetChip(component);
					component.transform.localPosition = Vector3.zero;
					component.DestroyChip();
					if ((bool)chip)
					{
						targetSlot.SetChip(chip);
					}
					else if ((bool)chip2)
					{
						targetSlot.SetChip(chip2);
					}
					CompleteUseBooster();
					ControlAssistant.main.ReleasePressedChip();
					GameMain.main.isPlaying = true;
					GameMain.main.TurnEndAfterUsingBooster();
					break;
				}
				StartCoroutine(EffectSelectFail(targetSlot.transform));
				targetSlot = null;
				ControlAssistant.main.ReleasePressedChip();
				yield return null;
			}
			yield return 0;
		}
		ControlAssistant.main.ReleasePressedChip();
	}

	private bool CheckCanCrush(Slot targetSlot)
	{
		if (!targetSlot)
		{
			return false;
		}
		if (!targetSlot.canBeCrush)
		{
			return false;
		}
		if (targetSlot.GetBlock() != null)
		{
			return targetSlot.GetBlock().EnableBoosterCandyPack;
		}
		if (targetSlot.GetChip() != null)
		{
			if (targetSlot.GetChip().chipType == ChipType.BringDown)
			{
				return false;
			}
			if (targetSlot.GetChip().chipType == ChipType.CandyChip)
			{
				return false;
			}
			if (targetSlot.GetChip().chipType == ChipType.SimpleChip)
			{
				return true;
			}
			return false;
		}
		return false;
	}
}
