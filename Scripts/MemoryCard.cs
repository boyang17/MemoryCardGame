using UnityEngine;
using System.Collections;

public class MemoryCard : MonoBehaviour
{
	[SerializeField] private GameObject cardBack;
	[SerializeField] private SceneController controller;
	[SerializeField] ParticleSystem smoke = null;
	public float shakeAmount = 0.1f;
	public float shakeDuration = 0.5f;
	
	private Vector3 initialPosition;
	private float shakeTimer = 0f;

	private int _id;
	public int id
	{
		get { return _id; }
	}

	public void SetCard(int id, Sprite image)
	{
		_id = id;
		GetComponent<SpriteRenderer>().sprite = image;
	}

	public void OnMouseDown()
	{
		if (cardBack.activeSelf && controller.canReveal)
		{
			cardBack.SetActive(false);
			controller.CardRevealed(this);
		}
	}

	public void Unreveal()
	{
		cardBack.SetActive(true);
	}

	void Start()
	{
		initialPosition = transform.position;
	}

	void Update()
	{
		StartCoroutine(Shake());
	}

	IEnumerator Shake()
	{
		if (shakeTimer > 0)
		{
			Smoke();
			Vector3 randomDisplacement = Random.insideUnitSphere * shakeAmount;
			randomDisplacement.z = 0f;
			transform.position = initialPosition + randomDisplacement;
			shakeTimer -= Time.deltaTime;
			yield return new WaitForSeconds(0.5f);
			gameObject.SetActive(false);
		}
		else
		{
			transform.position = initialPosition;
		}

	}

	public void ShakeMe()
	{
		shakeTimer = shakeDuration;
	}

	public void Smoke() {
		smoke.Play();
	}
}
