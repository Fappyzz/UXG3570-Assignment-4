using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

public class StandingUnit : MonoBehaviour
{
    static public bool isDragging = false;

    Vector3 originalPosition;
    [SerializeField] LayerMask deployZone;
    [SerializeField] LayerMask benchZone;

    StandingUnitDisplay standingUnitDisplay;

    public bool benched = true;

    void OnMouseDown()
    {
        isDragging = true;
        originalPosition = transform.position;
    }

    void OnMouseDrag()
    {
        Vector3 mousePosScreen = Input.mousePosition;

        // Create a ray from the camera to the mouse position
        Ray ray = Camera.main.ScreenPointToRay(mousePosScreen);

        // Calculate the intersection point of the ray with the Y=0 plane
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;
        if (groundPlane.Raycast(ray, out rayDistance))
        {
            // Get the point on the plane where the ray intersects
            Vector3 hitPoint = ray.GetPoint(rayDistance);

            // Maintain the Y-axis position
            hitPoint.y = transform.position.y;

            // Move the object to the intersection point
            transform.position = hitPoint;
        }
    }
    void OnMouseUp()
    {

        isDragging = false;

        // Raycast to check if the mouse is over a platform
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, benchZone))
        {
            StandingUnitDisplay hitStandingUnitDisplay = hit.collider.GetComponent<StandingUnitDisplay>();

            if (benched)
            {
                if (hitStandingUnitDisplay != this.standingUnitDisplay && hitStandingUnitDisplay.hasUnit == false)
                {
                    hitStandingUnitDisplay.ResetUnitDisplay();
                    hitStandingUnitDisplay.SetUnitData(standingUnitDisplay.unitData);
                    hitStandingUnitDisplay.UpdateUnitDisplay(true);
                    standingUnitDisplay.ResetUnitDisplay();
                }
                else if (hitStandingUnitDisplay != this.standingUnitDisplay && hitStandingUnitDisplay.hasUnit == true)
                {
                    UnitData hitUnitData = hitStandingUnitDisplay.unitData;
                    StandingUnit hitBenchUnit = hitStandingUnitDisplay.spawnedUnit.GetComponent<StandingUnit>();

                    hitStandingUnitDisplay.ResetUnitDisplay();
                    hitStandingUnitDisplay.SetUnitData(standingUnitDisplay.unitData);
                    hitStandingUnitDisplay.UpdateUnitDisplay(true);
                    standingUnitDisplay.ResetUnitDisplay();
                    standingUnitDisplay.SetUnitData(hitUnitData);
                    standingUnitDisplay.UpdateUnitDisplay(true);
                }
                else
                {
                    transform.position = originalPosition;
                }
            }
            else
            {
                if (hitStandingUnitDisplay != this.standingUnitDisplay && hitStandingUnitDisplay.hasUnit == false)
                {
                    hitStandingUnitDisplay.ResetUnitDisplay();
                    hitStandingUnitDisplay.SetUnitData(standingUnitDisplay.unitData);
                    hitStandingUnitDisplay.UpdateUnitDisplay(true);
                    hitStandingUnitDisplay.unitMan.BenchUnit(hitStandingUnitDisplay.unitData);
                    standingUnitDisplay.ResetUnitDisplay();
                }
                else if (hitStandingUnitDisplay != this.standingUnitDisplay && hitStandingUnitDisplay.hasUnit == true)
                {
                    UnitData hitUnitData = hitStandingUnitDisplay.unitData;
                    StandingUnit hitBenchUnit = hitStandingUnitDisplay.spawnedUnit.GetComponent<StandingUnit>();

                    hitStandingUnitDisplay.ResetUnitDisplay();
                    hitStandingUnitDisplay.SetUnitData(standingUnitDisplay.unitData);
                    hitStandingUnitDisplay.UpdateUnitDisplay(true);
                    hitStandingUnitDisplay.unitMan.BenchUnit(hitStandingUnitDisplay.unitData);
                    standingUnitDisplay.ResetUnitDisplay();
                    standingUnitDisplay.SetUnitData(hitUnitData);
                    standingUnitDisplay.UpdateUnitDisplay(false);
                    standingUnitDisplay.unitMan.DeployUnit(hitUnitData);
                }
                else
                {
                    transform.position = originalPosition;
                }
            }
        }
        else if (Physics.Raycast(ray, out hit, Mathf.Infinity, deployZone))
        {
            StandingUnitDisplay hitStandingUnitDisplay = hit.collider.GetComponent<StandingUnitDisplay>();

            if (benched)
            {
                if (hitStandingUnitDisplay != this.standingUnitDisplay && hitStandingUnitDisplay.hasUnit == false && UnitManager.currentUnits < UnitManager.maxUnits)
                {
                    hitStandingUnitDisplay.ResetUnitDisplay();
                    hitStandingUnitDisplay.SetUnitData(standingUnitDisplay.unitData);
                    hitStandingUnitDisplay.UpdateUnitDisplay(false);
                    hitStandingUnitDisplay.unitMan.DeployUnit(hitStandingUnitDisplay.unitData);
                    standingUnitDisplay.ResetUnitDisplay();
                }
                else if (hitStandingUnitDisplay != this.standingUnitDisplay && hitStandingUnitDisplay.hasUnit == true)
                {
                    UnitData hitUnitData = hitStandingUnitDisplay.unitData;
                    StandingUnit hitBenchUnit = hitStandingUnitDisplay.spawnedUnit.GetComponent<StandingUnit>();

                    hitStandingUnitDisplay.ResetUnitDisplay();
                    hitStandingUnitDisplay.SetUnitData(standingUnitDisplay.unitData);
                    hitStandingUnitDisplay.UpdateUnitDisplay(false);
                    hitStandingUnitDisplay.unitMan.DeployUnit(hitStandingUnitDisplay.unitData);
                    standingUnitDisplay.ResetUnitDisplay();
                    standingUnitDisplay.SetUnitData(hitUnitData);
                    standingUnitDisplay.UpdateUnitDisplay(true);
                    standingUnitDisplay.unitMan.BenchUnit(hitUnitData);
                }
                else
                {
                    transform.position = originalPosition;
                }
            }
            else
            {
                if (hitStandingUnitDisplay != this.standingUnitDisplay && hitStandingUnitDisplay.hasUnit == false)
                {
                    hitStandingUnitDisplay.ResetUnitDisplay();
                    hitStandingUnitDisplay.SetUnitData(standingUnitDisplay.unitData);
                    hitStandingUnitDisplay.UpdateUnitDisplay(false);
                    standingUnitDisplay.ResetUnitDisplay();
                }
                else if (hitStandingUnitDisplay != this.standingUnitDisplay && hitStandingUnitDisplay.hasUnit == true)
                {
                    UnitData hitUnitData = hitStandingUnitDisplay.unitData;
                    StandingUnit hitBenchUnit = hitStandingUnitDisplay.spawnedUnit.GetComponent<StandingUnit>();

                    hitStandingUnitDisplay.ResetUnitDisplay();
                    hitStandingUnitDisplay.SetUnitData(standingUnitDisplay.unitData);
                    hitStandingUnitDisplay.UpdateUnitDisplay(false);
                    standingUnitDisplay.ResetUnitDisplay();
                    standingUnitDisplay.SetUnitData(hitUnitData);
                    standingUnitDisplay.UpdateUnitDisplay(true);
                }
                else
                {
                    transform.position = originalPosition;
                }
            }
        }
        else
        {
            transform.position = originalPosition;
        }

    }

    public void SetStandingUnitDisplay(StandingUnitDisplay standingUnitDisplay)
    {
        this.standingUnitDisplay = standingUnitDisplay;
    }
}
