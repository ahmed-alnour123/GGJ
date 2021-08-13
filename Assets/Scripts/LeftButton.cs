using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LeftButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
    private Player player;

    void Start() {
        player = Player.current;
    }

    public void OnPointerDown(PointerEventData eventData) {
        player.isMovingleft = true;


    }
    public void OnPointerUp(PointerEventData eventData) {
        player.isMovingleft = false;
    }

}
