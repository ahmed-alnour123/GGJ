using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RightButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    private Player player;
    void Start() {
        player = Player.current;
    }
    public void OnPointerDown(PointerEventData eventData) {
        player.isMovingright = true;



    }
    public void OnPointerUp(PointerEventData eventData) {
        player.isMovingright = false;
    }

}
