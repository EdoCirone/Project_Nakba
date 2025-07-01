using UnityEngine;

public class StatusAilments : MonoBehaviour
{
    public bool isInjured = false;
    public bool isSick = false;
    public bool isSad = false;

    [SerializeField] float sickDamagePerMinute = 5f;
    [SerializeField] float moraleMultiplierWhenSad = 2f;
    [SerializeField] float speedDebuffWhenSad = 0.8f;
    [SerializeField] float speedDebuffWhenInjured = 0.5f;
    [SerializeField] float maxHealthPercentIfInjured = 0.7f;

    private Bisogni _bisogni;
    private TopDownMovement _movement;
    private LifeController _life;

    void Awake()
    {
        _bisogni = GetComponent<Bisogni>();
        _movement = GetComponent<TopDownMovement>();
        _life = GetComponent<LifeController>();
    }

    void Update()
    {
        float delta = Time.deltaTime / 60f;

        if (isSick && _life != null)
            _life.AddHp(-sickDamagePerMinute * delta);

        if (isSad && _bisogni != null)
            _bisogni.ApplySadnessEffect(moraleMultiplierWhenSad);

        if (_movement != null)
        {
            float baseSpeed = _movement.BaseSpeed;

            float finalSpeed = baseSpeed;
            if (isInjured)
                finalSpeed *= speedDebuffWhenInjured;
            if (isSad)
                finalSpeed *= speedDebuffWhenSad;

            _movement.OverrideSpeed(finalSpeed);
        }

        if (isInjured && _life != null)
        {
            float cap = _life.MaxHP * maxHealthPercentIfInjured;
            if (_life.CurrentHP > cap)
                _life.SetHp(cap);
        }
    }
}