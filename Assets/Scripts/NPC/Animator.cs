using UnityEngine;
using DragonBones;

public class Animator : MonoBehaviour
{
    //dragon bones
    public UnityArmatureComponent ArmatureComponent;
    public System.Action FunAfterAnimation;
    public string NameAnim = "";
    public int PlayTimes = -1;
    
    private string _nameFunctionAfterAnimation = "";
    
    private void Awake()
    {
        if (ArmatureComponent == null)
        {
            ArmatureComponent = GetComponent<UnityArmatureComponent>();
        }
    }
    private void Start()
    {
        this.ArmatureComponent.AddDBEventListener(EventObject.START, this.OnAnimationEventHandler);
        this.ArmatureComponent.AddEventListener(EventObject.LOOP_COMPLETE, this.OnAnimationEventHandler);
        this.ArmatureComponent.AddDBEventListener(EventObject.COMPLETE, this.OnAnimationEventHandler);
        this.ArmatureComponent.AddDBEventListener(EventObject.FADE_IN, this.OnAnimationEventHandler);
        this.ArmatureComponent.AddDBEventListener(EventObject.FADE_IN_COMPLETE, this.OnAnimationEventHandler);
        this.ArmatureComponent.AddDBEventListener(EventObject.FADE_OUT, this.OnAnimationEventHandler);
        this.ArmatureComponent.AddDBEventListener(EventObject.FADE_OUT_COMPLETE, this.OnAnimationEventHandler);
        this.ArmatureComponent.AddDBEventListener(EventObject.FRAME_EVENT, this.OnAnimationEventHandler);
        // Add sound event listener
        this.ArmatureComponent.AddDBEventListener(EventObject.SOUND_EVENT, this.OnSoundEventHandler);
        //UnityFactory.factory.soundEventManager.AddDBEventListener(EventObject.SOUND_EVENT, this.OnSoundEventHandler);
    }
    void OnSoundEventHandler(string type, EventObject eventObject)
    {
        //UnityEngine.Debug.Log(eventObject.name);
        if (eventObject.name == "soundName")
        {
            //this._sound.Play();
        }
    }
    //слушатель окончания анимации
    private void OnAnimationEventHandler(string type, EventObject eventObject)
    {
        //print("listener " + type);
        if (FunAfterAnimation != null)
        {
            switch (type)
            {
                case "loopComplete":
                    //print("loopComplete " + armatureComponent.animation.lastAnimationName);
                    if (NameAnim == _nameFunctionAfterAnimation)
                    {
                        //print("Заканчиваю анимацию " + Name_anim);//+ _arr_name_fun_anim[0]);
                        System.Action func = FunAfterAnimation;
                        ClearFun();
                        func();
                    }
                    break;
            }
        }
    }
    //очистка переданных функций
    public void ClearFun()
    {
        _nameFunctionAfterAnimation = "";
        FunAfterAnimation = null;
    }
    public void Play(string name_anim, int play_times = -1)
    {
        //print("Play" + name_anim);// + "  " + _arr_name_fun_anim.Count);
        if (NameAnim != name_anim && FunAfterAnimation == null)
        {
            NameAnim = name_anim;
            PlayTimes = play_times;
            ArmatureComponent.animation.Play(name_anim, play_times);
        }
    }
    //запуск переданной функции по окончанию анимации
    public void FunctionAfterAnimation(string name, System.Action fun, int play_times=-1)
    {
        if (FunAfterAnimation == null)//Name_anim != name && 
        {
            //print("Play_end_fun " + name);
            ArmatureComponent.animation.Play(name, play_times);
            NameAnim = name;
            _nameFunctionAfterAnimation = name;
            FunAfterAnimation = fun;
        }
    }
}
