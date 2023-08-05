
using UnityEngine;

public class AuroraMaterialGeneration : MonoBehaviour
{
    private LevelManager levelManager;
    private Material auroraMaterial;

    private Color auroraColor;
    private float noiseScale;
    private Vector2 noiseTiling;
    private Vector2 noiseSpeed;
    private Texture2D mask;
    private float maskPower;
    private float disolvePower;

    private void AssignStartingValuesToVariables()
    {
        levelManager = FindObjectOfType<LevelManager>();

        float intensity = Mathf.Pow(2, Random.Range(2f, 4f));

        auroraColor = Random.ColorHSV(0f, 1f, 0.75f, 1f, 0.05f, 0.35f) * intensity;
        //new Vector4(Random.Range(0.0f, 1.0f) * intensity,
        // Random.Range(0.0f, 1.0f) * intensity, Random.Range(0.0f, 1.0f) * intensity, 1.0f);

        noiseScale = Random.Range(10.0f, 32.0f);

        noiseTiling = new Vector2(Random.Range(1.5f, 3.0f), Random.Range(0.2f, 0.4f));

        noiseSpeed = new Vector2(0, Random.Range(-0.045f, -0.015f));

        mask = levelManager.auroraEffectMask[Random.Range(0, levelManager.auroraEffectMask.Length)];

        maskPower = Random.Range(1.8f, 2.2f);

        disolvePower = Random.Range(2.5f, 4.0f);
    }

    void Start()
    {
        AssignStartingValuesToVariables();

        // auroraMaterial = new Material(Shader.Find("Specular"));
        auroraMaterial = new Material(levelManager.auroraShader);

        auroraMaterial.SetColor("_AuroraColor", auroraColor);
        auroraMaterial.SetFloat("_NoiseScale", noiseScale);
        auroraMaterial.SetVector("_NoiseTiling", noiseTiling);
        auroraMaterial.SetVector("_NoiseSpeed", noiseSpeed);
        auroraMaterial.SetTexture("_Mask", mask);
        auroraMaterial.SetFloat("_MaskPower", maskPower);
        auroraMaterial.SetFloat("_DisolvePower", disolvePower);
        
        gameObject.GetComponent<MeshRenderer>().material = auroraMaterial;

    }


}
