
namespace MauiApp6.Controls
{
    public class WizardStepsControl : Grid 
    {
        public event EventHandler<StepChangedEventArgs>? StepChanged;

        public class StepChangedEventArgs : EventArgs
        {
            public StepChangedEventArgs(int previousStepIndex, int stepIndex)
                : base()
            {
                PreviousStepIndex = previousStepIndex;
                StepIndex = stepIndex;
            }

            public int PreviousStepIndex { get; }
            public int StepIndex { get; }
        }
        protected override void OnChildAdded(Element child)
        {
            if (child is VisualElement ve)
            {
                ve.IsVisible = false;

                if (GetCurrentIndex() < 0)
                {
                    ve.IsVisible = true;
                }
            }

            base.OnChildAdded(child);
        }

        public int GetCurrentIndex()
        {
            for (var i = 0; i < Children.Count; i++)
            {
                if (Children[i] is VisualElement ve && ve.IsVisible)
                    return i;
            }

            return -1;
        }

        public async Task Forward()
        {
            var c = GetCurrentIndex();

            var currentIndex = c;
            var nextIndex = c + 1;

            if (nextIndex >= Children.Count)
                nextIndex = 0;
            if (currentIndex == nextIndex)
                return;

            var currentView = Children[currentIndex] as VisualElement;
            var nextView = Children[nextIndex] as VisualElement;

            // Prepare the 'next' view to show, moving it out of 
            // view, by setting its x translation to the right of
            // our container (container's width)
            nextView.TranslationX = this.Width;
            
            // Make the 'next' view visible so we see it sliding in
            // now that it's translated outside of the container view and not 'seen'
            nextView.IsVisible = true;

            // Animate the translation of both the 'next' and 'current'
            // views so that we get a slide effect
            // The 'next' view slides in from the right and the
            // current view slides out of view to the right
            await Task.WhenAll(
                nextView.TranslateTo(0, 0, 500, Easing.CubicInOut),
                currentView.TranslateTo(-1 * this.Width, 0, 500, Easing.CubicInOut));

            // Reset the visibility and translation of the
            // current (now previous) view now that the animation is complete
            currentView.IsVisible = false;
            currentView.TranslationX = 0;

            // Invoke an event to know the step changed
            StepChanged?.Invoke(this, new StepChangedEventArgs(currentIndex, nextIndex));
        }

        public async Task Back()
        {
            var c = GetCurrentIndex();

            var currentIndex = c;
            var nextIndex = c + 1;

            if (nextIndex < 0 )
                nextIndex = Children.Count - 1;

            if (currentIndex == nextIndex)
                return;

            var currentView = Children[currentIndex] as VisualElement;
            var nextView = Children[nextIndex] as VisualElement;

            // Prepare the 'next' view to show, moving it out of 
            // view, by setting its x translation to the right of
            // our container (container's width)
            nextView.TranslationX = this.Width;

            // Make the 'next' view visible so we see it sliding in
            // now that it's translated outside of the container view and not 'seen'
            nextView.IsVisible = true;

            // Animate the translation of both the 'next' and 'current'
            // views so that we get a slide effect
            // The 'next' view slides in from the right and the
            // current view slides out of view to the right
            await Task.WhenAll(
                nextView.TranslateTo(0, 0, 500, Easing.CubicInOut),
                currentView.TranslateTo(-1 * this.Width, 0, 500, Easing.CubicInOut));

            // Reset the visibility and translation of the
            // current (now previous) view now that the animation is complete
            currentView.IsVisible = false;
            currentView.TranslationX = 0;

            // Invoke an event to know the step changed
            StepChanged?.Invoke(this, new StepChangedEventArgs(currentIndex, nextIndex));
        }

    }
}
