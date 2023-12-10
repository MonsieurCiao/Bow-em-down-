using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bow : MonoBehaviour
{
    public Animator animator;
    public GameObject arrow;
    public Transform spawnPos;
    GameObject projectileObject;

    //rotation
    Vector2 direction;

    //charging
    float distanceReq = 17;
    float distanceDragged;
    bool isAbleToCharge;
    public GameObject strengthArrow;
    Vector3 ScaleStrengthArrow;
    float chargeTime = 0;
    public bool inputAllowed = true;

    //bow components
    public float power = 15f;
    int piercing;
    bool flame;
    bool infinity;
    bool poison;
    float stun;
    float slow;
    float chanceOfCritical;
    float criticalMultiplier;
    public float totalChargeTime = 1.5f;


    // line of shot
    public GameObject point;
    GameObject[] points;
    public int numberOfPoints;
    public float spaceBetweenPoints;


    void Awake()
    {
        isAbleToCharge = true;
        ScaleStrengthArrow = new Vector3(0.377f, 0.9527f, 1);
        strengthArrow.transform.localScale = ScaleStrengthArrow;
        strengthArrow.SetActive(false);
        animator.SetInteger("BowChargeState", 0);
    }
    private void Start()
    {
        points = new GameObject[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i] = Instantiate(point, spawnPos.position, Quaternion.identity);
        }

    }

    void Update()
    {
        // Get the mouse position in world coordinates
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Set the Z-component to 0 to ensure the correct position in 2D space
        mousePos.z = 0;

        // Calculate the direction from the bow to the mouse
        direction = mousePos - transform.position;

        // Calculate the rotation angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply the rotation to the bow
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        if (Input.GetMouseButton(0) && isAbleToCharge && inputAllowed)
        {
            
            isAbleToCharge = false;
            
            StartCoroutine(Charge());
        }
    }

    IEnumerator Charge()
    {
        float DraggedNMaxRatio = 1;
        //get the starting Position of the mouse
        Vector2 startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        //activate strength arrow
        strengthArrow.SetActive(true);
        while (Input.GetMouseButton(0))
        {
            // Get the mouse position in world coordinates
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            distanceDragged = mousePos.x - startMousePos.x + Mathf.Abs(mousePos.y - startMousePos.y);

            //chargeTime
            if (chargeTime <= totalChargeTime) chargeTime += Time.deltaTime;


            if (distanceDragged >= 0)
            {
                DraggedNMaxRatio = distanceDragged / distanceReq * (chargeTime / 2);
                if (DraggedNMaxRatio > 1) DraggedNMaxRatio = 1;
                
                if(DraggedNMaxRatio < 1.0/3)
                {
                    animator.SetInteger("BowChargeState", 1);
                    
                }
                else if(DraggedNMaxRatio < 2.0 / 3)
                {
                    animator.SetInteger("BowChargeState", 2);
                    
                }
                else
                {
                    animator.SetInteger("BowChargeState", 3);
                    
                }
                strengthArrow.transform.localScale = new Vector3(ScaleStrengthArrow.x * DraggedNMaxRatio, ScaleStrengthArrow.y * DraggedNMaxRatio, 1);
            }
            yield return null;
        }
        animator.SetInteger("BowChargeState", 0);
        chargeTime = 0;

        StartCoroutine(Shoot(DraggedNMaxRatio * power));
        strengthArrow.transform.localScale = ScaleStrengthArrow;
        strengthArrow.SetActive(false);
    }
    IEnumerator Shoot(float power)
    {
        
        projectileObject = Instantiate(arrow, spawnPos);
        Arrow arrowScript = projectileObject.GetComponent<Arrow>();

        yield return new WaitForSeconds(0.1f);
        StartCoroutine(arrowScript.StartFlying(power));

        isAbleToCharge = true;
    }

    //Vector2 PointPosition(float t)
    //{
    //    Vector2 position = (Vector2)spawnPos.position + (direction.normalized * power * t) + 0.5f * Physics2D.gravity + (t * t);
    //    return position;
    //}
}
