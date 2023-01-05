//using NSwag.Generation.Processors;
//using NSwag.Generation.Processors.Contexts;
//using System.Threading.Tasks;

//public class SampleHeaderOperationProcessor : IOperationProcessor
//{
//    public Task<bool> ProcessAsync(OperationProcessorContext context)
//    {
//        context.OperationDescription.Operation.Parameters.Add(
//            new SwaggerParameter
//            {
//                Name = "Sample",
//                Kind = SwaggerParameterKind.Header,
//                Type = NJsonSchema.JsonObjectType.String,
//                IsRequired = false,
//                Description = "This is a test header",
//                Default = "{{\"field1\": \"value1\", \"field2\": \"value2\"}}"
//            });

//        return Task.FromResult(true);
//    }
//}