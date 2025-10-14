using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float fireRate = 1f;
    public int maxAmmo = 6;
    public float reloadTime = 3f;

    private int currentAmmo;
    private bool isReloading = false;
    private float nextFire;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (isReloading) return;

        Move();

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (Input.GetMouseButton(0) && Time.time > nextFire && currentAmmo > 0)
        {
            Shoot();
            nextFire = Time.time + 1f / fireRate;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
        }
    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 moveDir = (transform.forward * v + transform.right * h).normalized;
        Vector3 newPos = transform.position + moveDir * moveSpeed * Time.deltaTime;
        rb.MovePosition(newPos);
    }

    void Shoot()
    {
        Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        currentAmmo--;
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Recargando...");
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
        Debug.Log("Recarga completa.");
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}
