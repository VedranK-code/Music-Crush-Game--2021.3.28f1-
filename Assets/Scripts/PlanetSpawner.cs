using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    public Sprite[] planetSprites;
    public float spawnInterval = 5f;
    public float minScale = 0.2f;
    public float maxScale = 1f;
    public int maxPlanetsOnScreen = 10; // Maximum number of planets allowed on the screen
    public float moveSpeed = 0.5f; // Speed at which the planets move upwards
    public float zPosition = -150f; // Z-axis position of the planets

    private List<GameObject> planets = new List<GameObject>(); // List to keep track of spawned planets
    private List<Sprite> shuffledPlanetSprites = new List<Sprite>(); // Shuffled planet sprites

    private void Start()
    {
        ShufflePlanetSprites();
        StartCoroutine(SpawnPlanets());
    }

    private void ShufflePlanetSprites()
    {
        shuffledPlanetSprites.AddRange(planetSprites); // Copy the planetSprites array to the shuffledPlanetSprites list
        int n = shuffledPlanetSprites.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            Sprite temp = shuffledPlanetSprites[k];
            shuffledPlanetSprites[k] = shuffledPlanetSprites[n];
            shuffledPlanetSprites[n] = temp;
        }
    }

    private IEnumerator SpawnPlanets()
    {
        int currentIndex = 0;

        while (true)
        {
            if (planets.Count >= maxPlanetsOnScreen)
            {
                DestroyOldestPlanet();
            }

            // Instantiate a planet sprite from the shuffled array
            Sprite planetSprite = shuffledPlanetSprites[currentIndex];
            currentIndex = (currentIndex + 1) % shuffledPlanetSprites.Count;

            GameObject planet = new GameObject("Planet");
            SpriteRenderer spriteRenderer = planet.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = planetSprite;

            // Set the planet's position at the bottom of the screen
            float spawnX = Random.Range(Screen.width * 0.1f, Screen.width * 0.9f);
            float spawnY = -spriteRenderer.bounds.extents.y-400f;
            planet.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(spawnX, spawnY, zPosition));

            // Set a random scale for the planet
            float scale = Random.Range(minScale, maxScale);
            planet.transform.localScale = new Vector3(scale, scale, 1f);

            // Move the planet upwards
            planets.Add(planet); // Add the spawned planet to the list
            StartCoroutine(MovePlanet(planet));

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void DestroyOldestPlanet()
    {
        GameObject oldestPlanet = planets[0];
        planets.RemoveAt(0);
        Destroy(oldestPlanet);
    }

    private IEnumerator MovePlanet(GameObject planet)
    {
        while (planet.transform.position.y < Screen.height + planet.GetComponent<SpriteRenderer>().bounds.extents.y)
        {
            planet.transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
            yield return null;
        }

        Destroy(planet);
        planets.Remove(planet); // Remove the destroyed planet from the list
    }
}
