using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneController : MonoBehaviour
{
	//public static int gridRows = Dropdown.row;
	//public static int gridCols = Dropdown.col;
	public static int gridRows;
	public static int gridCols;
	[SerializeField] private MemoryCard originalCard;
	[SerializeField] private Sprite[] images;
	[SerializeField] private TextMeshProUGUI scoreLabel;

	private MemoryCard _firstRevealed;
	private MemoryCard _secondRevealed;
	private int _score = 0;

	public bool canReveal
	{
		get { return _secondRevealed == null; }
	}

	// Use this for initialization
	void Start()
	{
		gridRows = Dropdown.row;
		gridCols = Dropdown.col;
		Vector3 startPos = originalCard.transform.position;

		float aspect = (float)Screen.width / Screen.height;
		float worldHeight = Camera.main.orthographicSize * 1.5f;
		float worldWidth = worldHeight * aspect * 0.6f;

		float offsetX = worldWidth / gridCols;
		float offsetY = worldHeight / gridRows;

		// create shuffled list of cards
		int[] numbers = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

		List<int> rand = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
		int[] deck = new int[gridRows * gridCols];
		for (int i = 0; i < deck.Length / 2; i++)
		{
			int r = Random.Range(0, rand.Count - 1);
			deck[i] = rand[r];
			deck[deck.Length - 1 - i] = rand[r];
			rand.RemoveAt(r);
		}

		numbers = ShuffleArray(deck);

		// place cards in a grid
		for (int i = 0; i < gridCols; i++)
		{
			for (int j = 0; j < gridRows; j++)
			{
				MemoryCard card;

				// use the original for the first grid space
				if (i == 0 && j == 0)
				{
					card = originalCard;
				}
				else
				{
					card = Instantiate(originalCard) as MemoryCard;
				}

				// next card in the list for each grid space
				int index = j * gridCols + i;
				int id = numbers[index];
				card.SetCard(id, images[id]);

				float posX = (offsetX * i) + startPos.x;
				float posY = -(offsetY * j) + startPos.y;
				card.transform.position = new Vector3(posX, posY, startPos.z);
			}
		}
	}

	// Knuth shuffle algorithm
	private int[] ShuffleArray(int[] numbers)
	{
		int[] newArray = numbers.Clone() as int[];
		for (int i = 0; i < newArray.Length; i++)
		{
			int tmp = newArray[i];
			int r = Random.Range(i, newArray.Length);
			newArray[i] = newArray[r];
			newArray[r] = tmp;
		}
		return newArray;
	}

	public void CardRevealed(MemoryCard card)
	{
		if (_firstRevealed == null)
		{
			_firstRevealed = card;
		}
		else
		{
			_secondRevealed = card;
			StartCoroutine(CheckMatch());
			if (_score == (gridCols * gridRows) / 2) {
				ChangeToCongrats();
				//ChangeToStart();
			}
			
		}
	}

	private IEnumerator CheckMatch()
	{

		// increment score if the cards match
		if (_firstRevealed.id == _secondRevealed.id)
		{
			_score++;
			scoreLabel.text = "Score: " + _score;
			_firstRevealed.ShakeMe();
			_secondRevealed.ShakeMe();
		}

		// otherwise turn them back over after .5s pause
		else
		{
			yield return new WaitForSeconds(.5f);

			_firstRevealed.Unreveal();
			_secondRevealed.Unreveal();
		}

		_firstRevealed = null;
		_secondRevealed = null;
	}

	/* public void ChangeToStart()
    {
        StartCoroutine(Again());
    } */

	public void ChangeToCongrats()
    {
        StartCoroutine(Congratulation());
    }

	private IEnumerator Congratulation() {
		yield return new WaitForSeconds(1.0f);
		SceneManager.LoadScene("Congrats");
	}

	/* private IEnumerator Again() {
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene("Start");
		
	} */

	public void Restart()
	{

		//Application.LoadLevel("Scene"); /* obsolete since Unity 2017 */

		SceneManager.LoadScene("Scene");
	}
}
