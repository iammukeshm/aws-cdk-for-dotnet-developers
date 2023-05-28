using Amazon.CDK;
using Amazon.CDK.AWS.DynamoDB;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.S3;
using Constructs;

namespace CdkForDotnetDevelopers
{
    public class CdkForDotnetDevelopersStack : Stack
    {
        internal CdkForDotnetDevelopersStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            var s3Bucket = new Bucket(this, "demobucket123", new BucketProps
            {
                Versioned = true,
                AutoDeleteObjects = true,
                RemovalPolicy = RemovalPolicy.DESTROY
            });

            var ddbTable = new Table(this, "product-table", new TableProps
            {
                TableName = "products",
                PartitionKey = new Attribute { Name = "id", Type = AttributeType.STRING },
                BillingMode = BillingMode.PAY_PER_REQUEST

            });

            var lambdaFunction = new Function(this, "test-function", new FunctionProps
            {
                Runtime = Runtime.DOTNET_6,
                Code = Code.FromAsset("./src/TestLambda/releases/test.zip"),
                Handler = "TestLambda::TestLambda.Function::FunctionHandler",
                MemorySize = 1024,
            });

            new CfnOutput(this, "BucketArn", new CfnOutputProps { Value = s3Bucket.BucketArn });

            new CfnOutput(this, "TableArn", new CfnOutputProps { Value = ddbTable.TableArn });
        }
    }
}
