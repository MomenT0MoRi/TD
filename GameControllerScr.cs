using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower
{
	public int type;
	public float coolDown;
	public float currCoolDown = 0;
	public Sprite spriteTower;

	public Tower(int type,float cd, string path)
	{
		this.type = type;
		this.coolDown = cd;
		spriteTower = Resources.Load<Sprite> (path);
	}
}

public class CoreTower
{
	public float speed;
	public int damage;
	public Sprite spriteCore;

	public CoreTower(float _speed, int _damage, string path)
	{ 
		speed = _speed;
		damage = _damage;
		spriteCore = Resources.Load<Sprite> (path);
	}
}
	
public enum TowerType
{
	IceTower,
	AreaTower 
}

public class GameControllerScr : MonoBehaviour 
{
	public List<Tower>AllTowers = new List<Tower>();
	public List<CoreTower> AllCore = new List<CoreTower>();

	private void Awake()
	{
		AllTowers.Add (new Tower ( 0, 1.0f, "TowerSprite/FTower"));
		AllTowers.Add(new Tower( 1, 1.0f, "TowerSprite/STower"));

		AllCore.Add (new CoreTower (10.0f, 5, "CoreTower/FCore"));
		AllCore.Add (new CoreTower (10.0f, 1, "CoreTower/SCore"));
	}
}
