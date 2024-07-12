namespace ViewModel.Extensions
{
    public static class PlayerUIViewModelExtension
    {
        public static void Register_OnEnable(this PlayerUIViewModel vm)
        {
            Player.Instance.Register_OnHpChange(vm.OnHpChange);
            Player.Instance.Register_OnSkillGaugeChange(vm.OnSkillGaugeChange);
            Player.Instance.Register_OnEvasionGaugeChange(vm.OnEvasionGaugeChange);
        }
        public static void UnRegister_OnDisable(this PlayerUIViewModel vm)
        {
            Player.Instance.UnRegister_OnHpChange(vm.OnHpChange);
            Player.Instance.UnRegister_OnSkillGaugeChange(vm.OnSkillGaugeChange);
            Player.Instance.UnRegister_OnEvasionGaugeChange(vm.OnEvasionGaugeChange);
        }

        public static void OnHpChange(this PlayerUIViewModel vm, int hp)
        {
            vm.Hp = hp;            
        }
        public static void OnSkillGaugeChange(this PlayerUIViewModel vm, float gaugeRatio)
        {
            vm.SkillValueRatio = gaugeRatio;
        }
        public static void OnEvasionGaugeChange(this PlayerUIViewModel vm, float gaugeRatio)
        {
            vm.EvadeValueRatio = gaugeRatio;
        }
    }
}
