using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateShapeManager : MonoBehaviour
{
    [SerializeField]
    private float radius = 1.0f;

    [SerializeField]
    private int objectsToBePlaced = 10;

    [SerializeField]
    private float sampleMultipler = 100.0f;

    [SerializeField]
    private PrimitiveType PrimitiveTypeOption = PrimitiveType.Cube;

    [SerializeField]
    private Material objectMaterial;

    [SerializeField]
    private float scaleSpeed = 1.0f;

    [SerializeField]
    private Vector3 objectInitialScale = new Vector3(1,1,1);

    private SpectrumManager spectrumManager = null;

    private GameObject[] gameObjectsCreated;


    void Start()
    {
        spectrumManager = SpectrumManager.Instance;
        gameObjectsCreated = new GameObject[objectsToBePlaced];

        PlaceObjects();
    }

    void PlaceObjects()
    {
        for(int i = 0; i < objectsToBePlaced; i++)
        {
            float angle = (i+1) * Mathf.PI * 2.0f / objectsToBePlaced;
            Vector3 newPos = new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);
            
            GameObject go = GameObject.CreatePrimitive(PrimitiveTypeOption);

            Material randomMaterial = new Material(Shader.Find("Lightweight Render Pipeline/Lit"));
            randomMaterial.name = $"{nameof(PrimitiveType.Cube)}_{i}_material";
            randomMaterial.color = GetRandomColor();

            randomMaterial.EnableKeyword("_EMISSION");
            randomMaterial.SetColor("_EmissionColor", randomMaterial.color);

            go.GetComponent<Renderer>().material = randomMaterial;

            go.transform.position = newPos;
            go.transform.localScale = objectInitialScale;
            go.name = $"{nameof(PrimitiveType.Cube)}_{i}";
            go.transform.parent = gameObject.transform;
            gameObjectsCreated[i] = go;
        }
    }

    Color GetRandomColor() => Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

    void Update()
    {
        for(int i = 0; i < gameObjectsCreated.Length; i++)
        {   
            float value = spectrumManager.Samples[i] * sampleMultipler;
            GameObject go = gameObjectsCreated[i];
            
            go.transform.localScale = Vector3.Lerp(
                go.transform.localScale,
                new Vector3(go.transform.localScale.x, value, go.transform.localScale.z),
                scaleSpeed * Time.deltaTime);
        }
    }

}