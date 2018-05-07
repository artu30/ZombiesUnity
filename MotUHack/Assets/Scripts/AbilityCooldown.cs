using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AbilityCooldown : MonoBehaviour {

	[HideInInspector] public Ability ability;
	private float coolDownDuration;
	private float nextReadyTime;
	private float coolDownTimeLeft;
	public bool onCooldown = false;

	void Start () 
	{
		Initialize (ability); 
	}

	public void Initialize(Ability selectedAbility)
	{
		if (selectedAbility != null) {
			ability = selectedAbility;
			AbilityReady ();
		}

	}

	// Update is called once per frame
	void Update () 
	{
		if (ability != null) {
			bool coolDownComplete = (Time.time > nextReadyTime);
			if (coolDownComplete) 
			{
				AbilityReady ();
			} else 
			{
				CoolDown();
			}
		}

	}

	private void AbilityReady()
	{
		onCooldown = false;
	}

	private void CoolDown()
	{
		coolDownTimeLeft -= Time.deltaTime;
		float roundedCd = Mathf.Round (coolDownTimeLeft);
	}

	public void RunAbility()
	{
		coolDownDuration = ability.cooldown;
		if (!onCooldown) {
			onCooldown = true;
			nextReadyTime = coolDownDuration + Time.time;
			coolDownTimeLeft = coolDownDuration;
			ability.TriggerAbility (transform);
		}

	}
}
