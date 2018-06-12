using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour 
{
	
	public int fieldX, fieldY;
	public Transform parentCells;

	public Sprite[] cellsSprite = new Sprite[2];
	public GameObject cellPrefab;

	public List<GameObject> wayPoints = new List<GameObject>();
	GameObject[ , ] allCells = new GameObject[10, 24];
	int currWayX, currWayY;
	GameObject firstCell;

	string[] path = {
		"001000000000000000000000",
		"001000000000001111100000",
		"001000111110001000100000",
		"001000100010001000100000",
		"001000100010001000100000",
		"001000100010001000100000",
		"001000100010001000111111",
		"001111100010001000000000",
		"000000000011111000000000",
		"000000000000000000000000"
	};


	void Start()
	{
		CreateLevel ();
		LoadWayPoints ();
	}

	void CreateLevel()
	{
		Vector3 WorldCellPos = Camera.main.ScreenToWorldPoint (new Vector3 (0, Screen.height, 0));

		for (int x = 0; x < fieldX; x++)
			for (int y = 0; y < fieldY; y++) {

				int spriteIndex = int.Parse (path[y].ToCharArray()[x].ToString());
				Sprite spr = cellsSprite [spriteIndex];
				bool isGround = spr == cellsSprite [1] ? true : false;

				CreateCells (isGround, spr,x, y, WorldCellPos);

			}
	}

	void CreateCells(bool isGround, Sprite spr, int x, int y, Vector3 WorldCellsPos)
	{
		GameObject tempCell = Instantiate (cellPrefab);
		tempCell.transform.SetParent (parentCells, false);
		tempCell.GetComponent<SpriteRenderer> ().sprite = spr;

		float spriteSizeX = tempCell.GetComponent<SpriteRenderer> ().bounds.size.x;
		float spriteSizeY = tempCell.GetComponent<SpriteRenderer> ().bounds.size.y;

		tempCell.transform.position = new Vector3 (WorldCellsPos.x + (spriteSizeX * x), WorldCellsPos.y + (spriteSizeY * -y));

		if (isGround) {
			tempCell.GetComponent<CellsScr> ().isGround = true;
			if (firstCell == null) {
				firstCell = tempCell;
				currWayX = x;
				currWayY = y;
			}
		}
		allCells [y, x] = tempCell;
	}

	void LoadWayPoints()
	{
		GameObject currWayGo;
		wayPoints.Add (firstCell);

		while (true) {
			
			currWayGo = null;

			if (currWayX > 0 && allCells [currWayY, currWayX - 1].GetComponent<CellsScr> ().isGround &&
			    !wayPoints.Exists (x => x == allCells [currWayY, currWayX - 1])) {
				currWayGo = allCells [currWayY, currWayX - 1];
				currWayX--;
			} else if (currWayX < (fieldX - 1) && allCells [currWayY, currWayX + 1].GetComponent<CellsScr> ().isGround &&
			         !wayPoints.Exists (x => x == allCells [currWayY, currWayX + 1])) {
				currWayGo = allCells [currWayY, currWayX + 1];
				currWayX++;
			} else if (currWayY > 0 && allCells [currWayY - 1, currWayX].GetComponent<CellsScr> ().isGround &&
			         !wayPoints.Exists (x => x == allCells [currWayY - 1, currWayX])) {
				currWayGo = allCells [currWayY - 1, currWayX];
				currWayY--;
			} else if (currWayY < (fieldY) && allCells [currWayY + 1, currWayX].GetComponent<CellsScr> ().isGround &&
			         !wayPoints.Exists (x => x == allCells [currWayY + 1, currWayX])) {
				currWayGo = allCells [currWayY + 1, currWayX];
				currWayY++;
			} else
				break;

			wayPoints.Add (currWayGo);
		}
	}
}
