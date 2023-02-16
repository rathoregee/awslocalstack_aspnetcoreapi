using Amazon.CDK;

namespace Cdk
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();            

            Amazon.CDK.Environment makeEnv(string account, string region)
            {
                return new Amazon.CDK.Environment
                {
                    Account = account,
                    Region = region
                };
            }
           
            var localStack = makeEnv(account: "test", region: "ap-southeast-1");

            new CdkStack(app, "localstack", new StackProps { Env = localStack });

            app.Synth();
        }
    }
}
