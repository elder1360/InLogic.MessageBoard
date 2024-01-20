using Inlogic.MessageBoard.Console;
using Inlogic.MessageBoard.Console.Requests;
using MediatR;

public class PostMessageHandler : IRequestHandler<PostMessageCommand, Unit>
{
  IInMemoryStateStore _stateStore;
  public PostMessageHandler(IInMemoryStateStore stateStore)
  {
    this._stateStore = stateStore;
  }
  public Task<Unit> Handle(PostMessageCommand request, CancellationToken cancellationToken)
  {
    _stateStore.PostMessage(request.UserName, request.ProjectName, request.Message);
    Console.WriteLine($"{request.UserName} posted a message to {request.ProjectName}: {request.Message}");
    return Task.FromResult(Unit.Value);
  }
}

