using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using WI;

public class LocationMarkerManager : MonoBehaviour
{
    public GameObject earth;
    public Icon_Location prefab_MarkIcon;
    private List<LocationMarker> locationMarkers = new();
    private OrbitalController orbitalController;

    private void Awake()
    {
        locationMarkers = FindObjectsOfType<LocationMarker>().ToList();
        orbitalController = FindObjectOfType<OrbitalController>();

        foreach(var locationMarker in locationMarkers)
        {
            var locationMarkerIcon = Instantiate(prefab_MarkIcon, transform);
            locationMarker.SetMarkerIcon(locationMarkerIcon);
        }
    }

    private void Update()
    {
        SettingPos();
        HideMarker();
    }

    void SettingPos()
    {
        foreach (var locationMarker in locationMarkers)
        {
            Vector3 screenPos = orbitalController.camera.WorldToScreenPoint(locationMarker.centerPos);
            if (screenPos.z > 0)
            {
                locationMarker.markerIcon.transform.position = screenPos;
            }
        }
    }
    void HideMarker()
    {
        foreach(var locationMarker in locationMarkers)
        {
            var markerDistance = Vector3.Distance(locationMarker.centerPos, orbitalController.camera.transform.position);
            var earthDistance = Vector3.Distance(earth.transform.position, orbitalController.camera.transform.position);

            if (markerDistance > earthDistance)
            {
                locationMarker.DeactiveMarkerIcon();
            }
            else
            {
                locationMarker.ActiveMarkerIcon();
            }
        }
    }
}
