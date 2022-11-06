using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorToCave : Door
{
    public override void Activate()
    {
        if (Inventory.Instance.CurrentItem == null || Inventory.Instance.CurrentItem.Id != "lamp")
            DialogueBubble.instance.Setup(Player.instance.DialoguePosition, "Там слишком темно, мне страшно", 4);
        else
            Map.Instance.OpenLocation(destination, spawnPoint);
    }
}
