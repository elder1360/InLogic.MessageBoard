using System.Text;
using Inlogic.MessageBoard.Console;
using Inlogic.MessageBoard.Console.Requests;
using MediatR;

public class ReadProjectHandler : IRequestHandler<ReadProjectQuery, Unit>
{
  IInMemoryStateStore _stateStore;
  public ReadProjectHandler(IInMemoryStateStore stateStore)
  {
    this._stateStore = stateStore;
  }
  public Task<Unit> Handle(ReadProjectQuery request, CancellationToken cancellationToken)
  {

    var projectMessages = _stateStore.ReadProjectMessages(request.ProjectName);
    var stringBuilder = new StringBuilder();

    foreach (var message in projectMessages)
    {
      stringBuilder.AppendLine(message.UserName);
      stringBuilder.Append(message.Content);
      stringBuilder.Append($" ( {message.Timestamp.Minute} Minutes ago )");
      stringBuilder.AppendLine();
    }
    var mes = stringBuilder.ToString();
    Console.WriteLine(mes);
    return Task.FromResult(Unit.Value);
  }
}

