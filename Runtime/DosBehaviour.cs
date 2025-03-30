namespace Framework
{
    using System.Runtime.CompilerServices;

    public abstract class DosBehaviour : RootBehaviour
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected sealed override void Awake()
        {
            base.Awake();

            Awoke();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected sealed override void OnDestroy()
        {
            base.OnDestroy();

            BeforeDestroy();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void OnEnable(){ OnEnabled(); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void OnDisable(){ OnDisabled(); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected sealed override void Update()
        {
            base.Update();

            Tick();
        }

        protected virtual void Awoke() { }
        protected virtual void BeforeDestroy() { }
        protected virtual void Tick() { }
        protected virtual void OnEnabled() { }
        protected virtual void OnDisabled() { }
    }
}