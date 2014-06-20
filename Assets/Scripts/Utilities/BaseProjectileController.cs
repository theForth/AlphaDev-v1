
using UnityEngine;
using System.Collections;

public class BaseProjectileController : MonoBehaviour
{
		public bool		test = true;
	
		public GameObject projectile;
		public string	triggerAxis = "Fire1";
	
		public bool		isSemiAutomatic;
		public float	verticalSpread = 0;
		public float	horizontalSpread = 0;
		public int		projectileCount = 1;
		public float	projectileLife = 5;
		public float	rateOfFire = 1;
		public Vector3	projectileAcceleration;
		public Vector3	projectileGlobalAcceleration;
		public int		clipSize = 10;
		public float	reloadTime = 1;
	
		public float	projectileSpeed = 5;
		public float	projectileSpeedDelta = 0;
		public float	accelerationScale;
		public float 	displayScale = 1;
	
		public Vector3	displayOffset = -Vector3.one * 10 + Vector3.right * 20;
		public float	currentAmmo;

		public 	bool 	showDisplayHandles = false;
		public 	bool 	gizmosFlag = true;
	
		public bool	isReloading;
		public float	shootTimer;
		private float	reloadTimer;
		private float	lastTriggerValue;
		private bool	triggerPushed;

		public float	projectileMass = 1f;
	
		public AudioSource audioSource;
		public AudioClip shootSound;
		public AudioClip reloadingSound;
		public AudioClip reloadedSound;
		public AudioClip emptyClickSound;
	
		void Start ()
		{
				isReloading = false;
				currentAmmo = clipSize;
				shootTimer = 0;
		}
	
		void Update ()
		{
		
				/*triggerPushed = Input.GetAxis (triggerAxis) > lastTriggerValue;
				lastTriggerValue = Input.GetAxis (triggerAxis);

				if (test || Input.GetAxis (triggerAxis) > 0) {
						if (!isSemiAutomatic) {
								if (triggerPushed) {
										Shoot ();
								}
						} else {
								Shoot ();
						}
				}*/
				if (!isReloading && shootTimer > 0) {
						shootTimer -= Time.deltaTime;
				}
				

				foreach (AudioSource source in GetComponents<AudioSource>()) {
						if (!source.isPlaying) {
								Destroy (source);
						}
				}
		}
	
		public void Reload ()
		{
				if (!isReloading) {
						audioSource = gameObject.AddComponent<AudioSource> ();
						audioSource.clip = reloadingSound;
						audioSource.Play ();
						isReloading = true;
				}
				shootTimer = 0;
				reloadTimer += Time.deltaTime;
				currentAmmo = -reloadTimer / reloadTime * clipSize;
				if (currentAmmo <= -clipSize) {
						currentAmmo = clipSize;
						isReloading = false;
						reloadTimer = 0;
						shootTimer = 0;
						audioSource = gameObject.AddComponent<AudioSource> ();
						audioSource.clip = reloadedSound;
						audioSource.Play ();
				}
		}
	
		//Pull the trigger: shoots if the next projectile is ready
		public virtual void Shoot ()
		{
				if (shootTimer <= 0) {
						currentAmmo--;
						shootTimer += 1 / rateOfFire;
						Spawnprojectile ();
						audioSource = gameObject.AddComponent<AudioSource> ();
						audioSource.clip = shootSound;
						audioSource.Play ();
				}
				if (isReloading && triggerPushed && !isSemiAutomatic) {
						audioSource = gameObject.AddComponent<AudioSource> ();
						audioSource.clip = emptyClickSound;
						audioSource.Play ();
				}
		}
	
		//Force shoot: even if the projectile is not ready yet
		public virtual void Spawnprojectile ()
		{
				for (int i = 0; i < projectileCount; i++) {
						GameObject newprojectile = (GameObject)Instantiate (projectile, transform.position, transform.rotation);
						newprojectile.name = "Defaultprojectile";
						newprojectile.AddComponent<Rigidbody> ();
						newprojectile.rigidbody.mass = projectileMass;
						newprojectile.rigidbody.drag = 0;
						newprojectile.rigidbody.useGravity = false;
			
						newprojectile.rigidbody.velocity =
				 Camera.main.transform.forward * (projectileSpeed + projectileSpeedDelta * (Random.value - 0.5f) * 2)
								+ Camera.main.transform.up * (verticalSpread * (Random.value - 0.5f) * 2)
								+ Camera.main.transform.right * (horizontalSpread * (Random.value - 0.5f) * 2);
			
						newprojectile.AddComponent<ConstantForce> ();
						newprojectile.constantForce.force = (projectileAcceleration.x * Camera.main.transform.forward
								+ projectileAcceleration.y * Camera.main.transform.up
								+ projectileAcceleration.z * Camera.main.transform.right) * accelerationScale;
						newprojectile.constantForce.force += (projectileGlobalAcceleration) * accelerationScale;
						newprojectile.constantForce.force *= 50f;
			
						//newprojectile.AddComponent<APEprojectile> ().life = projectileLife;
				}
		}
	
		public Vector3 DisplayPosition (bool swap=false)
		{
				Vector3 result = DisplayOffset (swap) + transform.position;
				return result;
		}
	
		public Vector3 DisplayOffset (bool swap=false)
		{
				Vector3 result = displayOffset.z * transform.forward * transform.lossyScale.z
						+ displayOffset.y * transform.up * transform.lossyScale.y
						+ displayOffset.x * transform.right * transform.lossyScale.x;
				if (!swap) {
						return result;
				} else {
						return new Vector3 (result.z, result.y, result.x);
				}
		}
	
		void OnDrawGizmos ()
		{
				if (gizmosFlag) {
						DrawAPEGizmos ();
				}
		}
		void OnDrawGizmosSelected ()
		{
				if (!gizmosFlag) {
						DrawAPEGizmos ();
				}
		}
	
		void DrawAPEGizmos ()
		{
				if (this.enabled) {
						//Shoot cone display
						Mesh hull = new Mesh ();
						hull.Clear ();
						Vector3[] vertexMatrix = new Vector3[8];
						Vector3[] dots1, dots2;
						dots1 = DrawShot (transform.forward * (projectileSpeed + projectileSpeedDelta), projectileLife, Color.gray);
						dots2 = DrawShot (transform.forward * (projectileSpeed - projectileSpeedDelta), projectileLife, Color.gray);
						System.Array.Copy (dots1, 0, vertexMatrix, 0, dots1.Length);
						System.Array.Copy (dots2, 0, vertexMatrix, dots1.Length, dots2.Length);
						hull.vertices = vertexMatrix;
						Vector3[] except = null;
						DrawMesh (hull, except);
						Gizmos.DrawWireSphere (DrawTrajectory (transform.position, transform.forward * (projectileSpeed), projectileLife, Color.yellow), 0.3f);
	
						//Scale values
						float width = 1 / rateOfFire * clipSize * displayScale;
						float height = displayScale;
			
						//Shooting time display
						for (float i = 0; i < clipSize/rateOfFire; i+=1) {
								float size = 0.25f * height;
								Gizmos.color = Color.white;
								if (i % 5 == 0) {
										size = 0.5f * height;
										Gizmos.color = Color.white;
								}
								if (i % 10 == 0) {
										size = 1.0f * height;
										Gizmos.color = Color.white;
								}
								Gizmos.DrawLine (DisplayPosition () - transform.up * (height * 0 + 0) + transform.forward * i * displayScale,
								DisplayPosition () - transform.up * (height * 0 - size) + transform.forward * i * displayScale);
						}
						Gizmos.color = Color.white;
						Gizmos.DrawLine (DisplayPosition () - transform.up * (-height / 2 - height / 2) + transform.forward * 0,
							DisplayPosition () - transform.up * (-height / 2 - height / 2) + transform.forward * width);
						Gizmos.DrawLine (DisplayPosition () - transform.up * (-height / 2 + height / 2) + transform.forward * 0,
							DisplayPosition () - transform.up * (-height / 2 + height / 2) + transform.forward * width);
						Gizmos.DrawLine (DisplayPosition () - transform.up * (-height / 2 - height / 2) + transform.forward * width,
							DisplayPosition () - transform.up * (-height / 2 + height / 2) + transform.forward * width);
						Gizmos.color = Color.green;
						Gizmos.DrawLine (DisplayPosition () - transform.up * (-height / 2) + transform.forward * 0,
							DisplayPosition () - transform.up * (-height / 2) + transform.forward * Mathf.Max (0, currentAmmo + shootTimer * rateOfFire) / clipSize * width);
			
						//Clip size display
						for (float i = 0; i < clipSize; i+=1) {
								float size = 0.15f * height;
								Gizmos.color = Color.white;
								if (i % 5 == 0) {
										size = 0.5f * height;
										Gizmos.color = Color.white;
								}
								if (i % 10 == 0) {
										size = 1.0f * height;
										Gizmos.color = Color.white;
								}
								Gizmos.DrawLine (DisplayPosition () - transform.up * (+height * 1 + 0) + transform.forward * i * width / clipSize, 
								DisplayPosition () - transform.up * (+height * 1 - size) + transform.forward * i * width / clipSize);
						}
						Gizmos.color = Color.white;
						Gizmos.DrawLine (DisplayPosition () - transform.up * (+height / 2 - height / 2) + transform.forward * 0,
							DisplayPosition () - transform.up * (+height / 2 - height / 2) + transform.forward * width);
						Gizmos.DrawLine (DisplayPosition () - transform.up * (+height / 2 + height / 2) + transform.forward * 0,
							DisplayPosition () - transform.up * (+height / 2 + height / 2) + transform.forward * width);
						Gizmos.DrawLine (DisplayPosition () - transform.up * (+height / 2 - height / 2) + transform.forward * width,
							DisplayPosition () - transform.up * (+height / 2 + height / 2) + transform.forward * width);
						Gizmos.color = Color.yellow;
						Gizmos.DrawLine (DisplayPosition () - transform.up * (+height / 2) + transform.forward * 0,
				            DisplayPosition () - transform.up * (+height / 2) + transform.forward * Mathf.Max (0, currentAmmo) / clipSize * width);
			
						//Reload time display
						for (float i = 0; i < reloadTime; i+=1) {
								float size = 0.15f * height;
								Gizmos.color = Color.white;
								if (i % 5 == 0) {
										size = 0.5f * height;
										Gizmos.color = Color.white;
								}
								if (i % 10 == 0) {
										size = 1.0f * height;
										Gizmos.color = Color.white;
								}
								Gizmos.DrawLine (DisplayPosition () - transform.up * (-height * 0 + 0) - transform.forward * i * displayScale, 
								DisplayPosition () - transform.up * (-height * 0 - size) - transform.forward * i * displayScale);
						}
						Gizmos.color = Color.white;
						Gizmos.DrawLine (DisplayPosition () - transform.up * (-height / 2 - height / 2) - transform.forward * 0, 
							DisplayPosition () - transform.up * (-height / 2 - height / 2) - transform.forward * reloadTime * displayScale);
						Gizmos.DrawLine (DisplayPosition () - transform.up * (-height / 2 + height / 2) - transform.forward * 0, 
							DisplayPosition () - transform.up * (-height / 2 + height / 2) - transform.forward * reloadTime * displayScale);
						Gizmos.DrawLine (DisplayPosition () - transform.up * (-height / 2 - height / 2) - transform.forward * reloadTime * displayScale, 
							DisplayPosition () - transform.up * (-height / 2 + height / 2) - transform.forward * reloadTime * displayScale);
						Gizmos.color = Color.red;
						Gizmos.DrawLine (DisplayPosition () - transform.up * (-height / 2) - transform.forward * 0,
							DisplayPosition () - transform.up * (-height / 2) - transform.forward * reloadTimer * displayScale);
			
						//GUI context line
						Gizmos.color = Color.white;
						Gizmos.DrawLine (transform.position,
							transform.position + transform.up * (height * 2) * IsOverCannon ());
						Gizmos.DrawLine (transform.position + transform.up * (height * 2) * IsOverCannon (),
							DisplayPosition () - transform.up * (height * 2) * IsOverCannon ());
						Gizmos.DrawLine (DisplayPosition () - transform.up * (height * 2) * IsOverCannon (),
							DisplayPosition ());
			
						//Cannon position & shoot direction display
						if (test) {
								Gizmos.color = new Color (0.75f, 0.5f, 0.5f, 1);
						} else {
								Gizmos.color = Color.white;
						}
						Gizmos.DrawLine (transform.position + displayScale * transform.up, transform.position - displayScale * transform.up);
						Gizmos.DrawLine (transform.position + displayScale * transform.right, transform.position - displayScale * transform.right);
						Gizmos.DrawLine (transform.position + displayScale * transform.forward * 1.5f, transform.position - displayScale * transform.forward);
						Gizmos.DrawLine (transform.position + displayScale * transform.forward * 1.5f, transform.position - transform.up * displayScale / 2);
						Gizmos.DrawLine (transform.position + displayScale * transform.forward * 1.5f, transform.position + transform.up * displayScale / 2);
						Gizmos.DrawLine (transform.position + displayScale * transform.forward * 1.5f, transform.position - transform.right * displayScale / 2);
						Gizmos.DrawLine (transform.position + displayScale * transform.forward * 1.5f, transform.position + transform.right * displayScale / 2);
						Gizmos.DrawWireSphere (transform.position, 0.475f * displayScale);
			
						//projectiles per shot display
						Gizmos.color = Color.cyan;
						for (float i = 0; i < projectileCount; i++) {
								float size = 0.5f;
								if (i % 5 == 0) {
										size = 1.0f;
								}
								if (i % 10 == 0) {
										size = 2.0f;
								}
								Gizmos.DrawLine (transform.position + transform.up * i / projectileCount * displayScale * 2,
								transform.position + transform.forward * size * displayScale + transform.up * i / projectileCount * displayScale * 2);
						}
				
						DestroyImmediate (hull);
				}
		}
	
		public int IsOverCannon ()
		{
				if (displayOffset.y > 0) {
						return +1;
				}
				if (displayOffset.y < 0) {
						return -1;
				}
				return 0;
		}
	
		Vector3 DrawTrajectory (Vector3 pos, Vector3 vel, float time, Color color, bool draw=true)
		{
				Gizmos.color = color;
				Vector3 nextPos;
				for (float i = 0; i < time; i+=Time.fixedDeltaTime) {
						nextPos = pos + Time.fixedDeltaTime * vel;
						if (draw) {
								Gizmos.DrawLine (pos, nextPos);
						}
						pos = nextPos;
						Vector3 accel = (projectileAcceleration.x * transform.forward + projectileAcceleration.y * transform.up + projectileAcceleration.z * transform.right + projectileGlobalAcceleration);
						vel += (accel) * accelerationScale;
				}
				return pos;
		}
	
		public Vector3 ShotEnd (Vector3 initialSpeed, float time, Color color)
		{
				return DrawTrajectory (transform.position, initialSpeed, time, color, false);
		}
	
		Vector3[] DrawShot (Vector3 initialSpeed, float time, Color color, bool draw=true)
		{
				Vector3[] res = new Vector3[4];
				res [0] = DrawTrajectory (transform.position, initialSpeed - transform.up * verticalSpread - transform.right * horizontalSpread, time, color, draw);
				res [1] = DrawTrajectory (transform.position, initialSpeed + transform.up * verticalSpread - transform.right * horizontalSpread, time, color, draw);
				res [2] = DrawTrajectory (transform.position, initialSpeed + transform.up * verticalSpread + transform.right * horizontalSpread, time, color, draw);
				res [3] = DrawTrajectory (transform.position, initialSpeed - transform.up * verticalSpread + transform.right * horizontalSpread, time, color, draw);
				Gizmos.color = Color.white;
				return res;
		}
	
		void ExceptionLine (Vector3 source, Vector3 destination)
		{
				bool skip = true;
		
				Vector3 s = transform.worldToLocalMatrix.MultiplyVector (source);
				Vector3 d = transform.worldToLocalMatrix.MultiplyVector (destination);
		
				if (RoughlyEqual (s.x, d.x) || RoughlyEqual (s.y, d.y) || RoughlyEqual (s.z, d.z)) {
						skip = false;
				}
				if (RoughlyEqual (s.x, d.x) ^ RoughlyEqual (s.y, d.y) ^ RoughlyEqual (s.z, d.z)) {
						skip = true;
				}
		
				if (!skip) {
						Gizmos.DrawLine (source, destination);
				}
		}

		void DrawMesh (Mesh mesh, Vector3[] exception)
		{
				foreach (Vector3 vert in mesh.vertices) {
						Gizmos.color = Color.white;
						foreach (Vector3 check in mesh.vertices) {
								ExceptionLine (vert, check);
						}
				}
		}
	
		static bool RoughlyEqual (float a, float b)
		{
				float treshold = 0.01f;
				return (Mathf.Abs (a - b) < treshold);
		}
}
