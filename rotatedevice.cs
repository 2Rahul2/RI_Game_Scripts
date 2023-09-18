using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine;
[System.Serializable]
public class WireList{
    public GameObject[] Wires;
}
public class rotatedevice : MonoBehaviour , IPointerEnterHandler ,IPointerExitHandler
{

    // public rotatePuzzle Rp;
    public bool aligned , once , isprevious , truealigned , protectedhack , twowaycircuit;
    private bool click=false ;
    public WireList[] wierlist;
    [SerializeField] private bool multipleConnection , changepreviousnode ,startingDevice ,changeprevMultiplConnectioneAngle , changeprevNodeAngle , changeCurrentprevnodeangle;
    private float lerptime , roundValue;
    public float startangle;
    [SerializeField] private int[] FinalAngle , FinalAngle2 , MultipleConnectionFinalAngle , previoudNodeAngle , prevMultipleConnectionAngle ,prevNodeangle ,changeCurrentmultipleconnectionAngle , changeCurrentprevnodeAngleInt;
    public GameObject[] previousNode , changePropertiesObject , changewireObject ,changePrevNode , PrevNodePostProtected , MultipleConnectionWire;
    public int[] changeangle;
    // private these afterwards
    // don't 
    public int count=0 , countchangeangle=0 , child_count , changeObject_Wirecount=0 ,changemultipleConnectionCount=0;
    
    public GameObject wireParticle , changeProtectWire;
    public Sprite changeImage;
    public Material grayColorParticle , blueColorParticle;
    public endDevice TrueAlignedCount;
    private bool incrementAlignedCountOnce=false;

    public Sprite unprotectimg , protectimg;
    // [SerializeField] private Transform 
    private void Start() {
        roundValue = Mathf.Round(transform.eulerAngles.z);
        // changeWireColor();
        foreach(var wireObject in wierlist){
                    foreach(var wires in wireObject.Wires){
                        wires.GetComponent<ParticleSystemRenderer>().material = grayColorParticle;
                    }
                }
        child_count = wireParticle.transform.childCount;
        // for(int ind = 0;ind<previoudNodeAngle.Length;ind++){
        //     float RoundVal = Mathf.Round(previousNode[ind].transform.eulerAngles.z);
        //     if(RoundVal == previoudNodeAngle[ind]){
        //         wirecount += 1;
        
        //     }else{
        //         wirecount -=1;
        //     }
        // }
        if(count>=previousNode.Length){
            count = previousNode.Length;
        }
        prevNodeisAlignedForWire();
    }
    public bool prevNodeisAlignedForWire(){
        if(!startingDevice){
            for(int ind = 0;ind<previoudNodeAngle.Length;ind++){
                float RoundVal = Mathf.Round(previousNode[ind].transform.eulerAngles.z);
                if(RoundVal == previoudNodeAngle[ind]){
                    if(previousNode[ind].GetComponent<rotatedevice>().prevNodeisAlignedForWire()){
                        return true;
                    }else{
                        return false;
                    }
                }else{
                    return false;
                }
            }
            return false;
        }else{
            return true;
        }
    }
    void FixedUpdate()
    {
        // if(EventSystem.current.IsPointerOverGameObject()){
        //     print("hovering in if");
        // }

        // -90 == 270
        // -180 == 180
        roundValue = Mathf.Round(transform.eulerAngles.z);


        // wirecount = 0;
                
        // print(roundValue);
        if(twowaycircuit){
            if(roundValue == 0){                
                var eulerAngles = transform.eulerAngles;
                transform.Rotate(new Vector3(eulerAngles.x,0,180));
                startangle = 180;
            }
        }
        if(transform.eulerAngles.z == -90){
            // print("-90");
            var eulerAngles = transform.eulerAngles;
            transform.Rotate(new Vector3(eulerAngles.x,eulerAngles.y,270));
        }else if(transform.eulerAngles.z == -180){
            var eulerAngles = transform.eulerAngles;
            transform.Rotate(new Vector3(eulerAngles.x,eulerAngles.y,180));
        }
        if(roundValue == FinalAngle[0] || roundValue == FinalAngle[1]){
            aligned = true;
        }else{
            count =0;
            once =false;
            aligned=false;
        }
        if(isprevious){
            if(aligned){
                // truealigned=false;
                foreach(var oneNode in previousNode){
                    if(oneNode.GetComponent<rotatedevice>().truealigned && !oneNode.GetComponent<rotatedevice>().protectedhack){
                        count+=1;
                    }else{
                        count =0;
                    }
                }
            }
            if(count >= previousNode.Length){
                count = previousNode.Length;
            }
            if(count == previousNode.Length){
                if(protectedhack){

                    gameObject.GetComponent<Image>().sprite = unprotectimg;
                }

                truealigned=true;
                if(!incrementAlignedCountOnce){
                    TrueAlignedCount.CurrentAlignedCount++;
                    incrementAlignedCountOnce=true;
                }
                for(int index=0;index< child_count;index++){
                    GameObject wire = wireParticle.transform.GetChild(index).gameObject;
                    // wire.GetComponent<ParticleSystemRenderer>().material = blueColorParticle;
                }

                // foreach(var wire in wireParticle){
                //     wire.GetComponent<ParticleSystemRenderer>().material = blueColorParticle;
                //     // wire.SetActive(true);
                // }
                // print("spawnParticles");
            }else{
                if(protectedhack){
                    gameObject.GetComponent<Image>().sprite = protectimg;
                }
                for(int index=0;index< child_count;index++){
                    GameObject wire = wireParticle.transform.GetChild(index).gameObject;
                    // wire.GetComponent<ParticleSystemRenderer>().material = grayColorParticle;
                }
                // foreach(var wire in wireParticle){
                //     wire.GetComponent<ParticleSystemRenderer>().material = grayColorParticle;
                // }
                truealigned=false;
                if(incrementAlignedCountOnce){
                    TrueAlignedCount.CurrentAlignedCount--;
                    incrementAlignedCountOnce=false;
                }
                // wireParticle.SetActive(false);
            }
        }
        if(click){
            if(lerptime>1){
                foreach(var wireObject in wierlist){
                    foreach(var wires in wireObject.Wires){
                        wires.GetComponent<ParticleSystemRenderer>().material = grayColorParticle;
                    }
                }
                // foreach(var oneNode in previousNode){
                //     if(oneNode.GetComponent<rotatedevice>().truealigned){
                //         count+=1;
                //     }else{
                //         count =0;
                //     }
                // }
                if(count >= previousNode.Length){
                    count = previousNode.Length;
                }
                click = false;
            }else{
                var eulerAngles = transform.eulerAngles;
                transform.rotation = Quaternion.Lerp(transform.rotation , Quaternion.Euler(eulerAngles.x , eulerAngles.y , startangle)  , lerptime);
                lerptime += Time.deltaTime * 3;
            }
        }
                changeWireColor();
                // TriggerChanges();
        // OnMouseOver();
    }
    void TurnGrayWire(){
        foreach(var wireObject in wierlist){
            foreach(var wires in wireObject.Wires){
                wires.GetComponent<ParticleSystemRenderer>().material = grayColorParticle;
            }
        }
    }
    void changeWireColor(){
        if(multipleConnection){
                    // GameObject MergeAngle_Wires = MultipleConnectionFinalAngle.Zip(MultipleConnectionWire , (angle , wire) => new {multipleConnection =wire ,MultipleConnectionWire = angle});
                    // for(int index=0;index < MultipleConnectionFinalAngle.Length;index++){
                    //     if(MultipleConnectionFinalAngle[index] == roundValue){
                    //         GameObject[] wires = MultipleConnectionWire[index].GetComponentsInChildren<GameObject>();
                    //         // GameObject [] morewires = 
                    //         foreach(var wire in wires){
                    //             wire.GetComponent<ParticleSystemRenderer>().material = blueColorParticle;
                    //         }
                    //     }
                    // }
                    if(prevNodeisAlignedForWire()){
                        for(int index=0;index<MultipleConnectionFinalAngle.Length;index++){
                            // print(index);
                            if(MultipleConnectionFinalAngle[index] == roundValue){
                                print(gameObject.name);
                                foreach(var wire in wierlist[index].Wires){
                                    wire.GetComponent<ParticleSystemRenderer>().material = blueColorParticle;
                                }
                            }
                            // else{
                            //     foreach(var wire in wierlist[index].Wires){
                            //         wire.GetComponent<ParticleSystemRenderer>().material = grayColorParticle;
                            //     }
                            // }
                        }
                    }else{
                        TurnGrayWire();
                    }
        }
    }
    // public GameObject[] previosNode;
    public bool changePrevNodePreviousNode;
    public int changeprevnodeangleCount=0;

    public void TriggerChanges(){
        if(count == previousNode.Length){
            if(protectedhack){
                // changeprevnodePrevnodeLength=changePrevNode.Length;
                if(changeCurrentprevnodeangle){
                    for(int j=0;j<changeCurrentprevnodeAngleInt.Length;j++){
                        previoudNodeAngle[j]=changeCurrentprevnodeAngleInt[j];
                    }
                }
                for(int i =0 ; i<MultipleConnectionFinalAngle.Length;i++){
                    MultipleConnectionFinalAngle[i] =changeCurrentmultipleconnectionAngle[i];
                }
                        changeObject_Wirecount=0;
                    foreach(var changeProperties in changePropertiesObject){

                        if(changeProperties.GetComponent<rotatedevice>().changeprevNodeAngle){
                            for(int k = 0;k<changeProperties.GetComponent<rotatedevice>().previoudNodeAngle.Length;k++){
                                changeProperties.GetComponent<rotatedevice>().previoudNodeAngle[k] = prevNodeangle[changeprevnodeangleCount];
                                changeprevnodeangleCount+=1;
                            }
                        }
                        if(changeProperties.GetComponent<rotatedevice>().changeprevMultiplConnectioneAngle){
                            for(int i = 0;i<changeProperties.GetComponent<rotatedevice>().MultipleConnectionFinalAngle.Length;i++){
                                print(changemultipleConnectionCount);
                                changeProperties.GetComponent<rotatedevice>().MultipleConnectionFinalAngle[i] = prevMultipleConnectionAngle[changemultipleConnectionCount];
                                changemultipleConnectionCount += 1;
                            }
                        }
                        // changeProtectWire.SetActive(true);
                        // changeProperties.GetComponent<rotatedevice>().wireParticle.SetActive(false);
                        // changeProperties.GetComponent<rotatedevice>().wireParticle = changewireObject[changeObject_Wirecount];
                        // changeProperties.GetComponent<rotatedevice>().child_count = changewireObject[changeObject_Wirecount].transform.childCount;
                        // print(changewireObject[changeObject_Wirecount].name);
                        // print("heehe");
                        
                        for(int index = 0;index<2;index++){
                            changeProperties.GetComponent<rotatedevice>().FinalAngle[index] = changeangle[countchangeangle];
                            countchangeangle+=1;
                        }
                        if(changePrevNodePreviousNode){
                            for(int i=0;i<changeProperties.GetComponent<rotatedevice>().previousNode.Length;i++){
                                changeProperties.GetComponent<rotatedevice>().previousNode[i] = changePrevNode[changeObject_Wirecount];
                                changeObject_Wirecount += 1;
                            }
                        }
                        print("change" + changeObject_Wirecount);
                        // wireParticle = changeProtectWire;
                        // child_count = changeProtectWire.transform.childCount;
                    }
                    if(changepreviousnode){
                        for(int indexi = 0;indexi<previousNode.Length;indexi++){
                            previousNode[indexi] = PrevNodePostProtected[indexi];
                        }                    
                    }
                    gameObject.GetComponent<Image>().sprite = changeImage;
                    // transform.rotation = Quaternion.Euler(transform.eulerAngles.x,90,0);
                    FinalAngle[0] = FinalAngle2[0];
                    FinalAngle[1] = FinalAngle2[1];
                    transform.rotation = Quaternion.Euler(transform.eulerAngles.x,transform.eulerAngles.y,0);
                    // transform.rotation = Quaternion.Euler(0,180,0);
                    // print("rotate to zero");
                    protectedhack=false;
                }
        }
    }
    public void clicked(){
        if(!protectedhack){
            click=true;
            lerptime =0;
            startangle += 90;
            
        }
        
        // print("clicked");
    }
    // private void OnMouseOver() {
    //     Debug.Log("hover");
    // }
    public void OnPointerEnter(PointerEventData evenData){
        // print("hovering");
        // print(transform.GetChild(0).name);
        transform.GetChild(0).localScale = Vector3.Lerp(transform.GetChild(0).localScale , new Vector3(1.2f , 1.2f , 1.2f) , 20*Time.deltaTime);
        // transform.GetChild(0).localScale = new Vector3(1.2f , 1.2f , 1.2f);
    }
    public void OnPointerExit(PointerEventData eventData){
        transform.GetChild(0).localScale = new Vector3(1f,1f,1f);
    }
}
