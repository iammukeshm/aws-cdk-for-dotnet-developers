using Amazon.CDK;

namespace CdkForDotnetDevelopers
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();
            new CdkForDotnetDevelopersStack(app, "CdkForDotnetDevelopersStack", new StackProps
            {
            });
            app.Synth();
        }
    }
}
