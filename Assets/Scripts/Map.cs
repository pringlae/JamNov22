using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Map : MonoBehaviour
{
    public static Map Instance;
    [SerializeField] private Location _startLocation;
    [SerializeField] private int _maxLocationsLoaded;
    [SerializeField] private TextMeshProUGUI _locationNameText;
    [SerializeField] private Fader _fader;

    private List<Location> _loadedLocations = new List<Location>();
    private bool _inTransition = false;

    public Location CurrentLocation;

    private void Awake()
    {
        Instance = this;
        _fader.SetActive(true);
    }

    private void Start()
    {
        OpenLocation(_startLocation, 0);
    }

    public void OpenLocation(Location location, int spawnPointIndex) => StartCoroutine(OpenLocationImpl(location, spawnPointIndex));

    protected IEnumerator OpenLocationImpl(Location location, int spawnPointIndex)
    {
        if (_inTransition) yield break;

        _inTransition = true;
        Player.instance.enabled = false;
        yield return StartCoroutine(_fader.FadeIn());

        Location locationPrefab = _loadedLocations.Find((loc) => loc.Id == location.Id);
        if (locationPrefab != null)
            _loadedLocations.Remove(locationPrefab);
        else
        {
            while (_loadedLocations.Count >= _maxLocationsLoaded)
            {
                Destroy(_loadedLocations[0].gameObject);
                _loadedLocations.RemoveAt(0);
            }
            locationPrefab = Object.Instantiate(location, transform);
        }
        _loadedLocations.Add(locationPrefab);
        CurrentLocation = locationPrefab;

        _locationNameText.text = locationPrefab.VisibleName;

        foreach (var locPrefab in _loadedLocations)
            locPrefab.gameObject.SetActive(false);
        locationPrefab.gameObject.SetActive(true);

        Player.instance.transform.position = locationPrefab.SpawnPoints[spawnPointIndex].transform.position;

        yield return _fader.FadeOut();

        Player.instance.enabled = true;
        _inTransition = false;
    }
}
