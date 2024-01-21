using System.Text;
using Inlogic.MessageBoard.Console.CommandQuery.Queries;
using MediatR;

namespace Inlogic.MessageBoard.Console.Handlers.Queries;

public class ReadProjectQueryHandler(IInMemoryStateStore stateStore) : IRequestHandler<ReadProjectQuery>
{
  private readonly IInMemoryStateStore _stateStore = stateStore;

  async Task IRequestHandler<ReadProjectQuery>.Handle(ReadProjectQuery request, CancellationToken cancellationToken)
  {
    var projectMessages = _stateStore.ReadProjectMessages(request.ProjectName);
    var stringBuilder = new StringBuilder();

    foreach (var message in projectMessages)
    {
      stringBuilder.AppendLine(message.UserName);
      stringBuilder.Append(message.Content);
      stringBuilder.Append($" ( {Math.Round((DateTime.Now - message.Timestamp).TotalMinutes, 0)} Minutes ago )");
      stringBuilder.AppendLine();
    }
    var mes = stringBuilder.ToString();
    System.Console.WriteLine(mes);
    await Task.CompletedTask;
  }
}
