using Inlogic.MessageBoard.Console.CommandQuery.Commands;
using MediatR;

namespace Inlogic.MessageBoard.Console.Handlers.Commands;

public class PostMessageCommandHandler(IInMemoryStateStore stateStore) : IRequestHandler<PostMessageCommand>
{
  private readonly IInMemoryStateStore _stateStore = stateStore;

  Task IRequestHandler<PostMessageCommand>.Handle(PostMessageCommand request, CancellationToken cancellationToken)
  {
    _stateStore.PostMessage(request.UserName, request.ProjectName, request.Message);
    System.Console.WriteLine($"{request.UserName} posted a message to {request.ProjectName}: {request.Message}");
    return Task.CompletedTask;
  }
}
