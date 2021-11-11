using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Sprites;
using UnityEngine.UI;
public class MenuDragDropTower : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	
	public towerScript tower;
	public GameObject objmanager;
	public bool isDraggable = true;

	private GameObject dragged;
	private gameManager manager;
	void Start () {
		manager = objmanager.GetComponent<MenuButtons> ().manager;
	}
	void Update () {
		if (manager.playerEnergy - tower.energy < 0) {
			isDraggable = false;
			GetComponent<Image> ().color = Color.gray;
		} else {
			isDraggable = true;
			GetComponent<Image> ().color = Color.white;
		}
	}

	public void OnBeginDrag (PointerEventData eventData) {
		if (isDraggable) dragged = Instantiate (gameObject, transform);
	}

	public void OnDrag (PointerEventData eventData) {
		if (isDraggable)
			dragged.transform.position = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10f));
	}

	public void OnEndDrag (PointerEventData eventData) {
		if (isDraggable) {
			if (dragged != null) {
				RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
				if (hit && hit.collider.transform.tag == "empty") {
					manager.playerEnergy -= tower.energy;
					Instantiate (tower, hit.collider.gameObject.transform.position, Quaternion.identity);
				}
				Destroy (dragged);
				dragged = null;
			}
		}
	}
}
