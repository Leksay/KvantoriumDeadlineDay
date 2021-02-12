using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class NewtonSystem : MonoBehaviour
{
    [Range(0, 10)]
    [SerializeField] private float forceScale;
    [SerializeField] private List<Planet> Planets;
    [SerializeField] private float forceLooseScale;
    private Vector2 impulse;
    private float currentImpulse;
    private bool rightSide;
    private Planet activePlanet;
    private void Awake()
    {
        Planets = new List<Planet>();
    }
    private void Start()
    {
        Planets = Planets.OrderBy(p => p.collider.transform.position.x).ToList();
    }
    public void AddPlanet(NewtonPlanet planet)
    {
        Planets.Add(new Planet(planet));
        Planets.First().collider.GetComponent<NewtonPlanet>().SetDirection(-1);
        Planets.Last().collider.GetComponent<NewtonPlanet>().SetDirection(1);
    }

    public void AttackImpulse(Collider2D getImpulseCollider, float xDir)
    {
        StopAllPlanets();
        MakeImpulse(xDir);
    }

    public void TriggerEnterManagament()
    {
        StopAllPlanets();
        MakeImpulse();
    }
    private void StopAllPlanets()
    {
        Planets.ForEach(p =>
        { 
            p.rb.velocity = Vector2.zero;
        }
        );
    }



    private void MakeImpulse(float xDir)
    {
        activePlanet = xDir > 0 ? Planets.Last() : Planets.First();
        activePlanet.planet.checkingPlanet = true;
        impulse = Vector2.right * xDir * forceScale;
        currentImpulse = forceScale;
        activePlanet.rb.AddForce(impulse, ForceMode2D.Impulse);
    }

    private void MakeImpulse()
    {
        activePlanet.planet.checkingPlanet = false;
        if(activePlanet.collider == Planets[0].collider)
        {
            activePlanet = Planets[1];
        }
        else
        {
            activePlanet = Planets[0];
        }
        float xDir = activePlanet.planet.xDirection;
        activePlanet.planet.checkingPlanet = true;
        currentImpulse -= forceLooseScale;
        impulse = Vector2.right * xDir * currentImpulse;
        activePlanet.rb.AddForce(impulse, ForceMode2D.Impulse);
        if(currentImpulse < 0.1f)
        {
            activePlanet.planet.checkingPlanet = false;
        }
    
    }

    [System.Serializable]
    private struct Planet
    {
        public Rigidbody2D rb;
        public Collider2D collider;
        public NewtonPlanet planet;
        public Planet(NewtonPlanet planet)
        {
            this.rb = planet.myRB;
            this.collider = planet.myCollider;
            this.planet = planet;
        }
    }
}
