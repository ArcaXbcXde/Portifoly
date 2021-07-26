using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Vanish : MonoBehaviour {

	// 0 to 100 to just keep nice round numbers in the inspector
	[Tooltip ("The transparency of this entity after entering in contact with the player")]
	[Range (0.0f, 100.0f)]
	public float transparency = 0.0f;

	[Tooltip ("The percentage which the entity vanish in or out in a second")]
	[Range (0.0f, 100.0f)]
	public float vanishSpeed = 10.0f;
	
	// The tilemap reference this component is attached to and its child
	private Tilemap tilemapComponent;
	private Tilemap childTilemapComponent;

	// Starting colors for the selected tilemap and its child
	private Color tilemapColor;
	private Color childTilemapColor;

	// Basic awake, mainly to start variables
	private void Awake () {
		
		tilemapComponent = GetComponent<Tilemap>();
		tilemapColor = new Color (tilemapComponent.color.r, tilemapComponent.color.g, tilemapComponent.color.b, tilemapComponent.color.a);

		childTilemapComponent = transform.GetChild(0).GetComponent<Tilemap>();
		childTilemapColor = new Color (childTilemapComponent.color.r, childTilemapComponent.color.g, childTilemapComponent.color.b, childTilemapComponent.color.a);

		// The transparency and vanishSpeed variables are between 0 and 100, but percentage is between 0 and 1
		transparency /= 100;
		vanishSpeed /= 100;
	}

	// If the player touched the collider of the tilemap this component is attached to, it starts to get invisible
	// StopCoroutine is here in case the player gets in and out too quickly, then it makes a sweet fade-out effect after interrupting the fade-in.
	private void OnTriggerEnter2D (Collider2D col) {
		
		if (col.tag == "Player") {

			StopCoroutine (Retransparence(tilemapColor.a));
			StartCoroutine (Retransparence(transparency));
		}
	}

	// If the player left the collider of the tilemap this component is attached to, it starts to get visible again
	// StopCoroutine is here in case the player gets out and in too quickly, then it makes a sweet fade-in effect after interrupting the fade-out.
	private void OnTriggerExit2D (Collider2D col) {

		if (col.tag == "Player") {
			if (col.GetComponent<HealthControl>().Dead == false) {

				StopCoroutine (Retransparence(transparency));
				StartCoroutine (Retransparence(tilemapColor.a));
			}
		}
	}

	/// <summary>
	/// Repaints the alpha value from the colors of this component's tilemap and its child to the "targetAlpha" value
	/// </summary>
	/// <param name="targetAlpha">The alpha values the colors of this component's tilemap and its child are supposed to assume</param>
	/// <returns></returns>
	private IEnumerator Retransparence (float targetAlpha) {

		float currentAlpha = tilemapComponent.color.a;
		
		while (currentAlpha != targetAlpha) {
			
			if (currentAlpha > targetAlpha) {
				
				currentAlpha -= vanishSpeed;

				if (currentAlpha <= targetAlpha) {
					
					break;
				}
			} else {
				
				currentAlpha += vanishSpeed;

				if (currentAlpha >= targetAlpha) {
					
					break;
				}
			}

			tilemapComponent.color = new Color(tilemapColor.r, tilemapColor.g, tilemapColor.b, currentAlpha);
			childTilemapComponent.color = new Color(childTilemapColor.r, childTilemapColor.g, childTilemapColor.b, currentAlpha);
			
			yield return new WaitForSeconds (Time.deltaTime);
		}

		currentAlpha = targetAlpha;

		tilemapComponent.color = new Color(tilemapColor.r, tilemapColor.g, tilemapColor.b, currentAlpha);
		childTilemapComponent.color = new Color(childTilemapColor.r, childTilemapColor.g, childTilemapColor.b, currentAlpha);
	}
}