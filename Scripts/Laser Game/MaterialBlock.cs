using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialBlock : MonoBehaviour
{
    public Material[] defaultMaterials;
    public Material buildmodeMaterial;
    public Material wrongMaterial;

    public Renderer renderer;

    private void Awake()
    {
        //renderer = GetComponentInChildren<Renderer>();
        //defaultMaterials = renderer.materials;
    }

    public void SetDefaultMode() => ChangeMaterials(0);
    public void SetBuildableMode() => ChangeMaterials(1);
    public void SetUnbuildableMode() => ChangeMaterials(2);

    private void ChangeMaterials(int index)
    {
        if (index == 0)
            HandleMaterials(defaultMaterials);
        else if (index == 1)
            HandleMaterials(new Material[] { buildmodeMaterial, buildmodeMaterial, buildmodeMaterial, buildmodeMaterial, buildmodeMaterial, buildmodeMaterial, buildmodeMaterial, buildmodeMaterial, buildmodeMaterial });
        else if (index == 2)
            HandleMaterials(new Material[] { wrongMaterial, wrongMaterial, wrongMaterial, wrongMaterial, wrongMaterial, wrongMaterial, wrongMaterial, wrongMaterial, wrongMaterial });
    }

    private void HandleMaterials(Material material)
    {
        for (int i = 0; i < renderer.materials.Length; i++)
        {
            renderer.material = material;
        }
    }

    private void HandleMaterials(Material[] materials)
    {
        renderer.materials = materials;
    }

    public void CheckBuildable(Vector3 position)
    {
        Collider[] hits = Physics.OverlapBox(position + Vector3.up, Vector3.one * 0.6f, Quaternion.identity);

        bool red = false;
        for (int i = 0; i < hits.Length; i++)
        {
            if (!hits[i].CompareTag("Ground"))
                if (hits[i].CompareTag("Untagged") && hits[i].gameObject.layer == 2)
                {
                }
                else if (hits[i].CompareTag("Pickable object"))
                {
                    Debug.Log("Pickable or Receiver");
                    if (!hits[i].GetComponent<MaterialBlock>().Equals(this))
                    {
                        red = true;
                        break;
                    }
                }
                else if (hits[i].CompareTag("Receiver"))
                {
                    if (!hits[i].GetComponentInParent<MaterialBlock>().Equals(this))
                    {
                        red = true;
                        break;
                    }
                }
                else if (hits[i].CompareTag("Detector"))
                {
                    if (hits[i].GetComponentInParent<MaterialBlock>() != null)
                        if (!hits[i].GetComponentInParent<MaterialBlock>().Equals(this))
                        {
                            red = true;
                            break;
                        }
                }
                else
                {
                    red = true;
                    break;
                }
        }

        if (red)
            ChangeMaterials(2);
        else
            ChangeMaterials(1);
    }
}
