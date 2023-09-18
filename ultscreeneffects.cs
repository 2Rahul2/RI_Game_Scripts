using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
public class ultscreeneffects : MonoBehaviour
{
    public playerTwo player;
    public float changevalue;
    public PostProcessVolume volume;
    private Vignette vig;
    private ChromaticAberration Ca;
    private ColorGrading Cg;
    // private Lens

    public float effecttime;
    public float etime=0;
    // Start is called before the first frame update
    void Start()
    {
        // etime = effecttime;
        volume.profile.TryGetSettings(out vig);
        volume.profile.TryGetSettings(out Ca);
        volume.profile.TryGetSettings(out Cg);
        player.ultcomplete = true;

        // vig = ScriptableObject.CreateInstance<Vignette>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.ultcomplete == false){
            if(vig.intensity.value<0.5f){
                vig.intensity.value += Time.deltaTime;
                etime = effecttime;
            }
            // Cg.colorFilter.value = (123 , 88 , 69);
            Ca.enabled.Override(true);
            Cg.enabled.Override(true);


            // vig.enabled.Override(true);
            // vig.intensity.Override(0.5f);
        }else{
            if(etime<=0){
                // etime = effecttime;
                Cg.enabled.Override(false);
                Ca.enabled.Override(false);
                // vig.intensity.value = 0;
                vig.intensity.value -= Time.deltaTime / 10;
            }else{
                etime -= Time.deltaTime;

            }
            // vig.intensity.Override(0);
            // vig.enabled.Override(false);
        }
    }
}
