using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActivationMethod {

	Simple,
	SingleActivation,
	SingleDeactivation,
	AutoActivate,
	AutoDeactivate,
	OnlyActivations,
	OnlyDeactivations,
}
public abstract class Mechanicals : MonoBehaviour {
	
	[Tooltip("Time untill the activation of this mechanism after interacted")]
	[Range(0.0f, 60.0f)]
	public float timeUntilActivate = 0.0f;

	[Tooltip ("Time untill the deactivation of this mechanism after interacted")]
	[Range (0.0f, 60.0f)]
	public float timeUntilDeactivate = 0.0f;

	[Tooltip ("The way this mechanism will be activated or deactivated\n" +
		"\n- Simple -> Toggled however you want;" +
		"\n- Single Activation -> Only activates and only once;" +
		"\n- Single Deactivation -> Only deactivates and only once;" +
		"\n- Auto Activate -> Automatically reactivates after a certain time;" +
		"\n- Auto Deactivate -> Automatically deactivates after a certain time;" +
		"\n- Only Activations -> Will only activate its mechanism (can be activated repeteadly);" +
		"\n- Only Deactivations -> Will only deactivate its mechanism (can be deactivated repeteadly).")]
	public ActivationMethod activationMethod = ActivationMethod.Simple;

	[Tooltip ("Other mechanisms that will be activated by this mechanism, if any")]
	public Mechanicals[] objectsToActivate = new Mechanicals[1];

	public bool isActive = false;

	public bool isLocked = false;

	// Activates the mechanism according to the set "activationMethod" enum
	public virtual void Activate () {
		
		if (activationMethod != ActivationMethod.SingleActivation && activationMethod != ActivationMethod.SingleDeactivation) {

			isActive = true;
		}
		
		switch (activationMethod) {
			case ActivationMethod.Simple:

				Invoke("OnActivate", timeUntilActivate);
				break;
			case ActivationMethod.SingleActivation:

				if (isActive == false) {

					Invoke("OnActivate", timeUntilActivate);
					isActive = true;
				}
				break;
			case ActivationMethod.SingleDeactivation:

				if (isActive == false) {

					Invoke("OnDeactivate", timeUntilDeactivate);
					isActive = true;
				}
				break;
			case ActivationMethod.AutoActivate:

				Invoke("OnActivate", timeUntilActivate);
				break;
			case ActivationMethod.AutoDeactivate:

				Invoke("OnActivate", timeUntilActivate);
				Invoke("OnDeactivate", timeUntilDeactivate);
				break;
			case ActivationMethod.OnlyActivations:

				Invoke ("OnActivate", timeUntilActivate);
				break;
			case ActivationMethod.OnlyDeactivations:

				Invoke("OnDeactivate", timeUntilDeactivate);
				break;
		}
	}

	// Deactivates the mechanism according to the set "activationMethod" enum
	public virtual void Deactivate () {

		if (activationMethod != ActivationMethod.SingleActivation && activationMethod != ActivationMethod.SingleDeactivation) {

			isActive = false;
		}

		switch (activationMethod) {
			case ActivationMethod.Simple:

				Invoke("OnDeactivate", timeUntilDeactivate);
				break;
			case ActivationMethod.AutoActivate:

				Invoke("OnDeactivate", timeUntilDeactivate);
				Invoke("OnActivate", timeUntilActivate);
				break;
			case ActivationMethod.AutoDeactivate:
				
				Invoke("OnDeactivate", timeUntilDeactivate);
				break;
			case ActivationMethod.OnlyActivations:

				Invoke("OnActivate", timeUntilActivate);
				break;
			case ActivationMethod.OnlyDeactivations:

				Invoke("OnDeactivate", timeUntilDeactivate);
				break;
		}
	}

	// Every mechanism must have an activation and an deactivation methods, even if not used to avoid null calls
	protected abstract void OnActivate ();
	
	protected abstract void OnDeactivate ();
}