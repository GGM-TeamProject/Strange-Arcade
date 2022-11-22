using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTrail : MonoBehaviour
{
    [Header("Mesh Related")]
    public float MeshRefreshRate = 0.1f;
    public float MeshDestroyDelay = 3f;
    public Transform PositionToSpawn;

    [Header("Shader Related")]
    public Material Mat;
    public string ShaderVarRef;
    public float ShaderVarRate = 0.1f;
    public float ShaderVarRefreshRate = 0.05f;

    private bool _isTrailActive = false;
    private MeshRenderer[] _meshRenderers = null;
    
    public void ActiveTrail(float activeTime){
        if(_isTrailActive) return;

        _isTrailActive = true;
        StartCoroutine(ActivateTrail(activeTime));
    }

    IEnumerator ActivateTrail(float timeActive){
        while(timeActive > 0){
            timeActive -= MeshRefreshRate;

            if(_meshRenderers == null)
                _meshRenderers = GetComponentsInChildren<MeshRenderer>();

            GameObject obj = new GameObject();
            foreach(MeshRenderer meshRenderer in _meshRenderers){
                obj.transform.SetPositionAndRotation(PositionToSpawn.position, PositionToSpawn.rotation);
                obj.transform.localScale = new Vector3(3, 3, 3);

                MeshRenderer mr = new GameObject("meshRenderer").AddComponent<MeshRenderer>();
                MeshFilter mf = mr.gameObject.AddComponent<MeshFilter>();
                mr.transform.SetParent(obj.transform);
                mr.gameObject.transform.SetLocalPositionAndRotation(meshRenderer.transform.localPosition, meshRenderer.transform.rotation);
                mr.gameObject.transform.localScale = new Vector3(100, 100, 100);

                Mesh mesh = meshRenderer.GetComponent<MeshFilter>().mesh;

                mf.mesh = mesh;
                mr.material = Mat;

                StartCoroutine(AnimateMaterialFloat(mr.material, 0, ShaderVarRate, ShaderVarRefreshRate));

                Destroy(obj, MeshDestroyDelay);
            }

            yield return new WaitForSeconds(MeshRefreshRate);
        }

        _isTrailActive = false;
    }

    IEnumerator AnimateMaterialFloat(Material mat, float goal, float rate, float refrehRate){
        float valueToAnimate = mat.GetFloat(ShaderVarRef);
        
        while(valueToAnimate > goal){
            valueToAnimate -= rate;
            mat.SetFloat(ShaderVarRef, valueToAnimate);
            yield return new WaitForSeconds(refrehRate);
        }
    }
}
