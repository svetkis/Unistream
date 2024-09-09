using MediatR;
using System.Text.Json;

namespace Unistream.TestTask.BLL.Transactions.Queries.DeserializeTransaction;

public class SerializationHandler
    : IRequestHandler<SerializeTransactionQuery, string>
{
    private readonly JsonSerializerOptions _serializationOptions;

    public SerializationHandler()
    {
        _serializationOptions = new JsonSerializerOptions
        {
//            Converters = { new DateTimeConverter() }
        };
    }

    public async Task<string> Handle(SerializeTransactionQuery request, CancellationToken cancellationToken)
    {
        return JsonSerializer.Serialize(request.Dto, _serializationOptions);
    }
}
