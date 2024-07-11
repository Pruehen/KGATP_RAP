namespace ViewModel.Extensions
{
    public static class PlayerUIViewModelExtension
    {
        public static void Register_OnEnable(this PlayerUIViewModel vm)
        {
            Player.Instance.Register_OnHpChange(vm.OnHpChange);
            Player.Instance.Register_OnGaugeChange(vm.OnGaugeChange);
        }
        public static void UnRegister_OnDisable(this PlayerUIViewModel vm)
        {
            Player.Instance.UnRegister_OnHpChange(vm.OnHpChange);
            Player.Instance.UnRegister_OnGaugeChange(vm.OnGaugeChange);
        }

        public static void OnHpChange(this PlayerUIViewModel vm, int hp)
        {
            vm.Hp = hp;            
        }
        public static void OnGaugeChange(this PlayerUIViewModel vm, float gaugeRatio)
        {
            vm.Gauge = gaugeRatio;
        }
    }
}
