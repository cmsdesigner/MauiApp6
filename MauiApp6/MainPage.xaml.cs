
using MauiApp6.Controls;
using System.Diagnostics;


namespace MauiApp6
{
    public partial class MainPage : ContentPage
    {

      
        public MainPage()
        {
            InitializeComponent();
           
        }

        private async void OnPreviousClicked(object sender, EventArgs e)
        {
            
            await stepsControl.Back();
        }

        private async void OnNextClicked(object sender, EventArgs e)
        {
            

            await stepsControl.Forward();

        }

        private void OnEnregistrerClicked(object sender, EventArgs e)
        {
            
        }

        private void stepsControl_StepChanged(object sender, Controls.WizardStepsControl.StepChangedEventArgs e)
        {
            Debug.WriteLine(e.StepIndex.ToString());
            
        }
    }

}
