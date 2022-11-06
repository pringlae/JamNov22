using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPiecesInOrder : Minigame
{
    [SerializeField] private List<Item> itemsToOrder = new List<Item>();
    private static SpriteRenderer _greySprite = null;

    private Dictionary<Item, SpriteRenderer> _places = new Dictionary<Item, SpriteRenderer>();
    private bool _solvable = true;

    protected override void Awake()
    {
        base.Awake();
        Camera.main.orthographicSize = 5;

        if (_greySprite == null)
            _greySprite = Resources.Load<SpriteRenderer>("GreySprite");

        var itemsOnLocation = Map.Instance.CurrentLocation.GetComponentsInChildren<Item>();
        foreach (var item in itemsToOrder)
        {
            bool found = false;
            foreach (var itemOnLoc in itemsOnLocation)
            {
                if (itemOnLoc.Id == item.Id)
                {
                    found = true;
                    break;
                }
            }
            if (found)
            {
                var greyCopy = Object.Instantiate(_greySprite, transform);
                greyCopy.transform.position = item.transform.position;
                _greySprite.sprite = item.Sprite;
                item.transform.position = new Vector3(Random.value * 10 - 5, Random.value * 10 - 5, 0);
                _places[item] = greyCopy;
            }
            else
            {
                item.gameObject.SetActive(false);
                _solvable = false;
            }
        }
    }

    public override void Close()
    {
        if (_solvable)
        {
            bool everythingInPlace = true;
            foreach (var entry in _places)
                if (Vector3.Distance(entry.Key.transform.position, entry.Value.transform.position) > 1)
                {
                    everythingInPlace = false;
                    break;
                }
            _success = everythingInPlace;
        }
    }
}
